using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public class Font(FontFamily fontFamily, float emSize, FontStyle style) : IDisposable
    {
        public Font(string fontFamily, float emSize, FontStyle style)
            : this(new FontFamily(fontFamily), emSize, style)
        {
        }
        public Font(string fontFamily, float emSize)
            : this(fontFamily, emSize, FontStyle.Regular)
        {
        }
        public Font(Font prototype, FontStyle style)
            : this(prototype.FontFamily, prototype.Size, style)
        {
        }
        public float SizeInPoints => Size;
        public FontFamily FontFamily => fontFamily;
        public string Name => fontFamily.Name;
        public float Size => emSize;
        public FontStyle Style => style;
        public bool Bold => (Style & FontStyle.Bold) != 0;
        public bool Italic => (Style & FontStyle.Italic) != 0;
        public bool Strikeout => (Style & FontStyle.Strikeout) != 0;
        public bool Underline => (Style & FontStyle.Underline) != 0;
        public GraphicsUnit Unit => GraphicsUnit.Point;
        public void Dispose()
        {
        }
    }
}
