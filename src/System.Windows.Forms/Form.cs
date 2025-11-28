using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Form : ContainerControl
    {
        private string _text = string.Empty;
        private FormWindowState _windowState = FormWindowState.Normal;
        private MenuStrip? _mainMenuStrip;

        [Obsolete(NotImplementedWarning)] public bool KeyPreview { get; set; }

        public Form() : base()
        {
            Visible = false;
            Size = new Size(800, 600);
        }


        public event EventHandler? Load;
        public event EventHandler? Shown;
        public event FormClosedEventHandler? FormClosed;
        public event FormClosingEventHandler? FormClosing;

        private Button? _acceptButton;
        private Button? _cancelButton;

        public Button? AcceptButton
        {
            get => _acceptButton;
            set
            {
                _acceptButton = value;
                UpdateAcceptCancelButtons();
            }
        }

        public Button? CancelButton
        {
            get => _cancelButton;
            set
            {
                _cancelButton = value;
                UpdateAcceptCancelButtons();
            }
        }

        private void UpdateAcceptCancelButtons()
        {
            if (IsHandleCreated)
            {
                _acceptButton?.CreateControl();
                _cancelButton?.CreateControl();

                NativeMethods.QWidget_SetAcceptCancelButtons(
                    QtHandle,
                    _acceptButton?.QtHandle ?? IntPtr.Zero,
                    _cancelButton?.QtHandle ?? IntPtr.Zero
                );
            }
        }

        [Obsolete(NotImplementedWarning)] public Size MinimumSize { get; set; }

        protected virtual void OnLoad(EventArgs e) => Load?.Invoke(this, e);

        protected virtual void OnFormClosed(FormClosedEventArgs e) => FormClosed?.Invoke(this, e);

        protected virtual void OnFormClosing(FormClosingEventArgs e) => FormClosing?.Invoke(this, e);

        public void Activate()
        {
            Show();
            Focus();
        }

        protected override void CreateHandle()
        {
            var prevVisible = Visible;
            Visible = false;
            base.CreateHandle();
            NativeMethods.QWidget_Resize(QtHandle, Size.Width, Size.Height);
            NativeMethods.QWidget_SetTitle(QtHandle, _text);
            if (_windowState != FormWindowState.Normal)
            {
                NativeMethods.QWidget_SetWindowState(QtHandle, (int)_windowState);
            }
            UpdateIcon();

            // Attach menu bar if set
            if (_mainMenuStrip != null)
            {
                if (!_mainMenuStrip.IsHandleCreated)
                {
                    _mainMenuStrip.CreateControl();
                }
                NativeMethods.QWidget_SetMenuBar(QtHandle, _mainMenuStrip.QtHandle);

                // QWidget_SetMenuBar calls setParent, which hides the widget.
                // We must show it again if it's supposed to be visible.
                if (_mainMenuStrip.Visible)
                {
                    NativeMethods.QWidget_Show(_mainMenuStrip.QtHandle);
                }
            }

            if (_owner != null)
            {
                _owner.CreateControl();
                NativeMethods.Form_SetOwner(QtHandle, _owner.QtHandle);
            }
            UpdateFormStyles();

            // Connect resize and move events
            ConnectResizeEvent();
            ConnectCloseEvent();
            UpdateAcceptCancelButtons();
            Visible = prevVisible;
            OnLoad(EventArgs.Empty);
        }

        private void ConnectResizeEvent()
        {
            unsafe
            {
                var resizeCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int, int, void>)&OnResizeCallback;
                var moveCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int, int, void>)&OnMoveCallback;
                NativeMethods.QWidget_ConnectResize(QtHandle, resizeCallbackPtr, moveCallbackPtr, GCHandlePtr);
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

        protected override void UpdateVisibleCore(bool value)
        {
            base.UpdateVisibleCore(value);
            if (value)
                Shown?.Invoke(this, EventArgs.Empty);
        }

        private void ConnectCloseEvent()
        {
            unsafe
            {
                var closeCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, int>)&OnCloseCallback;
                var closedCallbackPtr = (IntPtr)(delegate* unmanaged[Cdecl]<nint, void>)&OnClosedCallback;
                NativeMethods.QWidget_ConnectCloseEvent(QtHandle, closeCallbackPtr, closedCallbackPtr, GCHandlePtr);
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

            // End dialog loop if any
            NativeMethods.Form_EndDialog(form.QtHandle);

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
                    NativeMethods.QWidget_SetTitle(QtHandle, _text);
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
                        _mainMenuStrip.CreateControl();
                    }
                    NativeMethods.QWidget_SetMenuBar(QtHandle, _mainMenuStrip.QtHandle);

                    if (_mainMenuStrip.Visible)
                    {
                        NativeMethods.QWidget_Show(_mainMenuStrip.QtHandle);
                    }
                }
            }
        }

        [Obsolete(NotImplementedWarning)] public FormStartPosition StartPosition { get; set; }

        public FormWindowState WindowState
        {
            get
            {
                if (IsHandleCreated)
                {
                    return (FormWindowState)NativeMethods.QWidget_GetWindowState(QtHandle);
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
                        NativeMethods.QWidget_SetWindowState(QtHandle, (int)_windowState);
                    }
                }
            }
        }

        public void Close()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QWidget_Close(QtHandle);
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

        private Icon? _icon;
        public Icon? Icon
        {
            get => _icon;
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    if (IsHandleCreated)
                    {
                        UpdateIcon();
                    }
                }
            }
        }

        private void UpdateIcon()
        {
            if (_icon != null)
            {
                NativeMethods.QWidget_SetIcon(QtHandle, _icon.GetQIcon());
            }
            else
            {
                NativeMethods.QWidget_SetIcon(QtHandle, IntPtr.Zero);
            }
        }

        private void UpdateFormStyles()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QWidget_SetFormProperties(QtHandle, _minimizeBox, _maximizeBox, _showInTaskbar, _showIcon, _topMost, (int)_formBorderStyle);
            }
        }

        public DialogResult DialogResult { get; set; }
        public Task<DialogResult> ShowDialogAsync(IWin32Window? owner = null)
        {
            this.Owner = owner as Form;

            CreateControl();

            NativeMethods.QWidget_SetWindowModality(QtHandle, 2);

            _isModal = true;
            this.Visible = true;
            var tcs = new TaskCompletionSource<DialogResult>();

            FormClosedEventHandler handler = null!;
            handler = (_, _) =>
            {
                this.FormClosed -= handler;
                _isModal = false;
                NativeMethods.QWidget_SetWindowModality(QtHandle, 1);
                tcs.TrySetResult(this.DialogResult);
            };
            this.FormClosed += handler;

            return tcs.Task;
        }

        internal bool _isModal;

        public DialogResult ShowDialog(IWin32Window? owner = null)
        {
            this.Owner = owner as Form;
            CreateControl();


            _isModal = true;
            try
            {
                NativeMethods.Form_ShowDialog(QtHandle);
            }
            finally
            {
                _isModal = false;
            }

            return DialogResult;
        }

        private Form? _owner;
        public Form? Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                if (IsHandleCreated)
                {
                    _owner?.CreateControl();
                    NativeMethods.Form_SetOwner(QtHandle, _owner?.QtHandle ?? default);
                    UpdateFormStyles();
                }
            }
        }
    }
}
