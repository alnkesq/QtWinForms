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

        public static Keys MapQtKeyToWinFormsKeys(int qtKey, int qtModifiers)
        {
            Keys keys = Keys.None;

            // Map Modifiers
            // Qt Modifiers: Shift=0x02000000, Control=0x04000000, Alt=0x08000000
            if ((qtModifiers & 0x02000000) != 0) keys |= Keys.Shift;
            if ((qtModifiers & 0x04000000) != 0) keys |= Keys.Control;
            if ((qtModifiers & 0x08000000) != 0) keys |= Keys.Alt;

            // Map Keys
            // ASCII A-Z (0x41 - 0x5A) map directly
            if (qtKey >= 0x41 && qtKey <= 0x5A)
            {
                keys |= (Keys)qtKey;
            }
            // 0-9 (0x30 - 0x39) map directly
            else if (qtKey >= 0x30 && qtKey <= 0x39)
            {
                keys |= (Keys)qtKey;
            }
            else
            {
                switch (qtKey)
                {
                    case 0x01000000: keys |= Keys.Escape; break;
                    case 0x01000001: keys |= Keys.Tab; break;
                    case 0x01000003: keys |= Keys.Back; break;
                    case 0x01000004: keys |= Keys.Return; break;
                    case 0x01000005: keys |= Keys.Enter; break;
                    case 0x01000006: keys |= Keys.Insert; break;
                    case 0x01000007: keys |= Keys.Delete; break;
                    case 0x01000008: keys |= Keys.Pause; break;
                    case 0x01000009: keys |= Keys.PrintScreen; break;
                    case 0x01000010: keys |= Keys.Home; break;
                    case 0x01000011: keys |= Keys.End; break;
                    case 0x01000012: keys |= Keys.Left; break;
                    case 0x01000013: keys |= Keys.Up; break;
                    case 0x01000014: keys |= Keys.Right; break;
                    case 0x01000015: keys |= Keys.Down; break;
                    case 0x01000016: keys |= Keys.PageUp; break;
                    case 0x01000017: keys |= Keys.PageDown; break;
                    case 0x01000020: keys |= Keys.ShiftKey; break;
                    case 0x01000021: keys |= Keys.ControlKey; break;
                    case 0x01000023: keys |= Keys.Menu; break; // Alt
                    case 0x01000024: keys |= Keys.CapsLock; break;
                    case 0x01000025: keys |= Keys.NumLock; break;
                    case 0x01000026: keys |= Keys.Scroll; break;
                    case 0x01000030: keys |= Keys.F1; break;
                    case 0x01000031: keys |= Keys.F2; break;
                    case 0x01000032: keys |= Keys.F3; break;
                    case 0x01000033: keys |= Keys.F4; break;
                    case 0x01000034: keys |= Keys.F5; break;
                    case 0x01000035: keys |= Keys.F6; break;
                    case 0x01000036: keys |= Keys.F7; break;
                    case 0x01000037: keys |= Keys.F8; break;
                    case 0x01000038: keys |= Keys.F9; break;
                    case 0x01000039: keys |= Keys.F10; break;
                    case 0x0100003a: keys |= Keys.F11; break;
                    case 0x0100003b: keys |= Keys.F12; break;
                    case 0x20: keys |= Keys.Space; break;
                }
            }
            return keys;
        }
    }
}
