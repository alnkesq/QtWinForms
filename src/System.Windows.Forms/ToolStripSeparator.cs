using System;

namespace System.Windows.Forms
{
    public class ToolStripSeparator : ToolStripItem
    {
        internal override bool IsQWidgetCreated => false;
        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QAction_CreateSeparator();
        }
    }
}
