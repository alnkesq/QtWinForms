using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class GroupBox : Control
    {
        private string _text = string.Empty;

        public GroupBox() : base()
        {
            Size = new Size(200, 100);
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QGroupBox_Create(IntPtr.Zero, _text);
                SetCommonProperties();
                CreateChildren();
            }
        }

        public override string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QGroupBox_SetTitle(QtHandle, _text);
                }
            }
        }
    }
}
