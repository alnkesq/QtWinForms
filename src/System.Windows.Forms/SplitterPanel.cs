using System.Drawing;

namespace System.Windows.Forms
{
    public class SplitterPanel : Panel
    {
        private SplitContainer _owner;

        public SplitterPanel(SplitContainer owner)
        {
            _owner = owner;
        }
    }
}
