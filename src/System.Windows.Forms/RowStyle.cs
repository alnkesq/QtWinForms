using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class RowStyle : TableLayoutStyle
    {
        public float Height
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        public RowStyle()
        {
        }
        public RowStyle(SizeType sizeType)
        {
            base.SizeType = sizeType;
        }

        public RowStyle(SizeType sizeType, float height)
        {
            base.SizeType = sizeType;
            Height = height;
        }
    }

}
