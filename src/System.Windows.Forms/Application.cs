using System;

namespace System.Windows.Forms
{
    public static class Application
    {
        private static IntPtr _app;

        public static void InitializeQt()
        {
            if (_app == IntPtr.Zero)
            {
                Console.WriteLine("Creating QApplication...");
                _app = NativeMethods.QApplication_Create();
                Console.WriteLine("QApplication created!");
            }
        }
        public static void Run(Form mainForm)
        {
            mainForm.Visible = true;
            Run();
        }

        public static void Run()
        {
            NativeMethods.QApplication_Run();
        }
    }
}
