using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Form : Control
    {
        private string _text = string.Empty;
        private FormWindowState _windowState = FormWindowState.Normal;
        private MenuStrip? _mainMenuStrip;

        public Form() : base()
        {
            Visible = false;
            Size = new Size(800, 600);
        }


        public event EventHandler? Load;
        public event EventHandler<FormClosedEventArgs>? FormClosed;
        public event EventHandler<FormClosingEventArgs>? FormClosing;

        protected virtual void OnLoad(EventArgs e) => Load?.Invoke(this, e);

        protected virtual void OnFormClosed(FormClosedEventArgs e) => FormClosed?.Invoke(this, e);

        protected virtual void OnFormClosing(FormClosingEventArgs e) => FormClosing?.Invoke(this, e);

        protected override void CreateHandle()
        {
            base.CreateHandle();
            NativeMethods.QWidget_Resize(Handle, Size.Width, Size.Height);
            NativeMethods.QWidget_SetTitle(Handle, _text);
            if (_windowState != FormWindowState.Normal)
            {
                NativeMethods.QWidget_SetWindowState(Handle, (int)_windowState);
            }
            UpdateFormStyles();
            
            // Attach menu bar if set
            if (_mainMenuStrip != null)
            {
                if (!_mainMenuStrip.IsHandleCreated)
                {
                    _mainMenuStrip.EnsureCreated();
                }
                NativeMethods.QWidget_SetMenuBar(Handle, _mainMenuStrip.Handle);
            }
            
            // Connect resize and move events
            ConnectResizeEvent();
            ConnectCloseEvent();
            OnLoad(EventArgs.Empty);
        }

        private void ConnectResizeEvent()
        {
            unsafe
            {
                var resizeCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int, int, void>)&OnResizeCallback;
                var moveCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int, int, void>)&OnMoveCallback;
                NativeMethods.QWidget_ConnectResize(Handle, resizeCallbackPtr, moveCallbackPtr, GCHandlePtr);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnResizeCallback(nint userData, int width, int height)
        {
            var form = ObjectFromGCHandle<Form>(userData);
            form.SetBoundsCore(form.Location.X, form.Location.Y, width, height);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnMoveCallback(nint userData, int x, int y)
        {
            var form = ObjectFromGCHandle<Form>(userData);
            // Update location - use SetBoundsCore to update internal state
            // Don't trigger resize event since size hasn't changed
            var currentSize = form.Size;
            form.SetBoundsCore(x, y, currentSize.Width, currentSize.Height);
        }

        private void ConnectCloseEvent()
        {
            unsafe
            {
                var closeCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int>)&OnCloseCallback;
                var closedCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, void>)&OnClosedCallback;
                NativeMethods.QWidget_ConnectCloseEvent(Handle, closeCallbackPtr, closedCallbackPtr, GCHandlePtr);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe int OnCloseCallback(nint userData)
        {
            var form = ObjectFromGCHandle<Form>(userData);
            var args = new FormClosingEventArgs(CloseReason.UserClosing, false);
            form.OnFormClosing(args);
            // Return 1 to allow close, 0 to cancel
            return args.Cancel ? 0 : 1;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnClosedCallback(nint userData)
        {
            var form = ObjectFromGCHandle<Form>(userData);
            var args = new FormClosedEventArgs(CloseReason.UserClosing);
            form.OnFormClosed(args);
        }

        public Size ClientSize
        {
            get => Size;
            set => Size = value;
        }
        public override string Text 
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetTitle(Handle, _text);
                }
            }
            }


        public MenuStrip? MainMenuStrip
        {
            get => _mainMenuStrip;
            set
            {
                _mainMenuStrip = value;
                if (_mainMenuStrip != null && IsHandleCreated)
                {
                    if (!_mainMenuStrip.IsHandleCreated)
                    {
                        _mainMenuStrip.EnsureCreated();
                    }
                    NativeMethods.QWidget_SetMenuBar(Handle, _mainMenuStrip.Handle);
                }
            }
        }

        public FormWindowState WindowState
        {
            get
            {
                if (IsHandleCreated)
                {
                    return (FormWindowState)NativeMethods.QWidget_GetWindowState(Handle);
                }
                return _windowState;
            }
            set
            {
                if (_windowState != value)
                {
                    _windowState = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QWidget_SetWindowState(Handle, (int)_windowState);
                    }
                }
            }
        }

        public void Close()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QWidget_Close(Handle);
            }
        }


        private bool _minimizeBox = true;
        private bool _maximizeBox = true;
        private bool _showInTaskbar = true;
        private bool _showIcon = true;
        private bool _topMost = false;

        public bool MinimizeBox
        {
            get => _minimizeBox;
            set
            {
                if (_minimizeBox != value)
                {
                    _minimizeBox = value;
                    UpdateFormStyles();
                }
            }
        }

        public bool MaximizeBox
        {
            get => _maximizeBox;
            set
            {
                if (_maximizeBox != value)
                {
                    _maximizeBox = value;
                    UpdateFormStyles();
                }
            }
        }

        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set
            {
                if (_showInTaskbar != value)
                {
                    _showInTaskbar = value;
                    UpdateFormStyles();
                }
            }
        }

        public bool ShowIcon
        {
            get => _showIcon;
            set
            {
                if (_showIcon != value)
                {
                    _showIcon = value;
                    UpdateFormStyles();
                }
            }
        }

        public bool TopMost
        {
            get => _topMost;
            set
            {
                if (_topMost != value)
                {
                    _topMost = value;
                    UpdateFormStyles();
                }
            }
        }

        private FormBorderStyle _formBorderStyle = FormBorderStyle.Sizable;

        public FormBorderStyle FormBorderStyle
        {
            get => _formBorderStyle;
            set
            {
                if (_formBorderStyle != value)
                {
                    _formBorderStyle = value;
                    UpdateFormStyles();
                }
            }
        }

        [Obsolete(NotImplementedWarning)] public Icon Icon { get; set; }

        private void UpdateFormStyles()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QWidget_SetFormProperties(Handle, _minimizeBox, _maximizeBox, _showInTaskbar, _showIcon, _topMost, (int)_formBorderStyle);
            }
        }
    }
}
