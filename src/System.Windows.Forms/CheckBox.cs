using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class CheckBox : Control
    {
        private string? _text;
        private bool _checked;
        private EventHandler? _checkedChangedHandler;

        public CheckBox()
        {
            // Deferred creation
        }

        protected override void CreateHandle()
        {
            if (Handle == IntPtr.Zero)
            {
                Handle = NativeMethods.QCheckBox_Create(IntPtr.Zero, Text ?? "");
                SetCommonProperties();
                
                // Set initial checked state
                if (_checked)
                {
                    NativeMethods.QCheckBox_SetChecked(Handle, _checked);
                }
                
                // Connect state changed event if handler is already attached
                if (_checkedChangedHandler != null)
                {
                    ConnectStateChangedEvent();
                }
            }
        }

        public new string Text
        {
            get => _text ?? "";
            set
            {
                _text = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QCheckBox_SetText(Handle, value);
                }
            }
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QCheckBox_SetChecked(Handle, value);
                }
            }
        }

        public event EventHandler CheckedChanged
        {
            add
            {
                if (_checkedChangedHandler == null && Handle != IntPtr.Zero)
                {
                    ConnectStateChangedEvent();
                }
                _checkedChangedHandler += value;
            }
            remove
            {
                _checkedChangedHandler -= value;
            }
        }

        private unsafe void ConnectStateChangedEvent()
        {
            delegate* unmanaged[Cdecl]<nint, int, void> callback = &OnStateChangedCallback;
            NativeMethods.QCheckBox_ConnectStateChanged(Handle, (IntPtr)callback, (IntPtr)(nint)GCHandle.Alloc(this));
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnStateChangedCallback(nint userData, int state)
        {
            var handle = GCHandle.FromIntPtr((IntPtr)userData);
            if (handle.Target is CheckBox checkBox)
            {
                checkBox._checked = (state != 0);
                checkBox._checkedChangedHandler?.Invoke(checkBox, EventArgs.Empty);
            }
        }
    }
}
