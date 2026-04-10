using System.ComponentModel;

namespace System.Windows.Forms
{
    public class FormClosingEventArgs : CancelEventArgs
    {
        public CloseReason CloseReason { get; }

        public FormClosingEventArgs(CloseReason closeReason, bool cancel)
            : base(cancel)
        {
            CloseReason = closeReason;
        }
    }
}
