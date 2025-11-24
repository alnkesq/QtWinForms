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
    }
}
