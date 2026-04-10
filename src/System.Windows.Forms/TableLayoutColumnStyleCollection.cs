namespace System.Windows.Forms
{
    public class TableLayoutColumnStyleCollection : TableLayoutStyleCollection
    {
        internal TableLayoutColumnStyleCollection(TableLayoutPanel? owner = null) : base(owner)
        {
        }

        public int Add(ColumnStyle columnStyle)
        {
            return base.Add(columnStyle);
        }
    }
}
