using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public abstract class ButtonBase : Control
    {
        protected override Size DefaultSize => new Size(75, 23);
    }
}
