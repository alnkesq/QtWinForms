using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
            var img = image.ImageSharpImage;
            image.InvalidateCompressedImageBytes();
            return new Graphics(img);
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

        public void DrawImage(Image image, PointF point)
        {
            DrawImage(image, point.X, point.Y);
        }
        public unsafe void DrawImage(Image image, float x, float y)
        {
            ImageSharpImage.Mutate(ctx => ctx.DrawImage(image.ImageSharpImage, new SixLabors.ImageSharp.Point((int)x, (int)y), 1f));
        }
        public void DrawImage(Image image, RectangleF rect)
        {
            DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public unsafe void DrawImage(Image image, float x, float y, float width, float height)
        {
            using var scaled = image.ImageSharpImage.Clone(ctx => ctx.Resize((int)width, (int)height));
            ImageSharpImage.Mutate(ctx => ctx.DrawImage(scaled, new SixLabors.ImageSharp.Point((int)x, (int)y), 1f));
        }

        public void DrawImage(Image image, Point point)
        {
            DrawImage(image, (float)point.X, (float)point.Y);
        }

        public void DrawImage(Image image, int x, int y)
        {
            DrawImage(image, (float)x, (float)y);
        }

        public void DrawImage(Image image, Rectangle rect)
        {
            DrawImage(image, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            DrawImage(image, (float)x, (float)y, (float)width, (float)height);
        }
        public unsafe void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            if (srcUnit != GraphicsUnit.Pixel) throw new NotSupportedException();
        }
        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            DrawImage(image, (float)x, (float)y, (RectangleF)srcRect, srcUnit);
        }
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            if (srcUnit != GraphicsUnit.Pixel) throw new NotSupportedException();

            using var processed = image.ImageSharpImage.Clone(ctx => ctx
                .Crop(new SixLabors.ImageSharp.Rectangle((int)srcRect.X, (int)srcRect.Y, (int)srcRect.Width, (int)srcRect.Height))
                .Resize((int)destRect.Width, (int)destRect.Height));

            ImageSharpImage.Mutate(ctx => ctx.DrawImage(processed, new SixLabors.ImageSharp.Point((int)destRect.X, (int)destRect.Y), 1f));
        }
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            DrawImage(image, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit);
        }
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, null);
        }
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttrs)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, null);
        }
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttrs, DrawImageAbort? callback)
        {
            DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, IntPtr.Zero);
        }
        public unsafe void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttrs, DrawImageAbort? callback, nint callbackData)
        {
            if (srcUnit != GraphicsUnit.Pixel) throw new NotSupportedException();

            using var processed = image.ImageSharpImage.Clone(ctx => ctx
                .Crop(new SixLabors.ImageSharp.Rectangle((int)srcX, (int)srcY, (int)srcWidth, (int)srcHeight))
                .Resize(destRect.Width, destRect.Height));

            ImageSharpImage.Mutate(ctx => ctx.DrawImage(processed, new SixLabors.ImageSharp.Point(destRect.X, destRect.Y), 1f));
        }
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            DrawImage(image, destRect, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, (ImageAttributes?)null);
        }

        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttr)
        {
            DrawImage(image, destRect, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, imageAttr, (DrawImageAbort?)null);
        }
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttr, DrawImageAbort? callback)
        {
            DrawImage(image, destRect, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, imageAttr, callback, IntPtr.Zero);
        }
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes? imageAttrs, DrawImageAbort? callback, nint callbackData)
        {
            DrawImage(image, destRect, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, imageAttrs, callback, callbackData);
        }
        public void DrawImageUnscaled(Image image, Point point)
        {
            DrawImage(image, point.X, point.Y);
        }
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            DrawImage(image, x, y);
        }

        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            DrawImage(image, rect.X, rect.Y);
        }
        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        {
            DrawImage(image, x, y);
        }


        public void Dispose()
        {
        }
        public delegate bool DrawImageAbort(nint callbackdata);

    }
}
