using System.ComponentModel;
using System.Globalization;

namespace System.Drawing
{
    public class ImageConverter : TypeConverter
    {
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is byte[] bytes)
            {
                using var ms = new MemoryStream(bytes);
                return Image.FromStream(ms);
            }
            return null;
        }

    }
}