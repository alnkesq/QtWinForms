using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class Button : Control
    {
        private EventHandler? _clickHandler;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QPushButton_Create(IntPtr.Zero, Text);
                SetCommonProperties();

                if (_clickHandler != null)
                {
                    ConnectClickEvent();
                }
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
                    NativeMethods.QPushButton_SetText(Handle, _text);
                }
            }
        }
        private string _text = string.Empty;

        public event EventHandler Click
        {
            add
            {
                if (_clickHandler == null && IsHandleCreated)
                {
                    ConnectClickEvent();
                }
                _clickHandler += value;
            }
            remove
            {
                _clickHandler -= value;
            }
        }

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QPushButton_ConnectClicked(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var button = ObjectFromGCHandle<Button>(userData);
            button._clickHandler?.Invoke(button, EventArgs.Empty);
        }

        public DialogResult DialogResult { get; set; }
        public FlatStyle FlatStyle { get; set; }
    }
}
