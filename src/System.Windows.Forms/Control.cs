using System;
using System.Drawing;

namespace System.Windows.Forms
{
    public class Control : IDisposable
    {
        public IntPtr Handle { get; protected set; }

        public Control()
        {
            Application.InitializeQt();
            Controls = new ControlCollection(this);
        }

        public ControlCollection Controls { get; }

        internal void EnsureCreated()
        {
            if (Handle == IntPtr.Zero)
            {
                CreateHandle();
            }
        }

        protected virtual void CreateHandle()
        {
            Handle = NativeMethods.QWidget_Create(IntPtr.Zero);
            SetCommonProperties();
        }

        protected void SetCommonProperties()
        {
            if (_backColor != Color.Empty)
            {
                NativeMethods.QWidget_SetBackColor(Handle, _backColor.R, _backColor.G, _backColor.B, _backColor.A);
            }

            if (!_enabled)
            {
                NativeMethods.QWidget_SetEnabled(Handle, _enabled);
            }
        }

        public string Text
        {
            set
            {
                EnsureCreated();
                NativeMethods.QWidget_SetTitle(Handle, value);
            }
        }

        public bool Visible
        {
            set
            {
                if (value)
                {
                    EnsureCreated();
                    NativeMethods.QWidget_Show(Handle);
                }
            }
        }

        public void Show()
        {
            EnsureCreated();
            NativeMethods.QWidget_Show(Handle);
        }

        public Point Location
        {
            get => _location;
            set
            {
                _location = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QWidget_Move(Handle, value.X, value.Y);
                }
            }
        }
        private Point _location;

        public Size Size
        {
            get => _size;
            set
            {
                _size = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QWidget_Resize(Handle, value.Width, value.Height);
                }
            }
        }
        private Size _size = new Size(100, 30); // Default button size

        public int Left
        {
            get => Location.X;
            set => Location = new Point(value, Location.Y);
        }

        public int Top
        {
            get => Location.Y;
            set => Location = new Point(Location.X, value);
        }

        public int Width
        {
            get => Size.Width;
            set => Size = new Size(value, Size.Height);
        }

        public int Height
        {
            get => Size.Height;
            set => Size = new Size(Size.Width, value);
        }

        public Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QWidget_SetBackColor(Handle, value.R, value.G, value.B, value.A);
                }
            }
        }
        private Color _backColor = Color.Empty;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.QWidget_SetEnabled(Handle, value);
                }
            }
        }
        private bool _enabled = true;

        public void Dispose()
        {
            // TODO: Implement proper disposal
        }
    }
}
