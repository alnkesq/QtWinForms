using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class RichTextBox : TextBox
    {
        [Obsolete(NotImplementedWarning)] public bool ReadOnly { get; set; }
    }
}
