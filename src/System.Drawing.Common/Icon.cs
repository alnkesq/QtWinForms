using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Drawing
{
    [TypeConverter(typeof(IconConverter))]
    public class Icon : IDisposable
    {
        public byte[] IcoBytes { get; init; }

        public void Dispose()
        {
        }
    }
}
