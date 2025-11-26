using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class Bitmap : Image
    {
        public Bitmap(Stream stream) 
            : base(SixLabors.ImageSharp.Image.Load<Rgba32>(stream))
        { 
        }
        public Bitmap(SixLabors.ImageSharp.Image<Rgba32> image)
            : base(image)
        {
        }
        public Bitmap(int width, int height)
            : base(new(width, height))
        {
        }
    }
}
