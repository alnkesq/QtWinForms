using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace System.Drawing
{
    public class Graphics : IDisposable
    {
        public SmoothingMode SmoothingMode { get; set; }
        private SixLabors.ImageSharp.Image<Rgba32> ImageSharpImage;

        public Graphics(Image<Rgba32> imageSharpImage)
        {
            ImageSharpImage = imageSharpImage;
        }

        public static Graphics FromImage(Image image)
        {
            return new Graphics(image.ImageSharpImage);
        }

        public void Clear(Color color)
        {
            ImageSharpImage.Mutate(ctx => ctx.BackgroundColor(Helpers.ImageSharpColorFromDrawingColor(color)));
        }



        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }
        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            DrawLine(pen, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y);
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            ImageSharpImage.Mutate(ctx =>
                       ctx.DrawLine(pen.ImageSharpPen, new SixLabors.ImageSharp.PointF(x1, y1), new SixLabors.ImageSharp.PointF(x2, y2)));
        }
        public void FillEllipse(Brush brush, RectangleF rect)
        {
            FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillEllipse(Brush brush, Rectangle rect)
        {
            FillEllipse(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }
        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            FillEllipse(brush, (float)x, (float)y, (float)width, (float)height);
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            var ellipse = new EllipsePolygon(
             x + width / 2f,
             y + height / 2f,
             width,
             height);

            ImageSharpImage.Mutate(ctx => ctx.Fill(brush.ImageSharpBrush, ellipse));
        }
        public void DrawRectangle(Pen pen, RectangleF rect)
        {
            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }


        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }


        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            var rect = new RectangularPolygon(x, y, width, height);

            ImageSharpImage.Mutate(ctx => ctx.Draw(pen.ImageSharpPen, rect));
        }

        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            DrawRectangle(pen, (float)x, (float)y, (float)width, (float)height);
        }

        public void Dispose()
        {
        }
    }
}
