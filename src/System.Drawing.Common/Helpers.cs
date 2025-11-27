using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    internal static class Helpers
    {
        public static SixLabors.ImageSharp.Color ImageSharpColorFromDrawingColor(Color color)
        {
            return SixLabors.ImageSharp.Color.FromRgba(color.R, color.G, color.B, color.A);
        }
        public static Color ImageSharpColorFromRgba32(Rgba32 c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static Rgba32 Rgba32FromDrawingColor(Color color)
        {
            return new Rgba32(color.R, color.G, color.B, color.A);
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
