namespace System.Windows.Forms
{
    public class ColumnHeader
    {
        private string _text = "";
        private int _width = 60;
        private ListView? _listView;

        public ColumnHeader()
        {
        }

        public ColumnHeader(string text)
        {
            _text = text;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _listView?.UpdateColumnHeader(this);
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                _listView?.UpdateColumnHeader(this);
            }
        }

        public int Index { get; internal set; } = -1;

        internal ListView? ListView
        {
            get => _listView;
            set => _listView = value;
        }
    }
}
