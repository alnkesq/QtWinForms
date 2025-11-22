using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TextBox : Control
    {
        private string _text = string.Empty;
        private bool _multiline;

        public bool Multiline
        {
            get => _multiline;
            set
            {
                if (_multiline != value)
                {
                    if (IsHandleCreated) throw new NotSupportedException("Multiline cannot be toggled after the control is created.");
                    _multiline = value;
                }
            }
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                if (_multiline)
                {
                    Handle = NativeMethods.QPlainTextEdit_Create(IntPtr.Zero, _text);
                }
                else
                {
                    Handle = NativeMethods.QLineEdit_Create(IntPtr.Zero, _text);
                }
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

                if (_multiline)
                {
                    NativeMethods.QPlainTextEdit_GetText_Invoke(Handle, &Callback, GCHandle<string>.ToIntPtr(box));
                }
                else
                {
                    NativeMethods.QLineEdit_GetText_Invoke(Handle, &Callback, GCHandle<string>.ToIntPtr(box));
                }
                return box.Target;
            }
            set
            {
                _text = value ?? "";
                if (IsHandleCreated)
                {
                    if (_multiline)
                    {
                        NativeMethods.QPlainTextEdit_SetText(Handle, _text);
                    }
                    else
                    {
                        NativeMethods.QLineEdit_SetText(Handle, _text);
                    }
                }
            }
        }
    }
}
