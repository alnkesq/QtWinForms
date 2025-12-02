using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class ColumnStyle : TableLayoutStyle
    {
        public float Width
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

        public ColumnStyle()
        {
        }
        public ColumnStyle(SizeType sizeType)
        {
            base.SizeType = sizeType;
        }

        public ColumnStyle(SizeType sizeType, float width)
        {
            base.SizeType = sizeType;
            Width = width;
        }
    }

}
