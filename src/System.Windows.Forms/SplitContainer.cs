using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
    public class SplitContainer : Control, ISupportInitialize
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
                        NativeMethods.QSplitter_SetOrientation(Handle, (int)value);
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
                        NativeMethods.QSplitter_SetSplitterDistance(Handle, value, WidgetSize);
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
                    NativeMethods.QSplitter_SetHandleWidth(Handle, value);
                }
            }
        }

        private int WidgetSize => Orientation == Orientation.Horizontal ? Height : Width;

        protected override void CreateHandle()
        {
            Handle = NativeMethods.QSplitter_Create(IntPtr.Zero, (int)_orientation);
            SetCommonProperties();
            
            // Set handle width
            NativeMethods.QSplitter_SetHandleWidth(Handle, _splitterWidth);

            CreateChildren();

            // After children are created, we can apply FixedPanel and SplitterDistance
            UpdateSplitterStretch();
            
            // Apply SplitterDistance if needed. 
            // Note: sizes might not be accurate until layout happens.
            if (_splitterDistance > 0)
            {
                NativeMethods.QSplitter_SetSplitterDistance(Handle, _splitterDistance, WidgetSize);
            }
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

            NativeMethods.QSplitter_SetStretchFactor(Handle, 0, stretch1);
            NativeMethods.QSplitter_SetStretchFactor(Handle, 1, stretch2);
        }
    }
}
