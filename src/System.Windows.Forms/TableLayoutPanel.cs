using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class TableLayoutPanel : Control
    {
        public TableLayoutRowStyleCollection RowStyles => throw new NotImplementedException();
        public TableLayoutColumnStyleCollection ColumnStyles => throw new NotImplementedException();

        public void SetColumn(Control control, int column) => throw new NotImplementedException();
        public void SetColumnSpan(Control control, int value) => throw new NotImplementedException();

        public void SetRow(Control control, int row) => throw new NotImplementedException();
        public void SetRowSpan(Control control, int value) => throw new NotImplementedException();

        public void SetCellPosition(Control control, TableLayoutPanelCellPosition position) => throw new NotImplementedException();

        [Obsolete(NotImplementedWarning)] public int RowCount { get; set; }
        [Obsolete(NotImplementedWarning)] public int ColumnCount { get; set; }

    }
}
