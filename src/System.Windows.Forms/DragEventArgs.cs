using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Windows.Forms
{
    public class DragEventArgs : EventArgs
    {
        public DragDropEffects Effect { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public IDataObject? Data => null;
    }
}
