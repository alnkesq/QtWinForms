using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ToolStripMenuItem : Control
    {
        private EventHandler? _clickHandler;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QAction_Create(Text);
                
                // Connect click event if handler is already attached
                if (_clickHandler != null)
                {
                    ConnectClickEvent();
                }
            }
        }

        public new string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QAction_SetText(Handle, _text);
                }
            }
        }
        private string _text = string.Empty;

        public event EventHandler Click
        {
            add
            {
                if (_clickHandler == null && IsHandleCreated)
                {
                    ConnectClickEvent();
                }
                _clickHandler += value;
            }
            remove
            {
                _clickHandler -= value;
            }
        }

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QAction_ConnectTriggered(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var menuItem = ObjectFromGCHandle<ToolStripMenuItem>(userData);
            menuItem._clickHandler?.Invoke(menuItem, EventArgs.Empty);
        }
    }
}
