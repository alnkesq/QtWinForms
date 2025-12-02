using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class PaintEventArgs : EventArgs, IDisposable
    {
        public void Dispose()
        {
        }

        [Obsolete(Control.NotImplementedWarning)] public Graphics Graphics { get; set; }
    }
}
