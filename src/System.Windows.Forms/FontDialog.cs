using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class FontDialog : CommonDialog
    {
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public Color Color { get; set; } = Color.Black;
        public bool ShowColor { get; set; } = false;
        public bool ShowEffects { get; set; } = true;
        public bool ShowApply { get; set; } = false;
        public bool ShowHelp { get; set; } = false;
        public int MaxSize { get; set; } = 0;
        public int MinSize { get; set; } = 0;

        // Event for Apply button (not fully supported by Qt's static method, but we add the event for API compatibility)
        public event EventHandler? Apply;

        public override void Reset()
        {
            Font = SystemFonts.DefaultFont;
            Color = Color.Black;
            ShowColor = false;
            ShowEffects = true;
            ShowApply = false;
            ShowHelp = false;
            MaxSize = 0;
            MinSize = 0;
        }

        public override unsafe DialogResult ShowDialog(IWin32Window? owner)
        {
            Utils.EnsureSTAThread();

            IntPtr ownerHandle = owner?.Handle ?? IntPtr.Zero;

            // Prepare buffers for output
            // Font family name max length
            const int MaxFamilyLen = 256;
            IntPtr familyBuffer = Marshal.AllocHGlobal(MaxFamilyLen);

            float size = Font.SizeInPoints;
            bool bold = Font.Bold;
            bool italic = Font.Italic;
            bool underline = Font.Underline;
            bool strikeout = Font.Strikeout;

            try
            {
                bool result = NativeMethods.QFontDialog_GetFont(
                    ownerHandle,
                    Font.FontFamily.Name,
                    Font.SizeInPoints,
                    Font.Bold,
                    Font.Italic,
                    Font.Underline,
                    Font.Strikeout,
                    familyBuffer,
                    MaxFamilyLen,
                    ref size,
                    ref bold,
                    ref italic,
                    ref underline,
                    ref strikeout
                );

                if (result)
                {
                    string familyName = Marshal.PtrToStringAnsi(familyBuffer) ?? "Arial";
                    FontStyle style = FontStyle.Regular;
                    if (bold) style |= FontStyle.Bold;
                    if (italic) style |= FontStyle.Italic;
                    if (underline) style |= FontStyle.Underline;
                    if (strikeout) style |= FontStyle.Strikeout;

                    Font = new Font(familyName, size, style);
                    return DialogResult.OK;
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(familyBuffer);
            }
        }
    }
}
