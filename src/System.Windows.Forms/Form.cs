using System.Drawing;

namespace System.Windows.Forms
{
    public class Form : Control
    {
        public Form() : base()
        {
            Size = new Size(800, 600);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            NativeMethods.QWidget_Resize(Handle, Size.Width, Size.Height);
        }
    }
}
