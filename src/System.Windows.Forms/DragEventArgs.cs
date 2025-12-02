using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DragEventArgs : EventArgs
    {
        public DragDropEffects Effect { get; set; }
    }
}
