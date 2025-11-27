using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class DataGridViewCellStyle
    {
        public DataGridViewTriState WrapMode { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Font? Font { get; set; }
        public Color SelectionBackColor { get; set; }
        public Color SelectionForeColor { get; set; }
        public DataGridViewContentAlignment Alignment { get; set; }
    }
}
