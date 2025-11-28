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


    }
}
