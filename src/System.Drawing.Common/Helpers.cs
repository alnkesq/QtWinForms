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

        internal static byte[] StreamToArray(Stream stream)
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
