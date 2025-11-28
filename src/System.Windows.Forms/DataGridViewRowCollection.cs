using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewRowCollection : IEnumerable<DataGridViewRow>
    {
        internal DataGridView? _owner;
        internal List<DataGridViewRow> _rows = new List<DataGridViewRow>();

        public DataGridViewRow this[int index]
        {
            get => _rows[index];
        }

        public int Count => _rows.Count;

        public int Add()
        {
            var row = new DataGridViewRow();
            row._owner = _owner;
            row.Index = _rows.Count;
            _rows.Add(row);

            if (_owner != null)
            {
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetRowCount(_owner.QtHandle, _rows.Count);
                }
                
                for (int c = 0; c < _owner.Columns.Count; c++)
                {
                    var cell = new DataGridViewCell
                    {
                        _owner = _owner,
                        _rowIndex = row.Index,
                        _columnIndex = c
                    };
                    row.Cells._cells.Add(cell);
                }
            }

            return _rows.Count - 1;
        }

        public void RemoveAt(int index)
        {
            _rows[index].ClearIndexes();
            _rows.RemoveAt(index);
            
            for (int i = index; i < _rows.Count; i++)
            {
                _rows[i].Index = i;
                
                for (int c = 0; c < _rows[i].Cells._cells.Count; c++)
                {
                    _rows[i].Cells._cells[c]._rowIndex = i;
                }
            }

            if (_owner != null && _owner.IsHandleCreated)
            {
                NativeMethods.QTableWidget_RemoveRow(_owner.QtHandle, index);
            }
        }

        public void Clear()
        {
            foreach (var row in _rows)
            {
                row.ClearIndexes();
            }
            _rows.Clear();
            if (_owner != null && _owner.IsHandleCreated)
            {
                NativeMethods.QTableWidget_SetRowCount(_owner.QtHandle, 0);
            }
        }

        public IEnumerator<DataGridViewRow> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
