using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Label : Control
    {
        private string _text = string.Empty;

        protected override void CreateHandle()
        {
            if (Handle == IntPtr.Zero)
            {
                Handle = NativeMethods.QLabel_Create(IntPtr.Zero, Text);
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
                    NativeMethods.QLabel_SetText(Handle, _text);
                }
            }
        }
    }
}
