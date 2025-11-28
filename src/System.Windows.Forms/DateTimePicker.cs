using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class DateTimePicker : Control
    {
        private DateTime _value = DateTime.Now;
        private DateTime _minDate = new DateTime(1753, 1, 1);
        private DateTime _maxDate = new DateTime(9998, 12, 31);

        public event EventHandler? ValueChanged;

        public DateTimePicker()
        {
            Size = new Size(200, 20);
        }

        protected unsafe override void CreateHandle()
        {
            QtHandle = NativeMethods.QDateTimeEdit_Create(Parent?.QtHandle ?? IntPtr.Zero);

            // Set initial values
            UpdateValue();
            UpdateMinDate();
            UpdateMaxDate();

            NativeMethods.QDateTimeEdit_ConnectDateTimeChanged(QtHandle, &OnDateTimeChanged, GCHandlePtr);

            SetCommonProperties();
        }

        public DateTime Value
        {
            get
            {
                if (IsHandleCreated)
                {
                    int year, month, day, hour, minute, second;
                    NativeMethods.QDateTimeEdit_GetDateTime(QtHandle, out year, out month, out day, out hour, out minute, out second);
                    _value = new DateTime(year, month, day, hour, minute, second);
                }
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    if (value < MinDate || value > MaxDate)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "Value must be between MinDate and MaxDate.");
                    }
                    _value = value;
                    UpdateValue();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        public DateTime MinDate
        {
            get => _minDate;
            set
            {
                if (_minDate != value)
                {
                    _minDate = value;
                    UpdateMinDate();
                    if (Value < _minDate)
                    {
                        Value = _minDate;
                    }
                }
            }
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                if (_maxDate != value)
                {
                    _maxDate = value;
                    UpdateMaxDate();
                    if (Value > _maxDate)
                    {
                        Value = _maxDate;
                    }
                }
            }
        }

        private void UpdateValue()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QDateTimeEdit_SetDateTime(QtHandle, _value.Year, _value.Month, _value.Day, _value.Hour, _value.Minute, _value.Second);
            }
        }

        private void UpdateMinDate()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QDateTimeEdit_SetMinimumDateTime(QtHandle, _minDate.Year, _minDate.Month, _minDate.Day, _minDate.Hour, _minDate.Minute, _minDate.Second);
            }
        }

        private void UpdateMaxDate()
        {
            if (IsHandleCreated)
            {
                NativeMethods.QDateTimeEdit_SetMaximumDateTime(QtHandle, _maxDate.Year, _maxDate.Month, _maxDate.Day, _maxDate.Hour, _maxDate.Minute, _maxDate.Second);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void OnDateTimeChanged(IntPtr userData, int year, int month, int day, int hour, int minute, int second)
        {
            var control = ObjectFromGCHandle<DateTimePicker>(userData);
            control._value = new DateTime(year, month, day, hour, minute, second);
            control.OnValueChanged(EventArgs.Empty);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
