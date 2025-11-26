using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class ImageList : IDisposable
    {
        public List<Image> Images { get; set; } = [];
        [Obsolete(Control.NotImplementedWarning)] public ColorDepth ColorDepth { get; set; } = ColorDepth.Depth32Bit;

        public void Dispose()
        {
            foreach (var image in Images)
            {
                image.Dispose();
            }
        }
    }
}
