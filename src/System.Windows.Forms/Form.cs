using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Form : Control
    {
        private string _text = string.Empty;
        private FormWindowState _windowState = FormWindowState.Normal;

        public Form() : base()
        {
            Size = new Size(800, 600);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            NativeMethods.QWidget_Resize(Handle, Size.Width, Size.Height);
            NativeMethods.QWidget_SetTitle(Handle, _text);
            if (_windowState != FormWindowState.Normal)
            {
                NativeMethods.QWidget_SetWindowState(Handle, (int)_windowState);
            }
            
            // Connect resize and move events
            ConnectResizeEvent();
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
    }
}
