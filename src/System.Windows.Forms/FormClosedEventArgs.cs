namespace System.Windows.Forms
{
    public class FormClosedEventArgs : EventArgs
    {

        public CloseReason CloseReason { get; }

        public FormClosedEventArgs(CloseReason closeReason)
        {
            CloseReason = closeReason;
        }
    }

}
