using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace System.Drawing
{
    [TypeConverter(typeof(ImageConverter))]
    public abstract class Image : IDisposable
    {
        private SixLabors.ImageSharp.Image<Rgba32>? _imageSharpImage;
        private byte[]? _bytes;
        private IntPtr _nativeQIcon;

        public Image(SixLabors.ImageSharp.Image<Rgba32> image)
        {
            this._imageSharpImage = image;
        }
        internal Image(byte[] bytes, bool isOwned = false)
        {
            this._bytes = isOwned ? bytes : bytes.ToArray();
        }
        public Image(ReadOnlySpan<byte> bytes)
        {
            this._bytes = bytes.ToArray();
        }

        public void Dispose()
        {
            _imageSharpImage?.Dispose();
            _bytes = null;
        }


        public SixLabors.ImageSharp.Image<Rgba32> ImageSharpImage
        {
            get
            {
                if (_imageSharpImage == null)
                {
                    _imageSharpImage = SixLabors.ImageSharp.Image.Load<Rgba32>(_bytes);
                }
                return _imageSharpImage;
            }
        }

        public byte[] Bytes
        {
            get
            {
                if (_bytes == null)
                {
                    using var ms = new MemoryStream();
                    _imageSharpImage.SaveAsPng(ms);
                    _bytes = ms.ToArray();
                }
                return _bytes;
            }
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
            return new Bitmap(File.ReadAllBytes(path));
        }

        internal static Image FromStream(Stream stream)
        {
            return new Bitmap(stream);
        }

        public Size Size => new Size(ImageSharpImage.Width, ImageSharpImage.Height);

        public IntPtr GetQIcon()
        {
            if (_nativeQIcon == default)
            {
                _nativeQIcon = NativeMethods.QIcon_CreateFromData(Bytes, Bytes.Length);
            }

            return _nativeQIcon;

        }
    }
}
