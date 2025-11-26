using System.ComponentModel;
using System.Globalization;

namespace System.Drawing
{
    public class IconConverter : TypeConverter
    {
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is byte[] bytes)
            {
                return new Icon() { Bytes = bytes };
            }
            return null;
        }
    }
}