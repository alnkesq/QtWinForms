using System;
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

            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            }
            _synchronizationContext = SynchronizationContext.Current;

            NativeMethods.QApplication_Run();
        }

        public static void SetMainThreadOrEnsureMainThread()
        {
            if (_mainThreadId == -1)
                _mainThreadId = Environment.CurrentManagedThreadId;
            else if (_mainThreadId != Environment.CurrentManagedThreadId)
                throw new InvalidOperationException("Qt already initialized on a different thread.");
        }
    }
}
