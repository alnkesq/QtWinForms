using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public static class SystemInformation
    {
        [Obsolete(Control.NotImplementedWarning)] public static bool TerminalServerSession => false;
    }
}
