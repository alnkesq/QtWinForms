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

        private object? _value;
        public object? Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_owner != null && _owner.IsHandleCreated)
                {
                    ApplyValue();
                }
            }
        }

        internal void ApplyValue()
        {
            NativeMethods.QTableWidget_SetCellText(_owner!.QtHandle, _rowIndex, _columnIndex, ValueToString(_value));
        }

        [Obsolete(Control.NotImplementedWarning)] public bool Selected { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCellStyle? Style { get; set; } = new();

        public override string? ToString()
        {
            return Value?.ToString();
        }

        internal void ClearIndexes()
        {
            _rowIndex = -1;
            _columnIndex = -1;
            _owner = null;
        }

        internal static string ValueToString(object? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}
