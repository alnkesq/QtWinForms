using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms
{
    public abstract class CommonDialog : Component
    {
        public abstract void Reset();
        public object? Tag { get; set; }

        public DialogResult ShowDialog() => ShowDialog(null);
        public abstract DialogResult ShowDialog(IWin32Window? owner);

    }
}
