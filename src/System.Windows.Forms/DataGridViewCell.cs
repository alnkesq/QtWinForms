using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCell
    {
        internal DataGridView? _owner;
        internal int _rowIndex;
        internal int _columnIndex;

        public int RowIndex => _rowIndex;
        public int ColumnIndex => _columnIndex;
        
        public DataGridViewColumn? OwningColumn => _owner?.Columns[_columnIndex];
        public DataGridViewRow? OwningRow => _owner?.Rows[_rowIndex];
        
        public object? Tag { get; set; }

        private string _text = "";
        public string Text
        {
            get => _text;
            set
            {
                _text = value ?? "";
                if (_owner != null && _owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetCellText(_owner.QtHandle, _rowIndex, _columnIndex, _text);
                }
            }
        }

        [Obsolete(Control.NotImplementedWarning)] public bool Selected { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCellStyle? Style { get; set; }

        public override string ToString()
        {
            return Text;
        }

        internal void ClearIndexes()
        {
            _rowIndex = -1;
            _columnIndex = -1;
            _owner = null;
        }
    }
}
