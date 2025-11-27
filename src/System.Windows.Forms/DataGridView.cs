using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridView : Control
    {
        [Obsolete(NotImplementedWarning)] public DataGridViewRow RowTemplate { get; set; } = new();
        [Obsolete(NotImplementedWarning)] public DataGridViewSelectedCellCollection SelectedCells => throw new NotImplementedException();
        [Obsolete(NotImplementedWarning)] public DataGridViewColumnCollection Columns => throw new NotImplementedException();
        [Obsolete(NotImplementedWarning)] public DataGridViewRowCollection Rows => throw new NotImplementedException();

        [Obsolete(NotImplementedWarning)] public event EventHandler? SelectionChanged;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellContentClick;
        [Obsolete(NotImplementedWarning)] public event DataGridViewCellEventHandler? CellDoubleClick;
        [Obsolete(NotImplementedWarning)] public DataGridViewCell? CurrentCell { get; set; }
        [Obsolete(NotImplementedWarning)] public bool VirtualMode { get; set; }
        [Obsolete(NotImplementedWarning)] public int RowCount { get; set; }
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
    }
}
