using System.Drawing;

namespace System.Windows.Forms
{
    public class ProgressBar : Control
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private ProgressBarStyle _style = ProgressBarStyle.Blocks;

        public ProgressBar()
        {
            Size = new Size(100, 23); // Default size
        }

        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QProgressBar_Create(Parent?.QtHandle ?? IntPtr.Zero);
            SetCommonProperties();
            UpdateRange();
            UpdateValue();
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

        public ProgressBarStyle Style
        {
            get => _style;
            set
            {
                if (_style != value)
                {
                    _style = value;
                    if (IsHandleCreated) UpdateRange(); // Style affects range (Marquee vs Blocks)
                }
            }
        }

        private void UpdateRange()
        {
            if (_style == ProgressBarStyle.Marquee)
            {
                NativeMethods.QProgressBar_SetRange(QtHandle, 0, 0);
            }
            else
            {
                NativeMethods.QProgressBar_SetRange(QtHandle, _minimum, _maximum);
            }
        }

        private void UpdateValue()
        {
            NativeMethods.QProgressBar_SetValue(QtHandle, _value);
        }
    }
}
