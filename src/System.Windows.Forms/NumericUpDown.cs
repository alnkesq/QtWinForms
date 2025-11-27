using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class NumericUpDown : Control
    {
        private decimal _value = 0;
        private decimal _minimum = 0;
        private decimal _maximum = 100;
        private decimal _increment = 1;

        public event EventHandler? ValueChanged;

        protected unsafe override void CreateHandle()
        {

            QtHandle = NativeMethods.QDoubleSpinBox_Create(IntPtr.Zero);
            SetCommonProperties();

            NativeMethods.QDoubleSpinBox_SetRange(QtHandle, (double)_minimum, (double)_maximum);
            NativeMethods.QDoubleSpinBox_SetSingleStep(QtHandle, (double)_increment);
            NativeMethods.QDoubleSpinBox_SetValue(QtHandle, (double)_value);

            NativeMethods.QDoubleSpinBox_ConnectValueChanged(QtHandle, &OnValueChangedCallback, GCHandlePtr);
            
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void OnValueChangedCallback(IntPtr userData, double value)
        {
            var control = ObjectFromGCHandle<NumericUpDown>(userData);
            control._value = (decimal)value;
            control.OnValueChanged(EventArgs.Empty);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public decimal Value
        {
            get
            {
                if (IsHandleCreated)
                {
                    _value = (decimal)NativeMethods.QDoubleSpinBox_GetValue(QtHandle);
                }
                return _value;
            }
            set
            {
                if (value < _minimum || value > _maximum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between Minimum and Maximum.");
                }

                if (_value != value)
                {
                    _value = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QDoubleSpinBox_SetValue(QtHandle, (double)_value);
                    }
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        public decimal Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_minimum > _maximum)
                {
                    _maximum = value;
                }
                if (_value < _minimum)
                {
                    Value = _minimum;
                }
                if (IsHandleCreated)
                {
                    NativeMethods.QDoubleSpinBox_SetRange(QtHandle, (double)_minimum, (double)_maximum);
                }
            }
        }

        public decimal Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum < _minimum)
                {
                    _minimum = value;
                }
                if (_value > _maximum)
                {
                    Value = _maximum;
                }
                if (IsHandleCreated)
                {
                    NativeMethods.QDoubleSpinBox_SetRange(QtHandle, (double)_minimum, (double)_maximum);
                }
            }
        }

        public decimal Increment
        {
            get => _increment;
            set
            {
                _increment = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QDoubleSpinBox_SetSingleStep(QtHandle, (double)_increment);
                }
            }
        }
    }
}
