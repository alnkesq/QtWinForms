using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
    public abstract class ContainerControl : Control
    {
        [Obsolete(NotImplementedWarning)] public Control? ActiveControl { get; set; }
        [Obsolete(NotImplementedWarning)] public SizeF AutoScaleDimensions { get; set; }
        [Obsolete(NotImplementedWarning)] public SizeF AutoScaleFactor { get; set; }
        [Obsolete(NotImplementedWarning)] public AutoScaleMode AutoScaleMode { get; set; }

        public Form? ParentForm
        {
            get 
            {
                return FindForm();
            }
        }
    }
}
