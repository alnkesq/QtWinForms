using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Cdecl = System.Runtime.CompilerServices.CallConvCdecl;

namespace System.Windows.Forms
{
    public class SplitContainer : ContainerControl, ISupportInitialize
    {
        private SplitterPanel _panel1;
        private SplitterPanel _panel2;
        private Orientation _orientation = Orientation.Vertical;
        private FixedPanel _fixedPanel = FixedPanel.None;
        private int _splitterDistance = 50; // Default value, though usually it's calculated
        private int _splitterWidth = 4;

        public SplitContainer()
        {
            _panel1 = new SplitterPanel(this);
            _panel2 = new SplitterPanel(this);
            
            // Add panels to Controls collection so they are created and parented correctly
            Controls.Add(_panel1);
            Controls.Add(_panel2);

            // Default size
            Size = new Size(150, 100);
        }

        public SplitterPanel Panel1 => _panel1;
        public SplitterPanel Panel2 => _panel2;

        public Orientation Orientation
        {
            get => _orientation;
            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QSplitter_SetOrientation(QtHandle, (int)value);
                    }
                }
            }
        }

        public FixedPanel FixedPanel
        {
            get => _fixedPanel;
            set
            {
                if (_fixedPanel != value)
                {
                    _fixedPanel = value;
                    UpdateSplitterStretch();
                }
            }
        }

        public int SplitterDistance
        {
            get => _splitterDistance;
            set
            {
                if (_splitterDistance != value)
                {
                    _splitterDistance = value;
                    if (IsHandleCreated)
                    {
                        // We need to set sizes. QSplitter takes a list of sizes.
                        // But setting sizes is complex because it depends on the total size.
                        // For now, maybe just try to set it if we can.
                        // Actually, QSplitter::setSizes is the way.
                        // We need to know the total size to calculate the other panel's size.
                        // Or we can just set the size of the first one and let the second one take the rest?
                        // QSplitter::setSizes expects sizes for all widgets.
                        // Let's implement a helper to set splitter distance.
                        NativeMethods.QSplitter_SetSplitterDistance(QtHandle, value, WidgetSize);
                    }
                }
            }
        }

        public int SplitterWidth
        {
            get => _splitterWidth;
            set
            {
                _splitterWidth = value;
                // QSplitter handle width can be styled or set via setHandleWidth
                if (IsHandleCreated)
                {
                    NativeMethods.QSplitter_SetHandleWidth(QtHandle, value);
                }
            }
        }

        private int WidgetSize => Orientation == Orientation.Horizontal ? Height : Width;

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QSplitter_Create(IntPtr.Zero, (int)_orientation);
            SetCommonProperties();
            
            // Set handle width
            NativeMethods.QSplitter_SetHandleWidth(QtHandle, _splitterWidth);

            // Connect to splitter moved event
            ConnectSplitterMoved();

            // Connect to resize event to handle form resizing
            ConnectResizeEvent();

            // Pre-calculate panel sizes based on SplitterDistance and container size
            // This ensures docked/anchored controls have correct parent sizes from the start
            UpdatePanelSizes();

            CreateChildren();

            // After children are created, we can apply FixedPanel and SplitterDistance
            UpdateSplitterStretch();
            
            // Apply SplitterDistance if needed. 
            // Note: sizes might not be accurate until layout happens.
            if (_splitterDistance > 0)
            {
                NativeMethods.QSplitter_SetSplitterDistance(QtHandle, _splitterDistance, WidgetSize);
            }
        }

        private void ConnectResizeEvent()
        {
            if (!IsHandleCreated) return;

            unsafe
            {
                var resizeCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int, int, void>)&OnResizeCallback;
                // We don't need move callback for SplitContainer, pass null
                NativeMethods.QWidget_ConnectResize(QtHandle, resizeCallbackPtr, IntPtr.Zero, GCHandlePtr);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnResizeCallback(nint userData, int width, int height)
        {
            GCHandle handle = GCHandle.FromIntPtr(userData);
            if (handle.Target is SplitContainer container)
            {
                container.OnSplitterResize(width, height);
            }
        }

        private void OnSplitterResize(int width, int height, bool allowReschedule = true)
        {
            // Update internal size using SetBoundsCore
            SetBoundsCore(Location.X, Location.Y, width, height);

            // Query the actual splitter sizes from Qt
            // This is important because Qt may have adjusted the splitter position
            // based on FixedPanel settings or other constraints
            if (IsHandleCreated)
            {
                NativeMethods.QSplitter_GetSizes(QtHandle, out int size1, out int size2);
                if (!(size1 == 0 && size2 == 0))
                {
                    _splitterDistance = size1;
                }
            }
            if (allowReschedule)
            {
                SynchronizationContext.Current!.Post(_ =>
                {
                    if (!IsDisposed)
                        OnSplitterResize(Width, Height, allowReschedule: false);
                }, null);
            }
            // Update panel sizes and trigger layout on their children
            UpdatePanelSizes();
        }

        private void ConnectSplitterMoved()
        {
            if (!IsHandleCreated) return;

            unsafe
            {
                delegate* unmanaged[Cdecl]<IntPtr, int, int, void> callback = &OnSplitterMovedCallback;
                NativeMethods.QSplitter_ConnectSplitterMoved(QtHandle, (IntPtr)callback, GCHandle.ToIntPtr(GCHandle.Alloc(this)));
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void OnSplitterMovedCallback(IntPtr userData, int pos, int index)
        {
            GCHandle handle = GCHandle.FromIntPtr(userData);
            if (handle.Target is SplitContainer container)
            {
                container.OnSplitterMoved(pos, index);
            }
        }

        private void OnSplitterMoved(int pos, int index)
        {
            // Update the splitter distance
            _splitterDistance = pos;

            // Update panel sizes and trigger layout
            UpdatePanelSizes();
        }

        private void UpdatePanelSizes()
        {
            // Calculate panel sizes based on orientation and splitter distance
            if (_orientation == Orientation.Horizontal)
            {
                // Horizontal: panels are stacked vertically
                _panel1.SetBoundsCore(0, 0, Width, _splitterDistance);
                _panel2.SetBoundsCore(0, _splitterDistance + _splitterWidth, Width, Height - _splitterDistance - _splitterWidth);
            }
            else
            {
                // Vertical: panels are side by side
                _panel1.SetBoundsCore(0, 0, _splitterDistance, Height);
                _panel2.SetBoundsCore(_splitterDistance + _splitterWidth, 0, Width - _splitterDistance - _splitterWidth, Height);
            }

            // Note: SetBoundsCore already calls OnResize which triggers PerformLayout
            // So we don't need to explicitly call PerformLayout here
        }

        private void UpdateSplitterStretch()
        {
            if (!IsHandleCreated) return;

            // Index 0 is Panel1, Index 1 is Panel2
            int stretch1 = 1;
            int stretch2 = 1;

            if (_fixedPanel == FixedPanel.Panel1)
            {
                stretch1 = 0;
                stretch2 = 1;
            }
            else if (_fixedPanel == FixedPanel.Panel2)
            {
                stretch1 = 1;
                stretch2 = 0;
            }

            NativeMethods.QSplitter_SetStretchFactor(QtHandle, 0, stretch1);
            NativeMethods.QSplitter_SetStretchFactor(QtHandle, 1, stretch2);
        }
    }
}
