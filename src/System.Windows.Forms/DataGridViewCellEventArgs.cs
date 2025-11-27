using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCellEventArgs : EventArgs, IDataGridViewCellEventArgs
    {
        public int ColumnIndex { get; }

        public int RowIndex { get; }

        internal DataGridViewCellEventArgs(DataGridViewCell dataGridViewCell)
            : this(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex)
        {
        }

        public DataGridViewCellEventArgs(int columnIndex, int rowIndex)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(columnIndex, -1, "columnIndex");
            ArgumentOutOfRangeException.ThrowIfLessThan(rowIndex, -1, "rowIndex");
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }
    }

}
