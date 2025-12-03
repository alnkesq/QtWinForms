using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class RichTextBox : TextBox
    {
        [Obsolete(NotImplementedWarning)] public bool ReadOnly { get; set; }

        protected override Size DefaultSize => new Size(100, 96);
    }
}
