using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridView : Control
    {
        private DataGridViewColumnCollection _columns;
        private DataGridViewRowCollection _rows;

        public DataGridView()
        {
            _columns = new DataGridViewColumnCollection { _owner = this };
            _rows = new DataGridViewRowCollection { _owner = this };
        }

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QTableWidget_Create(IntPtr.Zero);
            SetCommonProperties();

            if (_columns.Count > 0)
            {
                NativeMethods.QTableWidget_SetColumnCount(QtHandle, _columns.Count);
                for (int i = 0; i < _columns.Count; i++)
                {
                    var col = _columns[i];
                    if (!string.IsNullOrEmpty(col.HeaderText))
                    {
                        NativeMethods.QTableWidget_SetColumnHeaderText(QtHandle, i, col.HeaderText);
                    }
                }
            }

            if (_rows.Count > 0)
            {
                NativeMethods.QTableWidget_SetRowCount(QtHandle, _rows.Count);
            }

            UpdateCellsStructure();

            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    NativeMethods.QTableWidget_SetCellText(QtHandle, cell.RowIndex, cell.ColumnIndex, cell.Text);
                }
            }
        }

        internal void UpdateCellsStructure()
        {
            // Create initial cells structure for all rows based on current columns
            // This is only called during CreateHandle
            for (int r = 0; r < _rows.Count; r++)
            {
                var row = _rows[r];

                while (row.Cells.Count > ColumnCount)
                {
                    row.Cells._cells.RemoveAt(row.Cells.Count - 1);
                }
                

                
                for (int c = row.Cells.Count; c < ColumnCount; c++)
                {
                    var cell = new DataGridViewCell
                    {
                        _owner = this,
                        _rowIndex = r,
                        _columnIndex = c
                    };
                    row.Cells._cells.Add(cell);
                }
            }
        }

        public DataGridViewColumnCollection Columns => _columns;
        public DataGridViewRowCollection Rows => _rows;

        [Obsolete(NotImplementedWarning)] public DataGridViewRow RowTemplate { get; set; } = new();
        [Obsolete(NotImplementedWarning)] public DataGridViewSelectedCellCollection SelectedCells => throw new NotImplementedException();
        [Obsolete(NotImplementedWarning)] public event EventHandler? SelectionChanged;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellContentClick;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellDoubleClick;
        [Obsolete(NotImplementedWarning)] public DataGridViewCell? CurrentCell { get; set; }
        [Obsolete(NotImplementedWarning)] public bool VirtualMode { get; set; }
        public int RowCount => Rows.Count;
        public int ColumnCount => Columns.Count;
        [Obsolete(NotImplementedWarning)] public bool AllowUserToAddRows { get; set; }
        [Obsolete(NotImplementedWarning)] public bool AllowUserToDeleteRows { get; set; }
        [Obsolete(NotImplementedWarning)] public Color BackgroundColor { get; set; }
        [Obsolete(NotImplementedWarning)] public BorderStyle BorderStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public bool MultiSelect { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewCellStyle? DefaultCellStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewCellStyle? ColumnHeadersDefaultCellStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public bool ReadOnly { get; set; }
        [Obsolete(NotImplementedWarning)] public bool StandardTab { get; set; }
        [Obsolete(NotImplementedWarning)] public int RowHeadersWidth { get; set; }
        [Obsolete(NotImplementedWarning)] public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }

        public void ClearSelection()
        {
            throw new NotImplementedException();
        }

        internal DataGridViewColumn GetColumnByName(string name) => Columns.Single(x => x.Name == name);

        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {
                return Rows[rowIndex].Cells[columnIndex];
            }
            set
            {
                Rows[rowIndex].Cells[columnIndex] = value;
            }
        }

        public DataGridViewCell this[string columnName, int rowIndex]
        {
            get
            {
                return Rows[rowIndex].Cells[columnName];
            }
            set
            {
                Rows[rowIndex].Cells[columnName] = value;
            }
        }

    }
}
