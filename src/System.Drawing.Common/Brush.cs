
using SixLabors.ImageSharp.Drawing.Processing;

namespace System.Drawing
{
    public class Brush : IDisposable
    {
        public Color Color { get; }
        public SolidBrush ImageSharpBrush { get; }

        public Brush(Color color)
        {
            this.Color = color;
            this.ImageSharpBrush = new SolidBrush(Helpers.ImageSharpColorFromDrawingColor(color));
        }

        public void Dispose()
        {
        }
    }
}