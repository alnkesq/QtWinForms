using System.Collections;

namespace System.Windows.Forms
{
    public class ListViewItem
    {
        private string _text = "";
        internal ListView? _listView;
        private ListViewSubItemCollection _subItems;
        internal IntPtr _nativeItem = IntPtr.Zero;

        public object? Tag { get; set; }

        public ListViewItem()
        {
            _subItems = new ListViewSubItemCollection(this);
        }

        public ListViewItem(string text) : this()
        {
            Text = text;
        }

        public ListViewItem(string[] items) : this()
        {
            if (items != null && items.Length > 0)
            {
                _text = items[0];
                for (int i = 1; i < items.Length; i++)
                {
                    _subItems.Add(new ListViewSubItem(this, items[i]));
                }
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                _listView?.UpdateItem(this);
            }
        }

        public ListViewSubItemCollection SubItems => _subItems;
        
        private bool _selected;
        public bool Selected
        {
            get
            {
                if (_listView != null && _listView.IsHandleCreated)
                {
                    if (_listView.View == View.Details)
                    {
                        if (_nativeItem != IntPtr.Zero)
                            return NativeMethods.QTreeWidgetItem_IsSelected(_nativeItem);
                    }
                    else
                    {
                        return NativeMethods.QListWidget_IsItemSelected(_listView.QtHandle, Index);
                    }
                }
                return _selected;
            }
            set
            {
                _selected = value;
                if (_listView != null && _listView.IsHandleCreated)
                {
                    if (_listView.View == View.Details)
                    {
                        if (_nativeItem != IntPtr.Zero)
                            NativeMethods.QTreeWidgetItem_SetSelected(_nativeItem, value);
                    }
                    else
                    {
                        NativeMethods.QListWidget_SetItemSelected(_listView.QtHandle, Index, value);
                    }
                }
            }
        }

        public int Index { get; internal set; } = -1;

        internal ListView? ListView
        {
            get => _listView;
            set => _listView = value;
        }

        internal void EnsureNativeItem()
        {
            if (_nativeItem == IntPtr.Zero && _listView != null && _listView.IsHandleCreated)
            {
                _listView.CreateNativeItem(this);
            }
        }

        private int _imageIndex = -1;
        public int ImageIndex
        {
            get => _imageIndex;
            set
            {
                _imageIndex = value;
                _imageKey = null; // Clear key when index is set
                _listView?.UpdateItemIcon(this);
            }
        }

        private string? _imageKey;
        public string? ImageKey
        {
            get => _imageKey;
            set
            {
                _imageKey = value;
                _imageIndex = -1; // Clear index when key is set
                _listView?.UpdateItemIcon(this);
            }
        }

        internal int GetResolvedImageIndex(ListView listView)
        {
            // If ImageKey is set, resolve it to an index
            if (!string.IsNullOrEmpty(_imageKey))
            {
                ImageList? imageList = listView.View == View.LargeIcon 
                    ? listView.LargeImageList 
                    : listView.SmallImageList;

                if (imageList != null)
                {
                    int index = imageList.Images.IndexOfKey(_imageKey);
                    if (index >= 0)
                        return index;
                }
            }

            // Otherwise use ImageIndex
            return _imageIndex;
        }

        public class ListViewSubItemCollection : IList
        {
            private readonly ListViewItem _owner;
            private readonly List<ListViewSubItem> _items = new List<ListViewSubItem>();

            internal ListViewSubItemCollection(ListViewItem owner)
            {
                _owner = owner;
            }

            public ListViewSubItem this[int index]
            {
                get => _items[index];
                set
                {
                    _items[index] = value;
                    _owner._listView?.UpdateItem(_owner);
                }
            }

            public int Count => _items.Count;

            public bool IsReadOnly => false;

            public bool IsFixedSize => false;

            public object SyncRoot => ((ICollection)_items).SyncRoot;

            public bool IsSynchronized => false;

            object? IList.this[int index]
            {
                get => this[index];
                set => this[index] = (ListViewSubItem)value!;
            }

            public ListViewSubItem Add(ListViewSubItem item)
            {
                _items.Add(item);
                item.Owner = _owner;
                _owner._listView?.UpdateItem(_owner);
                return item;
            }

            public ListViewSubItem Add(string text)
            {
                var item = new ListViewSubItem(_owner, text);
                return Add(item);
            }

            public void Clear()
            {
                _items.Clear();
                _owner._listView?.UpdateItem(_owner);
            }

            public bool Contains(ListViewSubItem item) => _items.Contains(item);

            public int IndexOf(ListViewSubItem item) => _items.IndexOf(item);

            public void RemoveAt(int index)
            {
                _items.RemoveAt(index);
                _owner._listView?.UpdateItem(_owner);
            }

            public IEnumerator GetEnumerator() => _items.GetEnumerator();

            int IList.Add(object? value)
            {
                Add((ListViewSubItem)value!);
                return _items.Count - 1;
            }

            bool IList.Contains(object? value) => Contains((ListViewSubItem)value!);

            int IList.IndexOf(object? value) => IndexOf((ListViewSubItem)value!);

            void IList.Insert(int index, object? value) => throw new NotImplementedException();

            void IList.Remove(object? value) => throw new NotImplementedException();

            void ICollection.CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
        }
    }

    public class ListViewSubItem
    {
        private string _text = "";
        private ListViewItem? _owner;

        public ListViewSubItem()
        {
        }

        public ListViewSubItem(ListViewItem owner, string text)
        {
            _owner = owner;
            _text = text;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _owner?.ListView?.UpdateItem(_owner);
            }
        }

        internal ListViewItem? Owner
        {
            get => _owner;
            set => _owner = value;
        }
    }
}
