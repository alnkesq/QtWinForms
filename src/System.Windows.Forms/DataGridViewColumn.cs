using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewColumn
    {
        [Obsolete(Control.NotImplementedWarning)] public string? ToolTipText { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public int FillWeight { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public int Width { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public object? Tag { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public string? HeaderText { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCell? CellTemplate { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCell? HeaderCell { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCellStyle? DefaultCellStyle { get; set; }
    }
}
