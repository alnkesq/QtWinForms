namespace System.Windows.Forms
{
    public abstract class DataGridViewBand
    {
        public int Index { get; internal set; } = -1;
        internal DataGridView? _owner;
        public object? Tag { get; set; }
    }
}
