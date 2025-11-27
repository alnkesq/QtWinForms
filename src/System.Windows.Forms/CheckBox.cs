using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class CheckBox : Control
    {
        private string _text = string.Empty;
        private bool _checked;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QCheckBox_Create(IntPtr.Zero, Text);
                SetCommonProperties();

                if (_checked)
                {
                    NativeMethods.QCheckBox_SetChecked(QtHandle, _checked);
                }
                ConnectStateChangedEvent();
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
                    NativeMethods.QCheckBox_SetText(QtHandle, _text);
                }
            }
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QCheckBox_SetChecked(QtHandle, value);
                }
            }
        }

        public CheckState CheckState
        {
            get
            {
                return Checked ? CheckState.Checked : CheckState.Unchecked;
            }
            set
            {
                Checked = value == CheckState.Checked;
            }
        }

        public event EventHandler? CheckedChanged;

        private unsafe void ConnectStateChangedEvent()
        {
            delegate* unmanaged[Cdecl]<nint, int, void> callback = &OnStateChangedCallback;
            NativeMethods.QCheckBox_ConnectStateChanged(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void OnStateChangedCallback(nint userData, int state)
        {
            var checkBox = ObjectFromGCHandle<CheckBox>(userData);

            checkBox._checked = (state != 0);
            checkBox.CheckedChanged?.Invoke(checkBox, EventArgs.Empty);
        }
    }
}
