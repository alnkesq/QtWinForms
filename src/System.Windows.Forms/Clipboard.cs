using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public static class Clipboard
    {
        public static void SetText(string text) => SetText(text, TextDataFormat.UnicodeText);
        public static void SetText(string text, TextDataFormat format)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            Application.InitializeQt();
            NativeMethods.QClipboard_SetText(text);
        }
        public static string GetText() => GetText(TextDataFormat.UnicodeText);

        public static unsafe string GetText(TextDataFormat format)
        {
            Application.InitializeQt();
            using var box = new GCHandle<string>(string.Empty);

            [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
            static void Callback(void* utf16, int length, void* userData)
            {
                var box = GCHandle<string?>.FromIntPtr((nint)userData);
                string s = Marshal.PtrToStringUni((nint)utf16, length);
                box.Target = s;
            }

            NativeMethods.QClipboard_GetText_Invoke(&Callback, GCHandle<string>.ToIntPtr(box));
            return box.Target;
        }

        public static void Clear()
        {
            Application.InitializeQt();
            NativeMethods.QClipboard_Clear();
        }
    }
}
