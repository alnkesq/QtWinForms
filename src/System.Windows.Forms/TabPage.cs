using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TabPage : Panel
    {
        private string _text = string.Empty;

        public TabPage() : base()
        {
        }

        public TabPage(string text) : base()
        {
            _text = text;
        }

        public override string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                // Note: The text is set on the TabControl, not the page itself
                // The parent TabControl will handle updating the tab text
            }
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QWidget_Create(IntPtr.Zero);
                SetCommonProperties();
            }
        }
    }
}
