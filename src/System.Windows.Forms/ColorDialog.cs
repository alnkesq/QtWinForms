using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class ColorDialog : CommonDialog
    {
        public Color Color { get; set; } = Color.Black;
        public bool AllowFullOpen { get; set; } = true;
        public bool AnyColor { get; set; } = false;
        public bool FullOpen { get; set; } = false;
        public bool SolidColorOnly { get; set; } = false;
        public bool ShowHelp { get; set; } = false;
        public int[] CustomColors { get; set; } = new int[16];

        public override void Reset()
        {
            Color = Color.Black;
            AllowFullOpen = true;
            AnyColor = false;
            FullOpen = false;
            SolidColorOnly = false;
            ShowHelp = false;
            CustomColors = new int[16];
        }

        public override unsafe DialogResult ShowDialog(IWin32Window? owner)
        {
            Utils.EnsureSTAThread();

            IntPtr ownerHandle = owner?.Handle ?? IntPtr.Zero;

            // Convert Color to ARGB int
            int initialColor = Color.ToArgb();

            // Call the native method
            int resultColor = NativeMethods.QColorDialog_GetColor(
                ownerHandle,
                initialColor,
                AllowFullOpen
            );

            // Check if user cancelled (Qt returns -1 for cancelled dialog)
            if (resultColor == -1)
            {
                return DialogResult.Cancel;
            }

            // Convert ARGB int back to Color
            Color = Color.FromArgb(resultColor);
            return DialogResult.OK;
        }
    }
}
