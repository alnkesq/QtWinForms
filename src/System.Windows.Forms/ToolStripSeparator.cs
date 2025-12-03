using System;
using System.Drawing;

namespace System.Windows.Forms
{
    public class ToolStripSeparator : ToolStripItem
    {
        internal override bool IsQWidgetCreated => false;
        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QAction_CreateSeparator();
        }

        protected override Size DefaultSize => new Size(6, 6);
        protected override Padding DefaultMargin => Padding.Empty;
    }
}
