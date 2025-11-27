using System;

namespace System.Windows.Forms
{
    public class ToolStripSeparator : ToolStripItem
    {
        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // Create a separator action
                QtHandle = NativeMethods.QAction_CreateSeparator();
            }
        }
    }
}
