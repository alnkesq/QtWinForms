using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Drawing
{
    [TypeConverter(typeof(ImageConverter))]
    public abstract class Image : IDisposable
    {
        public readonly SixLabors.ImageSharp.Image<Rgba32> ImageSharpImage;
        
        // Lazily-initialized native Qt icon pointer for reuse
        public IntPtr _nativeQIcon = IntPtr.Zero;
        
        public Image(SixLabors.ImageSharp.Image<Rgba32> image)
        {
            this.ImageSharpImage = image;
        }
        public void Dispose()
        {
            ImageSharpImage.Dispose();
            // Note: We don't dispose the native QIcon here because it may be shared
            // across multiple tree nodes. The ImageList will manage the lifecycle.
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

        internal static Image FromStream(Stream stream)
        {
            return new Bitmap(SixLabors.ImageSharp.Image.Load<Rgba32>(stream));
        }

        public Size Size => new Size(ImageSharpImage.Width, ImageSharpImage.Height);
    }
}
