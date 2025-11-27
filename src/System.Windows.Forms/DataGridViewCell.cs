using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCell
    {
        public int RowIndex => throw new NotImplementedException();
        public int ColumnIndex => throw new NotImplementedException();
        public DataGridViewColumn OwningColumn => throw new NotImplementedException();
        public DataGridViewRow OwningRow => throw new NotImplementedException();
        public object? Tag { get; set; }

        [Obsolete(Control.NotImplementedWarning)] public bool Selected { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCellStyle Style { get; set; }
    }
}
