using System;

namespace System.Windows.Forms
{
    public class Control : IDisposable
    {
        internal IntPtr Handle { get; private set; }

        public Control()
        {
            // Widget creation deferred until EnsureCreated() is called
        }

        internal void EnsureCreated()
        {
            if (Handle == IntPtr.Zero)
            {
                Console.WriteLine("Creating QWidget...");
                Handle = NativeMethods.QWidget_Create(IntPtr.Zero);
                Console.WriteLine("QWidget created!");
            }
        }

        public string Text
        {
            set
            {
                EnsureCreated();
                NativeMethods.QWidget_SetTitle(Handle, value);
            }
        }

        public bool Visible
        {
            set
            {
                if (value)
                {
                    EnsureCreated();
                    NativeMethods.QWidget_Show(Handle);
                }
            }
        }

        public void Dispose()
        {
            // TODO: Implement proper disposal
        }
    }
}
