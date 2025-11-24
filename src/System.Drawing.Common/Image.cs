using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public abstract class Image : IDisposable
    {
        public readonly SixLabors.ImageSharp.Image<Rgba32> ImageSharpImage;
        public Image(SixLabors.ImageSharp.Image<Rgba32> image)
        {
            this.ImageSharpImage = image;
        }
        public void Dispose()
        {
            ImageSharpImage.Dispose();
        }

        public void Save(Stream destination, ImageFormat format)
        {
            if (format == ImageFormat.Png)
                ImageSharpImage.SaveAsPng(destination);
            else if (format == ImageFormat.Jpeg)
                ImageSharpImage.SaveAsJpeg(destination);
            else if (format == ImageFormat.Bmp)
                ImageSharpImage.SaveAsBmp(destination);
            else if (format == ImageFormat.Webp)
                ImageSharpImage.SaveAsWebp(destination);
            else throw new NotSupportedException();
        }

        public static Image FromFile(string path)
        {
            return new Bitmap(SixLabors.ImageSharp.Image.Load<Rgba32>(path));
        }

        public Size Size => new Size(ImageSharpImage.Width, ImageSharpImage.Height);
    }
}
