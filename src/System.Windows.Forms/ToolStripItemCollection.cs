namespace System.Windows.Forms
{

    public abstract class ToolStripItemCollection
    {

        public void AddRange(IEnumerable<ToolStripItem> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public abstract void Add(ToolStripItem item);

        public abstract ToolStripItem Add(string text);

    }
}
