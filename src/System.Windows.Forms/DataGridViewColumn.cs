using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewColumn : DataGridViewBand
    {
        internal int _index;
        
        private string _headerText = "";
        public string? HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value ?? "";
                if (_owner != null && _owner.IsHandleCreated)
                {
                    NativeMethods.QTableWidget_SetColumnHeaderText(_owner.QtHandle, _index, _headerText);
                }
            }
        }

        [Obsolete(Control.NotImplementedWarning)] public string? ToolTipText { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public int FillWeight { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public int Width { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCell? CellTemplate { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCell? HeaderCell { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public DataGridViewCellStyle? DefaultCellStyle { get; set; }
        public string? Name { get; set; }
    }
}
