using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing.Drawing2D
{
    public class HatchBrush : Brush
    {
        public HatchBrush(HatchStyle hatchstyle, Color foreColor)
            : base(foreColor)
        {
        }
        public HatchBrush(HatchStyle hatchstyle, Color foreColor, Color backColor)
            : base(foreColor)
        {
        }
    }
}
