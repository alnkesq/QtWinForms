using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCellValueEventArgs : EventArgs, IDataGridViewCellEventArgs
    {
        public int ColumnIndex { get; private set; }

        public int RowIndex { get; private set; }

        public object? Value { get; set; }

        internal DataGridViewCellValueEventArgs()
        {
            ColumnIndex = -1;
            RowIndex = -1;
        }


        public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(columnIndex, "columnIndex");
            ArgumentOutOfRangeException.ThrowIfNegative(rowIndex, "rowIndex");
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }

        internal void SetProperties(int columnIndex, int rowIndex, object? value)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            Value = value;
        }
    }

}
