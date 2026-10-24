using SkiaSharp;

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

        public Bitmap(SKBitmap bitmap)
            : base(bitmap)
        {
        }

        public Bitmap(int width, int height)
            : base(new SKBitmap(width, height, ImageSharpCompat.BlittableSKColorType, SKAlphaType.Unpremul))
        {
        }

        public Bitmap(Image copyFrom)
            : this(copyFrom.SkiaBitmap.Copy())
        {
        }

        public Color GetPixel(int x, int y)
        {
            return Helpers.DrawingColorFromSkiaColor(SkiaBitmap.GetPixel(x, y));
        }
        public void SetPixel(int x, int y, Color color)
        {
            SkiaBitmap.SetPixel(x, y, Helpers.SkiaColorFromDrawingColor(color));
            InvalidateCompressedImageBytes();
        }

    }
}
