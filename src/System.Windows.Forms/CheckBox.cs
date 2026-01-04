using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class CheckBox : Control
    {
        private string _text = string.Empty;
        private CheckState _checkState;
        private bool _threeState;

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QCheckBox_Create(IntPtr.Zero, Text);
            SetCommonProperties();

            if (_checkState != CheckState.Unchecked || _threeState)
            {
                NativeMethods.QCheckBox_SetCheckState(QtHandle, (int)_checkState, _threeState);
            }
            ConnectStateChangedEvent();
        }
        protected override Size DefaultSize => new Size(104, 24);
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
            get => _checkState == CheckState.Checked;
            set
            {
                CheckState = value ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        public CheckState CheckState
        {
            get => _checkState;
            set
            {
                if (_checkState != value)
                {
                    _checkState = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QCheckBox_SetCheckState(QtHandle, (int)_checkState, _threeState);
                    }
                }
            }
        }

        public bool ThreeState
        {
            get => _threeState;
            set
            {
                if (_threeState != value)
                {
                    _threeState = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QCheckBox_SetCheckState(QtHandle, (int)_checkState, _threeState);
                    }
                }
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
            var newState = (CheckState)state;
            var prevState = checkBox._checkState;
            if (prevState != newState)
            {
                checkBox._checkState = newState;
                if (prevState == CheckState.Indeterminate && !checkBox._threeState)
                    NativeMethods.QCheckBox_SetCheckState(checkBox.QtHandle, (int)newState, false);

                checkBox.CheckedChanged?.Invoke(checkBox, EventArgs.Empty);
            }
        }

        [Obsolete(NotImplementedWarning)] public ContentAlignment CheckAlign { get; set; }
    }
}
