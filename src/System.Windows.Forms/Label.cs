using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Label : Control
    {
        protected string _text = string.Empty;

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QLabel_Create(IntPtr.Zero, Text);
            SetCommonProperties();
        }
        protected override Size DefaultSize => new Size(100, 23);
        protected override Padding DefaultMargin => new Padding(3, 0, 3, 0);

        public override string Text
        {
            get => _text;
            set
            {
                _text = value ?? "";
                if (IsHandleCreated)
                {
                    UpdateNativeText();
                }
            }
        }

        protected virtual void UpdateNativeText()
        {
            NativeMethods.QLabel_SetText(QtHandle, _text);
        }

        [Obsolete(NotImplementedWarning)] public Image? Image { get; set; }
        [Obsolete(NotImplementedWarning)] public ContentAlignment ImageAlign { get; set; }
    }
}
