using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Button : Control
    {

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QPushButton_Create(IntPtr.Zero, Text);
                SetCommonProperties();
                ConnectClickEvent();
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
                    NativeMethods.QPushButton_SetText(QtHandle, _text);
                }
            }
        }
        private string _text = string.Empty;

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QPushButton_ConnectClicked(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var button = ObjectFromGCHandle<Button>(userData);
            button.OnClick(EventArgs.Empty);
        }

        public DialogResult DialogResult { get; set; }
        public FlatStyle FlatStyle { get; set; }
    }
}
