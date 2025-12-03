using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TrackBar : Control
    {
        private int _minimum = 0;
        private int _maximum = 10;
        private int _value = 0;

        public TrackBar()
        {
        }
        protected override Size DefaultSize => new Size(104, 45);

        protected unsafe override void CreateHandle()
        {
            QtHandle = NativeMethods.QSlider_Create(Parent?.QtHandle ?? IntPtr.Zero);
            SetCommonProperties();
            UpdateRange();
            UpdateValue();
            ConnectValueChanged();
        }

        public int Minimum
        {
            get => _minimum;
            set
            {
                if (_minimum != value)
                {
                    _minimum = value;
                    if (_maximum < _minimum) _maximum = _minimum;
                    if (_value < _minimum) _value = _minimum;
                    if (IsHandleCreated) UpdateRange();
                }
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;
                    if (_minimum > _maximum) _minimum = _maximum;
                    if (_value > _maximum) _value = _maximum;
                    if (IsHandleCreated) UpdateRange();
                }
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value < _minimum || value > _maximum)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Value of '{value}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.");

                if (_value != value)
                {
                    _value = value;
                    if (IsHandleCreated) UpdateValue();
                }
            }
        }

        public event EventHandler? ValueChanged;
        public event EventHandler? Scroll;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
            Scroll?.Invoke(this, e);
        }

        private void UpdateRange()
        {
            NativeMethods.QSlider_SetRange(QtHandle, _minimum, _maximum);
        }

        private void UpdateValue()
        {
            NativeMethods.QSlider_SetValue(QtHandle, _value);
        }

        private unsafe void ConnectValueChanged()
        {
            NativeMethods.QSlider_ConnectValueChanged(QtHandle, &OnValueChangedCallback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void OnValueChangedCallback(IntPtr userData, int value)
        {
            var trackBar = ObjectFromGCHandle<TrackBar>(userData);
            trackBar._value = value;
            trackBar.OnValueChanged(EventArgs.Empty);
        }

        [Obsolete(NotImplementedWarning)] public int TickFrequency { get; set; }
    }
}
