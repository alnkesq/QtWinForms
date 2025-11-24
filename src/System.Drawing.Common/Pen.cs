
using SixLabors.ImageSharp.Drawing.Processing;

namespace System.Drawing
{
    public class Pen : IDisposable
    {
        public Color Color { get; }
        public SolidPen ImageSharpPen { get; set; }

        public Pen(Color color)
        {
            this.Color = color;
            this.ImageSharpPen = new SixLabors.ImageSharp.Drawing.Processing.SolidPen(Helpers.ImageSharpColorFromDrawingColor(color));
        }

        public void Dispose()
        {
        }
    }
}