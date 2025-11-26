using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ToolStripDropDown : ToolStrip
    {
        public ToolStripDropDown()
        {
        }

        public event CancelEventHandler? Opening;

        protected virtual void OnOpening(CancelEventArgs e)
        {
            Opening?.Invoke(this, e);
        }

        protected override IntPtr CreateNativeControlCore()
        {
            return NativeMethods.QMenu_Create(string.Empty);
        }

        protected override void AddNativeItem(ToolStripItem item)
        {
            NativeMethods.QMenu_AddAction(Handle, item.Handle);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            if (IsHandleCreated)
            {
                ConnectAboutToShow();
            }
        }

        private unsafe void ConnectAboutToShow()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnAboutToShowCallback;
            NativeMethods.QMenu_ConnectAboutToShow(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnAboutToShowCallback(nint userData)
        {
            var control = ObjectFromGCHandle<ToolStripDropDown>(userData);
            control.OnAboutToShow();
        }

        private void OnAboutToShow()
        {
            var e = new CancelEventArgs();
            OnOpening(e);
            // Note: QMenu::aboutToShow doesn't support cancellation in the same way as WinForms Opening
            // But we can use it to populate the menu or change visibility of items
            // If e.Cancel is true, we might want to close the menu immediately, but it's already showing.
            // For ContextMenuStrip.Show(), we handle cancellation before showing.
            // For submenus, Qt handles showing. If we want to cancel, we might need to hide it.
            if (e.Cancel)
            {
                NativeMethods.QWidget_Hide(Handle);
            }
        }
    }
}
