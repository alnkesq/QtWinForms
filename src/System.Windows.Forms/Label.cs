using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Label : Control
    {
        private string _text = string.Empty;

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
                    NativeMethods.QLabel_SetText(Handle, _text);
                }
            }
        }
    }
}
