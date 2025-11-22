using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class FolderBrowserDialog : CommonDialog
    {
        public string SelectedPath { get; set; } = string.Empty;
        public string InitialDirectory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool ShowNewFolderButton { get; set; } = true;

        public override void Reset()
        {
            SelectedPath = string.Empty;
            InitialDirectory = string.Empty;
            Description = string.Empty;
            ShowNewFolderButton = true;
        }
        
        public override unsafe DialogResult ShowDialog(IWin32Window? owner)
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

             NativeMethods.QFileDialog_GetExistingDirectory(
                 ownerHandle, 
                 InitialDirectory ?? "", 
                 Description ?? "", 
                 ShowNewFolderButton, 
                 &Callback, 
                 GCHandle<string?>.ToIntPtr(box)
             );

             string? result = box.Target;

             if (!string.IsNullOrEmpty(result))
             {
                 SelectedPath = Path.GetFullPath(result); // normalizes backlashes on Windows
                 return DialogResult.OK;
             }
             
             return DialogResult.Cancel;
        }
    }
}
