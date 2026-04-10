using System.Drawing;

namespace System.Windows.Forms
{
    public class RichTextBox : TextBox
    {
        [Obsolete(NotImplementedWarning)] public bool ReadOnly { get; set; }

        protected override Size DefaultSize => new Size(100, 96);
    }
}
