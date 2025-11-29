using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewRow : DataGridViewBand
    {
        public DataGridViewRow()
        {
            Cells = new DataGridViewCellCollection(this);
        }

        public DataGridViewCellCollection Cells { get; }

        [Obsolete(Control.NotImplementedWarning)] public int Height { get; set; }

        internal void ClearIndexes()
        {
            Index = -1;
            foreach (var cell in Cells)
            {
                cell.ClearIndexes();
            }
        }

        internal void UpdateCellsStructure()
        {
            while (Cells.Count > _owner!.ColumnCount)
            {
                Cells._cells.RemoveAt(Cells.Count - 1);
            }

            while (Cells.Count < _owner!.ColumnCount)
            {
                var cell = new DataGridViewCell
                {
                    _owner = _owner!,
                    _rowIndex = Index,
                    _columnIndex = Cells.Count - 1,
                };
                Cells._cells.Add(cell);
            }

        }
    }
}
