using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a button on a ToolStrip.
    /// </summary>
    public class ToolStripButton : ToolStripItem
    {
        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // Create a QAction for the button
                Handle = NativeMethods.QAction_Create(string.Empty);
                UpdateImage();
                UpdateTextAndTooltip();
                ConnectClickEvent();
            }
        }
        [Obsolete(NotImplementedWarning)] public bool CheckOnClick { get; set; }
        [Obsolete(NotImplementedWarning)] public bool Checked { get; set; }

        public event EventHandler? Click;

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QAction_ConnectTriggered(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var button = ObjectFromGCHandle<ToolStripButton>(userData);
            button.Click?.Invoke(button, EventArgs.Empty);
        }
    }
}

