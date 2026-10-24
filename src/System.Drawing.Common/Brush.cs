using SkiaSharp;

namespace System.Drawing
{
    public class Brush : IDisposable
    {
        public Color Color { get; }
        public SKColor SkiaColor { get; }

        protected Brush(Color color)
        {
            this.Color = color;
            this.SkiaColor = Helpers.SkiaColorFromDrawingColor(color);
        }

        public void Dispose()
        {
        }
    }
}