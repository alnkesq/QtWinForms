using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public enum CloseReason
    {
        None,
        WindowsShutDown,
        MdiFormClosing,
        UserClosing,
        TaskManagerClosing,
        FormOwnerClosing,
        ApplicationExitCall
    }

}
