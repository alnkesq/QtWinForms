using System.ComponentModel;

namespace System.Windows.Forms
{
    public abstract class CommonDialog : Component
    {
        public CommonDialog()
        {
            Application.InitializeQt();
        }
        public abstract void Reset();
        public object? Tag { get; set; }

        public DialogResult ShowDialog() => ShowDialog(null);
        public abstract DialogResult ShowDialog(IWin32Window? owner);

    }
}
