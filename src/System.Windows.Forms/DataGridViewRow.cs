using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewRow
    {
        public object? Tag { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public int Height { get; set; }
    }
}
