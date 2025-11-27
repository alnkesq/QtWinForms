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

        private ImageListStreamer? _imageStream;

        public ImageListStreamer? ImageStream
        {
            get
            {
                return _imageStream;
            }
            set
            {
                if (_imageStream == value) return;
                if (_imageStream != null) throw new InvalidOperationException();
                if (Images.Count != 0) throw new InvalidOperationException();
                _imageStream = value;
                if (value != null)
                {
                    Images.AddRange(value.Images);
                }
            }
        }

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
