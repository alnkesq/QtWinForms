using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
    public class ContextMenuStrip : ToolStripDropDownMenu
    {
        public ContextMenuStrip()
        {
        }

        public ContextMenuStrip(IContainer container)
        {
            container.Add(this);
        }
        protected override IntPtr CreateNativeControlCore()
        {
            return NativeMethods.QMenu_Create(string.Empty);
        }

        protected override void AddNativeItem(ToolStripItem item)
        {
            NativeMethods.QMenu_AddAction(Handle, item.Handle);
        }

        public void Show(Control control, Point position)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            EnsureCreated();

            // Map client point to screen point
            Point screenPoint = control.PointToScreen(position);
            Show(screenPoint);
        }

        public event CancelEventHandler? Opening;

        protected virtual void OnOpening(CancelEventArgs e)
        {
            Opening?.Invoke(this, e);
        }

        public void Show(Point screenPos)
        {
            var e = new CancelEventArgs();
            OnOpening(e);
            if (e.Cancel)
            {
                return;
            }

            EnsureCreated();
            NativeMethods.QMenu_Popup(Handle, screenPos.X, screenPos.Y);
        }
    }
}
