using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    internal interface IDataGridViewCellEventArgs
    {
        int ColumnIndex { get; }

        int RowIndex { get; }
    }

}
