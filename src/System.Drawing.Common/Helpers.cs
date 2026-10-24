using SkiaSharp;

namespace System.Drawing
{
    internal static class Helpers
    {
        public static SKColor SkiaColorFromDrawingColor(Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }
        public static Color DrawingColorFromSkiaColor(SKColor color)
        {
            return Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static byte[] StreamToArray(Stream stream)
        {
            if (stream is MemoryStream ms)
            {
                return ms.ToArray();
            }
            else if (stream is UnmanagedMemoryStream ums)
            {
                unsafe
                {
                    return new ReadOnlySpan<byte>(ums.PositionPointer, (int)ums.Length).ToArray();
                }
            }
            else
            {
                using var ms2 = new MemoryStream();
                stream.CopyTo(ms2);
                return ms2.ToArray();
            }
        }


    }
}
