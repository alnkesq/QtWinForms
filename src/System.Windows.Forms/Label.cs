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
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QLabel_Create(IntPtr.Zero, Text);
                SetCommonProperties();
            }
        }

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
            NativeMethods.QLabel_SetText(Handle, _text);
        }

        [Obsolete(NotImplementedWarning)] public Image? Image { get; set; }
    }
}
