using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public interface IDataObject
    {
        object? GetData(string format, bool autoConvert);

        object? GetData(string format);
        object? GetData(Type format);
        bool GetDataPresent(string format, bool autoConvert);
        bool GetDataPresent(string format);

        bool GetDataPresent(Type format);
        string[] GetFormats(bool autoConvert);
        string[] GetFormats();
        void SetData(string format, bool autoConvert, object? data);

        void SetData(string format, object? data);

        void SetData(Type format, object? data);
        void SetData(object? data);
    }
}
