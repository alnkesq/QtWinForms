using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewColumnCollection : IEnumerable<DataGridViewColumn>
    {
        internal DataGridView? _owner;
        internal List<DataGridViewColumn> _columns = new List<DataGridViewColumn>();

        public DataGridViewColumn this[int index]
        {
            get => _columns[index];
        }

        public int Count => _columns.Count;

        public void Add(DataGridViewColumn col)
        {
            col._owner = _owner;
            col._index = _columns.Count;
            _columns.Add(col);

            if (_owner != null)
            {
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetColumnCount(_owner.QtHandle, _columns.Count);
                    if (!string.IsNullOrEmpty(col.HeaderText))
                    {
                        NativeMethods.QTableWidget_SetColumnHeaderText(_owner.QtHandle, col._index, col.HeaderText);
                    }
                }
                
                for (int r = 0; r < _owner.Rows.Count; r++)
                {
                    var row = _owner.Rows[r];
                    var newCell = new DataGridViewCell
                    {
                        _owner = _owner,
                        _rowIndex = r,
                        _columnIndex = col._index
                    };
                    row.Cells._cells.Add(newCell);
                }
            }
        }

        public void Clear()
        {
            _columns.Clear();
            if (_owner != null)
            {
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetColumnCount(_owner.QtHandle, 0);
                }
                
                for (int r = 0; r < _owner.Rows.Count; r++)
                {
                    _owner.Rows[r].Cells._cells.Clear();
                }
            }
        }

        public IEnumerator<DataGridViewColumn> GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        public void RemoveAt(int index)
        {
            _columns.RemoveAt(index);
            
            for (int i = index; i < _columns.Count; i++)
            {
                _columns[i]._index = i;
            }

            if (_owner != null)
            {
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_RemoveColumn(_owner.QtHandle, index);
                }

                for (int r = 0; r < _owner.Rows.Count; r++)
                {
                    var row = _owner.Rows[r];
                    row.Cells._cells.RemoveAt(index);
                    
                    for (int c = index; c < row.Cells._cells.Count; c++)
                    {
                        row.Cells._cells[c]._columnIndex = c;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
