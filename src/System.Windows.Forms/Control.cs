using System;
using System.Drawing;

namespace System.Windows.Forms
{
    public class Control : IDisposable
    {
        public IntPtr Handle { get; protected set; }

        public Control()
        {
            // Widget creation deferred until EnsureCreated() is called
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
            Console.WriteLine("Creating QWidget...");
            Handle = NativeMethods.QWidget_Create(IntPtr.Zero);
            Console.WriteLine("QWidget created!");
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

        public void Dispose()
        {
            // TODO: Implement proper disposal
        }
    }
}
