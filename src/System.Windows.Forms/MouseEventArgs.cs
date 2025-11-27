using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{

    public class MouseEventArgs : EventArgs
    {
        public MouseButtons Button { get; }

        public int Clicks { get; }

        public int X { get; }
        public int Y { get; }

        public int Delta { get; }

        public Point Location => new Point(X, Y);

        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            Button = button;
            Clicks = clicks;
            X = x;
            Y = y;
            Delta = delta;
        }

        internal MouseEventArgs(MouseButtons button, int clicks, Point location, int delta = 0)
        {
            Button = button;
            Clicks = clicks;
            X = location.X;
            Y = location.Y;
            Delta = delta;
        }
    }

}
