using System;
using System.Runtime.CompilerServices;
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

        public unsafe override string Text
        {
            get
            {
                if (!IsHandleCreated) return _text;
                using var box = new GCHandle<string>(string.Empty);

                [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
                static void Callback(void* utf16, int length, void* userData)
                {
                    var box = GCHandle<string?>.FromIntPtr((nint)userData);
                    string s = Marshal.PtrToStringUni((nint)utf16, length);
                    box.Target = s;
                }
                NativeMethods.QLineEdit_GetText_Invoke(Handle, &Callback, GCHandle<string>.ToIntPtr(box));
                return box.Target;
            }
            set
            {
                _text = value ?? "";
                if (IsHandleCreated)
                {
                    NativeMethods.QLineEdit_SetText(Handle, _text);
                }
            }
        }
    }
}
