using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class RadioButton : Control
    {
        private string _text = string.Empty;
        private bool _checked;
        private EventHandler? _checkedChangedHandler;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QRadioButton_Create(IntPtr.Zero, Text ?? "");
                SetCommonProperties();
                
                if (_checked)
                {
                    NativeMethods.QRadioButton_SetChecked(Handle, _checked);
                }
                
                if (_checkedChangedHandler != null)
                {
                    ConnectToggledEvent();
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
                    NativeMethods.QRadioButton_SetText(Handle, _text);
                }
            }
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QRadioButton_SetChecked(Handle, value);
                    }
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler CheckedChanged
        {
            add
            {
                if (_checkedChangedHandler == null && IsHandleCreated)
                {
                    ConnectToggledEvent();
                }
                _checkedChangedHandler += value;
            }
            remove
            {
                _checkedChangedHandler -= value;
            }
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            _checkedChangedHandler?.Invoke(this, e);
        }

        private unsafe void ConnectToggledEvent()
        {
            delegate* unmanaged[Cdecl]<nint, byte, void> callback = &OnToggledCallback;
            NativeMethods.QRadioButton_ConnectToggled(Handle, (IntPtr)callback, (IntPtr)(nint)GCHandle.Alloc(this));
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnToggledCallback(nint userData, byte checkedState)
        {
            var handle = GCHandle.FromIntPtr((IntPtr)userData);
            if (handle.Target is RadioButton radioButton)
            {
                bool isChecked = checkedState != 0;
                // Only trigger if the state actually changed from what we think it is
                // This prevents double events if the setter triggered the update
                if (radioButton._checked != isChecked)
                {
                    radioButton._checked = isChecked;
                    radioButton.OnCheckedChanged(EventArgs.Empty);
                }
            }
        }
    }
}
