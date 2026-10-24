using SkiaSharp;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Drawing
{
    [TypeConverter(typeof(ImageConverter))]
    public abstract class Image : IDisposable
    {
        private SKBitmap? _skiaBitmap;
        private byte[]? _bytes;
        private IntPtr _nativeQIcon;
        private IntPtr _nativeQPixmap;

        public Image(SKBitmap bitmap)
        {
            this._skiaBitmap = bitmap;
        }
        internal Image(byte[] bytes, bool isOwned = false)
        {
            if (bytes == null || bytes.Length == 0) throw new ArgumentNullException();
            this._bytes = isOwned ? bytes : bytes.ToArray();
        }
        public Image(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0) throw new ArgumentNullException();
            this._bytes = bytes.ToArray();
        }

        public void Dispose()
        {
            _skiaBitmap?.Dispose();
            _skiaBitmap = null;
            _bytes = null;
            if (_nativeQIcon != default)
            {
                NativeMethods.QIcon_Destroy(_nativeQIcon);
                _nativeQIcon = default;
            }
            if (_nativeQPixmap != default)
            {
                NativeMethods.QPixmap_Destroy(_nativeQPixmap);
                _nativeQPixmap = default;
            }
        }


        public SKBitmap SkiaBitmap
        {
            get
            {
                if (_skiaBitmap == null)
                {
                    _skiaBitmap = SKBitmap.Decode(_bytes);
                    if (_skiaBitmap == null)
                    {
                        throw new InvalidDataException("Image could not be parsed.");
                    }
                }
                return _skiaBitmap;
            }
        }

        public byte[] Bytes
        {
            get
            {
                if (_bytes == null)
                {
                    using var ms = new MemoryStream();
                    using var data = SkiaBitmap.Encode(SKEncodedImageFormat.Png, 100);
                    if (data == null)
                    {
                        throw new InvalidOperationException("Image could not be encoded.");
                    }
                    ms.Write(data.AsSpan());
                    _bytes = ms.ToArray();
                }
                return _bytes;
            }
        }

        public void Save(Stream destination, ImageFormat format)
        {
            var encoding = format == ImageFormat.Png ? SKEncodedImageFormat.Png
                : format == ImageFormat.Jpeg ? SKEncodedImageFormat.Jpeg
                : format == ImageFormat.Bmp ? SKEncodedImageFormat.Bmp
                : format == ImageFormat.Webp ? SKEncodedImageFormat.Webp
                : throw new NotSupportedException();
            var quality = format == ImageFormat.Jpeg ? ImageSharpCompat.DefaultJpegQuality
                : format == ImageFormat.Webp ? ImageSharpCompat.DefaultWebpQuality
                : 100;
            using var data = SkiaBitmap.Encode(encoding, quality);
            if (data == null)
            {
                throw new InvalidOperationException("Image could not be encoded.");
            }
            data.SaveTo(destination);
        }

        public static Image FromFile(string path)
        {
            return new Bitmap(File.ReadAllBytes(path));
        }

        public static Image FromStream(Stream stream)
        {
            return new Bitmap(stream);
        }

        public Size Size => new Size(SkiaBitmap.Width, SkiaBitmap.Height);

        public int Width => Size.Width;
        public int Height => Size.Height;

        public IntPtr GetQIcon()
        {
            if (_nativeQIcon == default)
            {
                _nativeQIcon = NativeMethods.QIcon_CreateFromData(Bytes, Bytes.Length);
            }

            return _nativeQIcon;
        }

        public IntPtr GetQPixmap()
        {
            if (_nativeQPixmap == default)
            {
                _nativeQPixmap = NativeMethods.QPixmap_CreateFromData(Bytes, Bytes.Length);
            }

            return _nativeQPixmap;
        }

        public void InvalidateCompressedImageBytes()
        {
            if (_skiaBitmap == null) throw new InvalidOperationException();
            _bytes = null;
        }
    }
}
