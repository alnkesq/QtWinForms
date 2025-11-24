using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class FontFamily(string name) : IDisposable
    {
        public string Name => name;
        public void Dispose()
        {
        }
    }
}
