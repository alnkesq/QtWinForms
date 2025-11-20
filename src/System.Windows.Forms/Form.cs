using System.Drawing;

namespace System.Windows.Forms
{
    public class Form : Control
    {
        private string _text = string.Empty;

        public Form() : base()
        {
            Size = new Size(800, 600);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            NativeMethods.QWidget_Resize(Handle, Size.Width, Size.Height);
            NativeMethods.QWidget_SetTitle(Handle, _text);
        }

        public Size ClientSize
        {
            get => Size;
            set => Size = value;
        }
        public override string Text 
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QWidget_SetTitle(Handle, _text);
                }
            }
        }
    }
}
