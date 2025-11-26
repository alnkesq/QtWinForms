using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
    public static class Application
    {
        private static IntPtr _app;
        internal static int _mainThreadId = -1;
        internal static SynchronizationContext? _synchronizationContext;

        public static void InitializeQt()
        {
            if (_app == IntPtr.Zero)
            {
                SetMainThreadOrEnsureMainThread();
                _app = NativeMethods.QApplication_Create();
            }
        }
        public static void Run(Form mainForm)
        {
            SetMainThreadOrEnsureMainThread();
            mainForm.Visible = true;
            Run();
        }

        public static void Run()
        {
            SetMainThreadOrEnsureMainThread();
            NativeMethods.QApplication_Run();
        }

        public static void SetMainThreadOrEnsureMainThread()
        {
            if (_mainThreadId == -1)
            {
                if (SynchronizationContext.Current == null)
                {
                    SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                }
                _synchronizationContext = SynchronizationContext.Current;
                _mainThreadId = Environment.CurrentManagedThreadId;

                var path = Environment.GetEnvironmentVariable("QTWINFORMS_DLL_PATH") ?? qtWinFormsNativeDirectory;
                if (path != null)
                {
                    NativeLibrary.SetDllImportResolver(typeof(System.Windows.Forms.NativeMethods).Assembly, (a, b, c) =>
                    {
                        if (a == NativeMethods.LibName)
                        {
                            var winFormsDll = typeof(System.Windows.Forms.Control).Assembly.Location;
                            if (File.Exists(path)) return NativeLibrary.Load(path);
                            if (OperatingSystem.IsWindows()) return NativeLibrary.Load(Path.Combine(path, NativeMethods.LibName + ".dll"));
                            if (OperatingSystem.IsLinux()) return NativeLibrary.Load(Path.Combine(path, "lib" + NativeMethods.LibName + ".so"));
                            if (OperatingSystem.IsMacOS()) return NativeLibrary.Load(Path.Combine(path, "lib" + NativeMethods.LibName + ".dylib"));
                        }
                        return default;

                    });
                }
            }
            else if (_mainThreadId != Environment.CurrentManagedThreadId)
                throw new InvalidOperationException("Qt already initialized on a different thread.");
        }


        private static string? qtWinFormsNativeDirectory;

        public static void SetQtWinFormsNativeDirectory(string directoryRelativeToWinFormsDll)
        {
            qtWinFormsNativeDirectory = directoryRelativeToWinFormsDll;
        }
    }
}
