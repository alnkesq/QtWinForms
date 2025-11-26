using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace System.Drawing
{
    [TypeConverter(typeof(IconConverter))]
    public class Icon : IDisposable
    {
        public required byte[] Bytes { get; init; }
        
        public IntPtr _nativeQIcon;

        public IntPtr GetQIcon()
        {
            if (_nativeQIcon == default)
            {
                _nativeQIcon = NativeMethods.QIcon_CreateFromData(Bytes, Bytes.Length);
            }

            return _nativeQIcon;

        }

        public void Dispose()
        {
            if (_nativeQIcon != default)
            {
                // TODO: destroy QIcon
                _nativeQIcon = default;
            }
        }
    }
}
