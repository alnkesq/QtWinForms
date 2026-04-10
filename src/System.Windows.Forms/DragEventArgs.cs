namespace System.Windows.Forms
{
    public class DragEventArgs : EventArgs
    {
        public DragDropEffects Effect { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public IDataObject? Data => null;
    }
}
