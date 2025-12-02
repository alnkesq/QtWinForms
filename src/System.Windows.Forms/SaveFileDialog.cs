using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class SaveFileDialog : FileDialog
    {
        [Obsolete(Control.NotImplementedWarning)] public bool OverwritePrompt { get; set; }

        public unsafe override DialogResult ShowDialog(IWin32Window? owner)
        {
            Utils.EnsureSTAThread();

            IntPtr ownerHandle = owner?.Handle ?? IntPtr.Zero;

            using var box = new GCHandle<string?>(null);

            [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
            static void Callback(void* utf16, int length, IntPtr userData)
            {
                var box = GCHandle<string?>.FromIntPtr(userData);
                string s = Marshal.PtrToStringUni((nint)utf16, length);
                box.Target = s;
            }

            string qtFilter = TranslateFilter(Filter);

            NativeMethods.QFileDialog_GetSaveFileName(
                ownerHandle,
                InitialDirectory ?? "",
                Title ?? "",
                qtFilter,
                &Callback,
                GCHandle<string?>.ToIntPtr(box)
            );

            string? result = box.Target;

            if (!string.IsNullOrEmpty(result))
            {
                FileName = Path.GetFullPath(result); // Normalize backlashes on Windows
                return DialogResult.OK;
            }

            return DialogResult.Cancel;
        }
    }
}
