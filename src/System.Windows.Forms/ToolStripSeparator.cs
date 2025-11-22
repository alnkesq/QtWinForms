using System;

namespace System.Windows.Forms
{
    public class ToolStripSeparator : Control
    {
        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // Create a separator action
                Handle = NativeMethods.QAction_CreateSeparator();
            }
        }
    }
}
