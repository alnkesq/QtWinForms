using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public class ImageList : IComponent, IDisposable
    {
        public ImageList()
        {
        }
        public ImageList(IContainer container)
        {
            container.Add(this);
        }

        [Obsolete(Control.NotImplementedWarning)] public ImageListStreamer? ImageStream { get; set; }

        [Obsolete(Control.NotImplementedWarning)] public Color TransparentColor { get; set; }
        public ImageCollection Images { get; set; } = [];
        [Obsolete(Control.NotImplementedWarning)] public ColorDepth ColorDepth { get; set; } = ColorDepth.Depth32Bit;
        public ISite? Site { get; set; }

        public event EventHandler? Disposed;

        public void Dispose()
        {
            foreach (var image in Images)
            {
                image.Dispose();
            }
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        public class ImageCollection : List<Image>
        {
            internal Dictionary<string, int>? nameToIndex;
            [Obsolete(Control.NotImplementedWarning)]
            public void SetKeyName(int index, string name)
            {
                nameToIndex ??= new();
                nameToIndex[name] = index;
            }
        }
    }
}
