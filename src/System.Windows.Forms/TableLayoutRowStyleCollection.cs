namespace System.Windows.Forms
{
    public class TableLayoutRowStyleCollection : TableLayoutStyleCollection
    {
        internal TableLayoutRowStyleCollection(TableLayoutPanel? owner = null) : base(owner)
        {
        }

        public int Add(RowStyle rowStyle)
        {
            return base.Add(rowStyle);
        }
    }
}
