using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class Bitmap : Image
    {
        public Bitmap(Stream stream) 
            : base(Helpers.StreamToArray(stream), isOwned: true)
        { 
        }
        internal Bitmap(byte[] bytes, bool isOwned = false)
            : base(bytes, isOwned)
        { 
        
        }

        public Bitmap(SixLabors.ImageSharp.Image<Rgba32> image)
            : base(image)
        {
        }
        
        public Bitmap(int width, int height)
            : base(new SixLabors.ImageSharp.Image<Rgba32>(width, height))
        {
        }
    }
}
