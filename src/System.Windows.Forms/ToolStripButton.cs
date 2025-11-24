using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a button on a ToolStrip. Text-only, no icon handling for now.
    /// </summary>
    public class ToolStripButton : ToolStripItem
    {
        private string _text = string.Empty;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // Create a QAction for the button
                Handle = NativeMethods.QAction_Create(_text);
                ConnectClickEvent();
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

        public event EventHandler? Click;

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QAction_ConnectTriggered(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var button = ObjectFromGCHandle<ToolStripButton>(userData);
            button.Click?.Invoke(button, EventArgs.Empty);
        }
    }
}
