namespace System.Drawing
{
    public static class SystemFonts
    {
        public static Font DefaultFont { get; private set; } = new Font(string.Empty, 9, FontStyle.Regular);
    }
}
