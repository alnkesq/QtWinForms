using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class FontFamily(string name) : IDisposable
    {
        public static FontFamily GenericSansSerif => new FontFamily(string.Empty);
        public string Name => name;
        public void Dispose()
        {
        }
    }
}
