using System.Drawing;

namespace System.Windows.Forms
{
    public class PaintEventArgs : EventArgs, IDisposable
    {
        public void Dispose()
        {
        }

        [Obsolete(Control.NotImplementedWarning)] public Graphics Graphics { get; set; }
    }
}
