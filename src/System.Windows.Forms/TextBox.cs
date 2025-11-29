using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TextBox : Control
    {
        private string _text = string.Empty;
        private bool _multiline;

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void OnTextChangedCallback(IntPtr userData)
        {
            var control = ObjectFromGCHandle<TextBox>(userData);
            control.OnTextChanged(EventArgs.Empty);
        }

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

        private bool _useSystemPasswordChar;

        public bool UseSystemPasswordChar
        {
            get => _useSystemPasswordChar;
            set
            {
                if (_useSystemPasswordChar != value)
                {
                    _useSystemPasswordChar = value;
                    if (IsHandleCreated && !_multiline)
                    {
                        NativeMethods.QLineEdit_SetEchoMode(QtHandle, value ? 2 : 0);
                    }
                }
            }
        }


        [Obsolete(NotImplementedWarning)] public void SelectAll() { }

        protected override void CreateHandle()
        {
            unsafe
            {
                delegate* unmanaged[Cdecl]<IntPtr, void> callback = &OnTextChangedCallback;
                if (_multiline)
                {
                    QtHandle = NativeMethods.QPlainTextEdit_Create(IntPtr.Zero, _text);
                    NativeMethods.QPlainTextEdit_ConnectTextChanged(QtHandle, (IntPtr)callback, GCHandlePtr);
                }
                else
                {
                    QtHandle = NativeMethods.QLineEdit_Create(IntPtr.Zero, _text);
                    if (_useSystemPasswordChar)
                    {
                        NativeMethods.QLineEdit_SetEchoMode(QtHandle, 2);
                    }
                    NativeMethods.QLineEdit_ConnectTextChanged(QtHandle, (IntPtr)callback, GCHandlePtr);
                }
            }
            SetCommonProperties();
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
                    NativeMethods.QPlainTextEdit_GetText_Invoke(QtHandle, &Callback, GCHandle<string>.ToIntPtr(box));
                }
                else
                {
                    NativeMethods.QLineEdit_GetText_Invoke(QtHandle, &Callback, GCHandle<string>.ToIntPtr(box));
                }
                return box.Target;
            }
            set
            {
                if (_text != value)
                {
                    _text = value ?? "";
                    if (IsHandleCreated)
                    {
                        if (_multiline)
                        {
                            NativeMethods.QPlainTextEdit_SetText(QtHandle, _text);
                        }
                        else
                        {
                            NativeMethods.QLineEdit_SetText(QtHandle, _text);
                        }
                    }
                    else
                    {
                        OnTextChanged(EventArgs.Empty);
                    }
                }
            }
        }
    }
}
