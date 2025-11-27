
using SixLabors.ImageSharp.Drawing.Processing;

namespace System.Drawing
{
    public class Brush : IDisposable
    {
        public Color Color { get; }
        public SixLabors.ImageSharp.Drawing.Processing.SolidBrush ImageSharpBrush { get; }

        protected Brush(Color color)
        {
            this.Color = color;
            this.ImageSharpBrush = new(Helpers.ImageSharpColorFromDrawingColor(color));
        }

        public void Dispose()
        {
        }
    }
}