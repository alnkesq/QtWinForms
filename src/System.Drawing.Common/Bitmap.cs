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

        public Bitmap(Image copyFrom)
            : this(copyFrom.ImageSharpImage.Clone())
        {
        }

        public Color GetPixel(int x, int y)
        {
            return Helpers.ImageSharpColorFromRgba32(ImageSharpImage[x, y]);
        }
        public void SetPixel(int x, int y, Color color)
        {
            ImageSharpImage[x, y] = Helpers.Rgba32FromDrawingColor(color);
            _bytes = null;
        }

    }
}
