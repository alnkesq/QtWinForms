using System.Collections;
using System.Linq;

namespace System.Windows.Forms
{
    public class DataGridViewCellCollection : IEnumerable<DataGridViewCell>
    {
        internal List<DataGridViewCell> _cells = new List<DataGridViewCell>();
        private DataGridViewRow _ownerRow;

        internal DataGridViewCellCollection(DataGridViewRow ownerRow)
        {
            this._ownerRow = ownerRow;
        }

        public int Count => _cells.Count;

        public IEnumerator<DataGridViewCell> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public DataGridViewCell this[int index]
        {
            get
            {
                return _cells[index];
            }
            set
            {
                _cells[index].ClearIndexes();
                value._rowIndex = this._ownerRow.Index;
                value._columnIndex = index;
                value._owner = Owner;
                _cells[index] = value;
            }
        }

        internal DataGridView? Owner => _ownerRow._owner;

        public DataGridViewCell this[string columnName]
        {
            get
            {
                return this[Owner!.GetColumnByName(columnName).Index];
            }
            set
            {
                this[Owner!.GetColumnByName(columnName).Index] = value;
            }
        }
    }
}
