using System;
using System.Drawing;

namespace System.Windows.Forms
{
    public class ContextMenuStrip : ToolStripDropDownMenu
    {
        public ContextMenuStrip()
        {
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

        public void Show(Point screenPos)
        {
            EnsureCreated();
            NativeMethods.QMenu_Popup(Handle, screenPos.X, screenPos.Y);
        }
    }
}
