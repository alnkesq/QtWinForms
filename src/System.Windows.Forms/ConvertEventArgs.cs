using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class ConvertEventArgs : EventArgs
    {
        public object? Value { get; set; }

        public Type? DesiredType { get; }

        public ConvertEventArgs(object? value, Type? desiredType)
        {
            Value = value;
            DesiredType = desiredType;
        }
    }

}
