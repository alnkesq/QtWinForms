using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCellFormattingEventArgs : ConvertEventArgs, IDataGridViewCellEventArgs
    {
        public DataGridViewCellStyle CellStyle { get; set; }

        public int ColumnIndex { get; }

        public bool FormattingApplied { get; set; }

        public int RowIndex { get; }

          public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object? value, Type? desiredType, DataGridViewCellStyle cellStyle)
            : base(value, desiredType)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(columnIndex, -1, "columnIndex");
            ArgumentOutOfRangeException.ThrowIfLessThan(rowIndex, -1, "rowIndex");
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            CellStyle = cellStyle;
        }
    }

}
