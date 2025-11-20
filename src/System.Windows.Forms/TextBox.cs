using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TextBox : Control
    {
        private string _text = string.Empty;

        protected override void CreateHandle()
        {
            if (Handle == IntPtr.Zero)
            {
                Handle = NativeMethods.QLineEdit_Create(IntPtr.Zero, Text);
                SetCommonProperties();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value ?? "";
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QLineEdit_SetText(Handle, _text);
                }
            }
        }
    }
}
