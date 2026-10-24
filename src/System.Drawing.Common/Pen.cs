
﻿using SkiaSharp;

namespace System.Drawing
{
    public class Pen : IDisposable
    {
        public Color Color { get; }
        public SKColor SkiaColor { get; }

        public Pen(Color color)
        {
            this.Color = color;
            this.SkiaColor = Helpers.SkiaColorFromDrawingColor(color);
        }
        public Pen(Color color, float size)
            : this(color)
        {
        }

        public void Dispose()
        {
        }
    }
}