using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class ImageList : IDisposable
    {
        public List<Image> Images { get; set; } = [];

        public void Dispose()
        {
            foreach (var image in Images)
            {
                image.Dispose();
            }
        }
    }
}
