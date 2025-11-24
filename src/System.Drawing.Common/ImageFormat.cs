using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class ImageFormat
    {
        public static ImageFormat Png { get; } = new ImageFormat(".png");
        public static ImageFormat Jpeg { get; } = new ImageFormat(".jpg");
        public static ImageFormat Bmp { get; } = new ImageFormat(".bmp");
        public static ImageFormat Gif { get; } = new ImageFormat(".gif");
        public static ImageFormat Icon { get; } = new ImageFormat(".ico");
        public static ImageFormat Webp { get; } = new ImageFormat(".webp");
        private string _name;
        private ImageFormat(string name)
        {
            this._name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
