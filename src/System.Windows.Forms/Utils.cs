using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
    internal static class Utils
    {
        public static void EnsureSTAThread()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new ThreadStateException("The current thread must be set to Single Thread Apartment (STA) mode before OLE calls can be made. Ensure that your Main function has STAThreadAttribute marked on it.");
            }
        }
    }
}
