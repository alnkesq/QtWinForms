using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class Control : IWin32Window, IDisposable
    {
        // ... (existing code)
        public IntPtr Handle { get; protected set; }
        internal GCHandle<Control>? gcHandle;

        internal IntPtr GCHandlePtr => GCHandle<Control>.ToIntPtr((gcHandle ??= new GCHandle<Control>(this)));
        internal static T ObjectFromGCHandle<T>(IntPtr gcHandle) where T : class => GCHandle<T>.FromIntPtr(gcHandle).Target;

        public bool InvokeRequired => Environment.CurrentManagedThreadId != Application._mainThreadId;

        public void Invoke(Action action)
        {
            if (InvokeRequired) Application._synchronizationContext!.Send(a => ((Action)a!)(), action);
            action();
        }
        public T Invoke<T>(Func<T> func)
        {
            if (InvokeRequired)
            {
                T result = default!;
                Application._synchronizationContext!.Send(_ => result = func(), null);
                return result;
            }
            return func();
        }

        public object? Invoke(Delegate d, params object?[]? args)
        {
            if (InvokeRequired)
            {
                object? result = null;
                Application._synchronizationContext!.Send(_ => result = d.DynamicInvoke(args), null);
                return result;
            }
            return d.DynamicInvoke(args);
        }

        public Control()
        {
            Application.InitializeQt();
            Controls = new ControlCollection(this);
        }

        public Control? Parent { get; internal set; }

        public bool IsHandleCreated => Handle != default;

        public ControlCollection Controls { get; }

        internal void EnsureCreated()
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }
        }

        protected virtual void CreateHandle()
        {
            Handle = NativeMethods.QWidget_Create(IntPtr.Zero);
            SetCommonProperties();
            CreateChildren();
        }

        protected void CreateChildren()
        {

            // Create handles for any child controls that were added before this control was created
            foreach (Control child in Controls)
            {
                child.EnsureCreated();

                // Set parent relationship in Qt
                NativeMethods.QWidget_SetParent(child.Handle, Handle);

                // Apply position and size (Qt needs this after setParent)
                NativeMethods.QWidget_Move(child.Handle, child.Location.X, child.Location.Y);
                NativeMethods.QWidget_Resize(child.Handle, child.Size.Width, child.Size.Height);

                // Initialize anchor bounds now that parent is set
                child.InitializeAnchorBounds();

                // QWidget::setParent hides the widget, so we must show it again if it's supposed to be visible
                if (child.Visible)
                {
                    NativeMethods.QWidget_Show(child.Handle);
                }
            }

            // Trigger layout to handle docking/anchoring
            PerformLayout();
        }

        protected void SetCommonProperties()
        {
            Application.SetMainThreadOrEnsureMainThread();
            UpdateContextMenuPolicy();

            if (_backColor != Color.Empty)
            {
                NativeMethods.QWidget_SetBackColor(Handle, _backColor.R, _backColor.G, _backColor.B, _backColor.A);
            }
            if (_foreColor != Color.Empty)
            {
                NativeMethods.QWidget_SetForeColor(Handle, _foreColor.R, _foreColor.G, _foreColor.B, _foreColor.A);
            }

            if (!_enabled)
            {
                NativeMethods.QWidget_SetEnabled(Handle, _enabled);
            }

            if (_font != null)
            {
                NativeMethods.QWidget_SetFont(Handle, _font.FontFamily.Name, _font.SizeInPoints, _font.Bold, _font.Italic, _font.Underline, _font.Strikeout);
            }

            if (_visible)
            {
                NativeMethods.QWidget_Show(Handle);
            }
            else
            {
                NativeMethods.QWidget_Hide(Handle);
            }
        }

        public virtual string Text
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    if (value) EnsureCreated();

                    if (IsHandleCreated)
                    {
                        if (value)
                        {
                            NativeMethods.QWidget_Show(Handle);
                        }
                        else
                        {
                            NativeMethods.QWidget_Hide(Handle);
                        }
                    }
                }
            }
        }
        private bool _visible = true;

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public Point Location
        {
            get => _location;
            set
            {
                _location = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_Move(Handle, value.X, value.Y);
                }
            }
        }
        private Point _location;

        public Size Size
        {
            get => _size;
            set
            {
                _size = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_Resize(Handle, value.Width, value.Height);
                }
            }
        }
        private Size _size = new Size(100, 30); // Default button size

        public int Left
        {
            get => Location.X;
            set => Location = new Point(value, Location.Y);
        }

        public int Top
        {
            get => Location.Y;
            set => Location = new Point(Location.X, value);
        }

        public int Width
        {
            get => Size.Width;
            set => Size = new Size(value, Size.Height);
        }

        public int Height
        {
            get => Size.Height;
            set => Size = new Size(Size.Width, value);
        }

        public Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetBackColor(Handle, value.R, value.G, value.B, value.A);
                }
            }
        }
        private Color _foreColor = Color.Empty;
        public Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetForeColor(Handle, value.R, value.G, value.B, value.A);
                }
            }
        }
        private Color _backColor = Color.Empty;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetEnabled(Handle, value);
                }
            }
        }
        private bool _enabled = true;

        // Dock and Anchor properties
        private DockStyle _dock = DockStyle.None;
        private AnchorStyles _anchor = AnchorStyles.Top | AnchorStyles.Left;
        private Rectangle _anchorBounds; // Stores initial bounds for anchor calculations
        private int _anchorDistLeft;   // Distance from left edge when anchored
        private int _anchorDistTop;    // Distance from top edge when anchored
        private int _anchorDistRight;  // Distance from right edge when anchored
        private int _anchorDistBottom; // Distance from bottom edge when anchored

        public DockStyle Dock
        {
            get => _dock;
            set
            {
                if (_dock != value)
                {
                    _dock = value;
                    // Setting Dock disables Anchor
                    if (value != DockStyle.None)
                    {
                        _anchor = AnchorStyles.Top | AnchorStyles.Left;
                    }
                    Parent?.PerformLayout();
                }
            }
        }

        public AnchorStyles Anchor
        {
            get => _anchor;
            set
            {
                if (_anchor != value)
                {
                    _anchor = value;
                    // Setting Anchor disables Dock
                    if (value != (AnchorStyles.Top | AnchorStyles.Left))
                    {
                        _dock = DockStyle.None;
                    }
                    // Store current bounds for anchor calculations
                    if (Parent != null)
                    {
                        _anchorBounds = new Rectangle(Location, Size);
                    }
                }
            }
        }

        // Resize event
        public event EventHandler? Resize;

        protected virtual void OnResize(EventArgs e)
        {
            Resize?.Invoke(this, e);
            // When this control resizes, perform layout on children
            PerformLayout();
        }

        internal void InitializeAnchorBounds()
        {
            // Store initial bounds for anchor calculations
            // This is called when a control is added to a parent
            if (Parent != null && _anchorBounds.IsEmpty)
            {
                _anchorBounds = new Rectangle(Location, Size);

                // Calculate and store distances from parent edges
                _anchorDistLeft = Location.X;
                _anchorDistTop = Location.Y;
                _anchorDistRight = Parent.Width - (Location.X + Size.Width);
                _anchorDistBottom = Parent.Height - (Location.Y + Size.Height);
            }
        }

        internal void SetBoundsCore(int x, int y, int width, int height)
        {
            bool sizeChanged = _size.Width != width || _size.Height != height;
            var positionChanged = _location.X != x || _location.Y != y;

            _location = new Point(x, y);
            _size = new Size(width, height);

            if (IsHandleCreated)
            {
                if (positionChanged)
                    NativeMethods.QWidget_Move(Handle, x, y);
                if (sizeChanged)
                    NativeMethods.QWidget_Resize(Handle, width, height);
            }

            if (sizeChanged)
            {
                OnResize(EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void SuspendLayout() { }
        public void ResumeLayout(bool performLayout) { }

        public void PerformLayout()
        {
            if (!IsHandleCreated) return;

            // Perform dock layout
            PerformDockLayout();

            // Perform anchor layout (for controls that aren't docked)
            foreach (Control child in Controls)
            {
                if (child.Dock == DockStyle.None && child.Anchor != (AnchorStyles.Top | AnchorStyles.Left))
                {
                    child.UpdateAnchorPosition();
                }
            }
        }

        private void PerformDockLayout()
        {
            if (!IsHandleCreated) return;

            // Track remaining client area
            Rectangle clientRect = new Rectangle(0, 0, Width, Height);

            // Process docked controls in Z-order (order they were added)
            // We need to process them in the order: Top, Bottom, Left, Right, Fill
            // But respect the order they were added for controls with the same dock style

            var dockedControls = new System.Collections.Generic.List<Control>();
            foreach (Control child in Controls)
            {
                if (child.Dock != DockStyle.None)
                {
                    dockedControls.Add(child);
                }
            }

            // Process in dock priority order, but maintain add order within same dock style
            ProcessDockedControls(dockedControls, DockStyle.Top, ref clientRect);
            ProcessDockedControls(dockedControls, DockStyle.Bottom, ref clientRect);
            ProcessDockedControls(dockedControls, DockStyle.Left, ref clientRect);
            ProcessDockedControls(dockedControls, DockStyle.Right, ref clientRect);
            ProcessDockedControls(dockedControls, DockStyle.Fill, ref clientRect);
        }

        private void ProcessDockedControls(System.Collections.Generic.List<Control> controls, DockStyle dockStyle, ref Rectangle clientRect)
        {
            foreach (Control child in controls)
            {
                if (child.Dock != dockStyle) continue;

                switch (dockStyle)
                {
                    case DockStyle.Top:
                        child.SetBoundsCore(clientRect.X, clientRect.Y, clientRect.Width, child.Height);
                        clientRect.Y += child.Height;
                        clientRect.Height -= child.Height;
                        break;

                    case DockStyle.Bottom:
                        child.SetBoundsCore(clientRect.X, clientRect.Y + clientRect.Height - child.Height, clientRect.Width, child.Height);
                        clientRect.Height -= child.Height;
                        break;

                    case DockStyle.Left:
                        child.SetBoundsCore(clientRect.X, clientRect.Y, child.Width, clientRect.Height);
                        clientRect.X += child.Width;
                        clientRect.Width -= child.Width;
                        break;

                    case DockStyle.Right:
                        child.SetBoundsCore(clientRect.X + clientRect.Width - child.Width, clientRect.Y, child.Width, clientRect.Height);
                        clientRect.Width -= child.Width;
                        break;

                    case DockStyle.Fill:
                        child.SetBoundsCore(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height);
                        break;
                }
            }
        }

        private void UpdateAnchorPosition()
        {
            if (Parent == null || _anchorBounds.IsEmpty) return;

            int newX = _anchorBounds.X;
            int newY = _anchorBounds.Y;
            int newWidth = _anchorBounds.Width;
            int newHeight = _anchorBounds.Height;

            // Calculate horizontal anchoring
            bool anchorLeft = (_anchor & AnchorStyles.Left) == AnchorStyles.Left;
            bool anchorRight = (_anchor & AnchorStyles.Right) == AnchorStyles.Right;

            if (anchorLeft && anchorRight)
            {
                // Anchored to both sides - stretch horizontally
                // Maintain distance from both left and right edges
                newX = _anchorDistLeft;
                newWidth = Parent.Width - _anchorDistLeft - _anchorDistRight;
                if (newWidth < 0) newWidth = 0;
            }
            else if (anchorRight && !anchorLeft)
            {
                // Anchored to right only - maintain distance from right edge
                newX = Parent.Width - _anchorDistRight - _anchorBounds.Width;
            }
            else if (anchorLeft)
            {
                // Anchored to left only - maintain distance from left edge
                newX = _anchorDistLeft;
            }
            // else: not anchored horizontally - keep original X

            // Calculate vertical anchoring
            bool anchorTop = (_anchor & AnchorStyles.Top) == AnchorStyles.Top;
            bool anchorBottom = (_anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom;

            if (anchorTop && anchorBottom)
            {
                // Anchored to both top and bottom - stretch vertically
                // Maintain distance from both top and bottom edges
                newY = _anchorDistTop;
                newHeight = Parent.Height - _anchorDistTop - _anchorDistBottom;
                if (newHeight < 0) newHeight = 0;
            }
            else if (anchorBottom && !anchorTop)
            {
                // Anchored to bottom only - maintain distance from bottom edge
                newY = Parent.Height - _anchorDistBottom - _anchorBounds.Height;
            }
            else if (anchorTop)
            {
                // Anchored to top only - maintain distance from top edge
                newY = _anchorDistTop;
            }
            // else: not anchored vertically - keep original Y

            SetBoundsCore(newX, newY, newWidth, newHeight);
        }

        public string? Name { get; set; }
        public object? Tag { get; set; }
        [Obsolete(NotImplementedWarning)] public bool UseVisualStyleBackColor { get; set; } = true;
        [Obsolete(NotImplementedWarning)] public int TabIndex { get; set; } = 0;
        [Obsolete(NotImplementedWarning)] public AutoScaleMode AutoScaleMode { get; set; }
        [Obsolete(NotImplementedWarning)] public bool AutoSize { get; set; }
        [Obsolete(NotImplementedWarning)] public SizeF AutoScaleDimensions { get; set; }

        protected const string NotImplementedWarning = "Not implemented, NOP";

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose children
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    Controls[i].Dispose();
                }
            }

            if (IsHandleCreated)
            {
                NativeMethods.QWidget_Destroy(Handle);
                Handle = IntPtr.Zero;
            }

            if (disposing && gcHandle != null)
            {
                gcHandle.Value.Dispose();
                gcHandle = null;
            }
        }

        public virtual Font Font
        {
            get => _font ??= SystemFonts.DefaultFont; // Default to system font if not set
            set
            {
                if (_font != value)
                {
                    _font = value;
                    if (IsHandleCreated && _font != null)
                    {
                        NativeMethods.QWidget_SetFont(Handle, _font.FontFamily.Name, _font.SizeInPoints, _font.Bold, _font.Italic, _font.Underline, _font.Strikeout);
                    }
                }
            }
        }
        private Font? _font;
        [Obsolete(NotImplementedWarning)] public ContentAlignment TextAlign { get; set; }
        [Obsolete(NotImplementedWarning)] public event PreviewKeyDownEventHandler? PreviewKeyDown;

        public virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e) => PreviewKeyDown?.Invoke(this, e);

        private ContextMenuStrip? _contextMenuStrip;

        public ContextMenuStrip? ContextMenuStrip
        {
            get => _contextMenuStrip;
            set
            {
                if (_contextMenuStrip != value)
                {
                    _contextMenuStrip = value;
                    UpdateContextMenuPolicy();
                }
            }
        }

        private void UpdateContextMenuPolicy()
        {
            if (IsHandleCreated)
            {
                if (_contextMenuStrip != null)
                {
                    NativeMethods.QWidget_SetContextMenuPolicy(Handle, 3); // Qt::CustomContextMenu
                    ConnectCustomContextMenuRequested();
                }
                else
                {
                    NativeMethods.QWidget_SetContextMenuPolicy(Handle, 0); // Qt::DefaultContextMenu
                }
            }
        }

        private unsafe void ConnectCustomContextMenuRequested()
        {
            delegate* unmanaged[Cdecl]<nint, int, int, void> callback = &OnCustomContextMenuRequestedCallback;
            NativeMethods.QWidget_ConnectCustomContextMenuRequested(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnCustomContextMenuRequestedCallback(nint userData, int x, int y)
        {
            var control = ObjectFromGCHandle<Control>(userData);
            control.OnContextMenuRequested(x, y);
        }

        private void OnContextMenuRequested(int x, int y)
        {
            if (_contextMenuStrip != null)
            {
                var screenPoint = PointToScreen(new Point(x, y));
                _contextMenuStrip.Show(screenPoint);
            }
        }

        public Point PointToScreen(Point p)
        {
            if (!IsHandleCreated) return p;
            int sx = 0, sy = 0;
            NativeMethods.QWidget_MapToGlobal(Handle, p.X, p.Y, out sx, out sy);
            return new Point(sx, sy);
        }

        public override string ToString()
        {
            var text = Text;
            return string.IsNullOrEmpty(text) ? base.ToString()! : text;
        }
    }
}
