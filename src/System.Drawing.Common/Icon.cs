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
        public byte[] Bytes { get; }

        public Icon(byte[] bytes, bool owned = false)
        {
            if (bytes == null || bytes.Length == 0) throw new ArgumentNullException();
            Bytes = owned ? bytes : bytes.ToArray();
        }
        public Icon(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0) throw new ArgumentNullException();
            Bytes = bytes.ToArray();
        }

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
                NativeMethods.QIcon_Destroy(_nativeQIcon);
                _nativeQIcon = default;
            }
        }
    }
}
