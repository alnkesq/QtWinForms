using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ListView : Control
    {
        private View _view = View.LargeIcon;
        private ListViewItemCollection _items;
        private ColumnHeaderCollection _columns;
        private bool _isDetailsView = false;

        public ListView()
        {
            _items = new ListViewItemCollection(this);
            _columns = new ColumnHeaderCollection(this);
        }

        protected override void CreateHandle()
        {
            // Determine which Qt widget to use based on View
            _isDetailsView = (_view == View.Details);

            if (_isDetailsView)
            {
                // Use QTreeWidget for Details view
                QtHandle = NativeMethods.QTreeWidget_Create(IntPtr.Zero);
                NativeMethods.QTreeWidget_SetHeaderHidden(QtHandle, false);
            }
            else
            {
                // Use QListWidget for LargeIcon/SmallIcon/List views
                QtHandle = NativeMethods.QListWidget_Create(IntPtr.Zero);
                
                // Set view mode
                int viewMode = _view == View.LargeIcon ? 1 : 0; // 1 = IconMode, 0 = ListMode
                NativeMethods.QListWidget_SetViewMode(QtHandle, viewMode);
            }

            SetCommonProperties();

            // Add existing columns (for Details view)
            if (_isDetailsView && _columns.Count > 0)
            {
                UpdateColumns();
            }

            // Add existing items
            foreach (ListViewItem item in _items)
            {
                item._listView = this;
                CreateNativeItem(item);
            }
        }

        public View View
        {
            get => _view;
            set
            {
                if (_view != value)
                {
                    _view = value;
                    
                    // If handle is already created, we need to recreate it with the correct widget type
                    if (IsHandleCreated)
                    {
                        RecreateHandle();
                    }
                }
            }
        }

        public ListViewItemCollection Items => _items;

        public ColumnHeaderCollection Columns => _columns;

        private ImageList? _smallImageList;
        public ImageList? SmallImageList
        {
            get => _smallImageList;
            set
            {
                _smallImageList = value;
                if (IsHandleCreated)
                {
                    UpdateAllItemIcons();
                }
            }
        }

        private ImageList? _largeImageList;
        public ImageList? LargeImageList
        {
            get => _largeImageList;
            set
            {
                _largeImageList = value;
                if (IsHandleCreated)
                {
                    UpdateAllItemIcons();
                }
            }
        }

        private void UpdateAllItemIcons()
        {
            foreach (ListViewItem item in _items)
            {
                UpdateItemIcon(item);
            }
        }

        internal IntPtr GetQIconFromImageList(int imageIndex)
        {
            ImageList? imageList = _view == View.LargeIcon ? _largeImageList : _smallImageList;
            
            if (imageList == null || imageIndex < 0 || imageIndex >= imageList.Images.Count)
                return IntPtr.Zero;

            var image = imageList.Images[imageIndex];
            return image.GetQIcon();
        }

        internal void UpdateItemIcon(ListViewItem item)
        {
            if (!IsHandleCreated || item._nativeItem == IntPtr.Zero)
                return;

            int imageIndex = item.GetResolvedImageIndex(this);
            IntPtr icon = GetQIconFromImageList(imageIndex);

            if (_isDetailsView)
            {
                NativeMethods.QTreeWidgetItem_SetIcon(item._nativeItem, 0, icon);
            }
            else
            {
                // For QListWidget, we need to set icon on the item
                if (imageIndex >= 0)
                {
                    NativeMethods.QListWidgetItem_SetIcon(QtHandle, item.Index, icon);
                }
            }
        }

        private void RecreateHandle()
        {
            // Save parent and visibility state
            var parent = Parent;
            var wasVisible = Visible;
            
            // Destroy old handle
            if (QtHandle != IntPtr.Zero)
            {
                // Clear native item references
                foreach (ListViewItem item in _items)
                {
                    item._nativeItem = IntPtr.Zero;
                }

                NativeMethods.QWidget_Destroy(QtHandle);
                QtHandle = IntPtr.Zero;
            }

            // Create new handle
            CreateHandle();

            // Restore parent relationship
            if (parent != null && parent.IsHandleCreated)
            {
                NativeMethods.QWidget_SetParent(QtHandle, parent.QtHandle);
                NativeMethods.QWidget_Move(QtHandle, Location.X, Location.Y);
                NativeMethods.QWidget_Resize(QtHandle, Size.Width, Size.Height);
            }

            // Restore visibility
            if (wasVisible)
            {
                NativeMethods.QWidget_Show(QtHandle);
            }
        }

        internal void CreateNativeItem(ListViewItem item)
        {
            if (!IsHandleCreated || item._nativeItem != IntPtr.Zero)
                return;

            if (_isDetailsView)
            {
                // Create tree widget item
                item._nativeItem = NativeMethods.QTreeWidget_AddTopLevelItem(QtHandle, item.Text);
                
                // Set subitems
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    NativeMethods.QTreeWidgetItem_SetText(item._nativeItem, i + 1, item.SubItems[i].Text);
                }

                // Set icon
                UpdateItemIcon(item);
            }
            else
            {
                // Add to list widget
                NativeMethods.QListWidget_AddItem(QtHandle, item.Text);
                // Note: QListWidget doesn't return item pointer, so we track by index
                item._nativeItem = (IntPtr)(item.Index + 1); // Use index+1 as a pseudo-handle

                // Set icon
                UpdateItemIcon(item);
            }
        }

        internal void UpdateItem(ListViewItem item)
        {
            if (!IsHandleCreated || item._nativeItem == IntPtr.Zero)
                return;

            if (_isDetailsView)
            {
                NativeMethods.QTreeWidgetItem_SetText(item._nativeItem, 0, item.Text);
                
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    NativeMethods.QTreeWidgetItem_SetText(item._nativeItem, i + 1, item.SubItems[i].Text);
                }
            }
            else
            {
                // For QListWidget, we need to remove and re-add
                // This is a limitation of the current implementation
                if (item.Index >= 0)
                {
                    NativeMethods.QListWidget_RemoveItem(QtHandle, item.Index);
                    NativeMethods.QListWidget_InsertItem(QtHandle, item.Index, item.Text);
                }
            }
        }

        internal void UpdateColumnHeader(ColumnHeader column)
        {
            if (!IsHandleCreated || !_isDetailsView)
                return;

            UpdateColumns();
        }

        private void UpdateColumns()
        {
            if (!IsHandleCreated || !_isDetailsView)
                return;

            NativeMethods.QTreeWidget_SetColumnCount(QtHandle, _columns.Count);

            string[] labels = new string[_columns.Count];
            
            for (int i = 0; i < _columns.Count; i++)
            {
                labels[i] = _columns[i].Text;
            }

            NativeMethods.QTreeWidget_SetHeaderLabels(QtHandle, labels, labels.Length);

            for (int i = 0; i < _columns.Count; i++)
            {
                NativeMethods.QTreeWidget_SetColumnWidth(QtHandle, i, _columns[i].Width);
            }
        }

        [Obsolete(NotImplementedWarning)] public bool HideSelection { get; set; }
        [Obsolete(NotImplementedWarning)] public bool UseCompatibleStateImageBehavior { get; set; }

        public class ListViewItemCollection : IList
        {
            private readonly ListView _owner;
            private readonly List<ListViewItem> _items = new List<ListViewItem>();

            internal ListViewItemCollection(ListView owner)
            {
                _owner = owner;
            }

            public ListViewItem this[int index]
            {
                get => _items[index];
                set => _items[index] = value;
            }

            public int Count => _items.Count;

            public bool IsReadOnly => false;

            public bool IsFixedSize => false;

            public object SyncRoot => ((ICollection)_items).SyncRoot;

            public bool IsSynchronized => false;

            object? IList.this[int index]
            {
                get => this[index];
                set => this[index] = (ListViewItem)value!;
            }

            public ListViewItem Add(string text)
            {
                var item = new ListViewItem(text);
                Add(item);
                return item;
            }

            public ListViewItem Add(ListViewItem item)
            {
                item.Index = _items.Count;
                item.ListView = _owner;
                _items.Add(item);
                
                if (_owner.IsHandleCreated)
                {
                    _owner.CreateNativeItem(item);
                }
                
                return item;
            }

            public void Clear()
            {
                if (_owner.IsHandleCreated)
                {
                    if (_owner._isDetailsView)
                    {
                        NativeMethods.QTreeWidget_Clear(_owner.QtHandle);
                    }
                    else
                    {
                        NativeMethods.QListWidget_Clear(_owner.QtHandle);
                    }
                }

                foreach (var item in _items)
                {
                    item._nativeItem = IntPtr.Zero;
                    item.ListView = null;
                }

                _items.Clear();
            }

            public bool Contains(ListViewItem item) => _items.Contains(item);

            public int IndexOf(ListViewItem item) => _items.IndexOf(item);

            public void RemoveAt(int index)
            {
                var item = _items[index];
                
                if (_owner.IsHandleCreated && item._nativeItem != IntPtr.Zero)
                {
                    if (_owner._isDetailsView)
                    {
                        NativeMethods.QTreeWidget_RemoveTopLevelItem(_owner.QtHandle, item._nativeItem);
                    }
                    else
                    {
                        NativeMethods.QListWidget_RemoveItem(_owner.QtHandle, index);
                    }
                }

                item._nativeItem = IntPtr.Zero;
                item.ListView = null;
                _items.RemoveAt(index);

                // Update indices
                for (int i = index; i < _items.Count; i++)
                {
                    _items[i].Index = i;
                }
            }

            public IEnumerator GetEnumerator() => _items.GetEnumerator();

            int IList.Add(object? value)
            {
                Add((ListViewItem)value!);
                return _items.Count - 1;
            }

            bool IList.Contains(object? value) => Contains((ListViewItem)value!);

            int IList.IndexOf(object? value) => IndexOf((ListViewItem)value!);

            void IList.Insert(int index, object? value) => throw new NotImplementedException();

            void IList.Remove(object? value) => throw new NotImplementedException();

            void ICollection.CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
        }

        public class ColumnHeaderCollection : IList
        {
            private readonly ListView _owner;
            private readonly List<ColumnHeader> _items = new List<ColumnHeader>();

            internal ColumnHeaderCollection(ListView owner)
            {
                _owner = owner;
            }

            public ColumnHeader this[int index]
            {
                get => _items[index];
                set => _items[index] = value;
            }

            public int Count => _items.Count;

            public bool IsReadOnly => false;

            public bool IsFixedSize => false;

            public object SyncRoot => ((ICollection)_items).SyncRoot;

            public bool IsSynchronized => false;

            object? IList.this[int index]
            {
                get => this[index];
                set => this[index] = (ColumnHeader)value!;
            }

            public ColumnHeader Add(string text)
            {
                var column = new ColumnHeader(text);
                Add(column);
                return column;
            }

            public ColumnHeader Add(string text, int width)
            {
                var column = new ColumnHeader(text) { Width = width };
                Add(column);
                return column;
            }

            public void AddRange(ColumnHeader[] columns)
            {
                foreach (var col in columns)
                {
                    Add(col);
                }
            }

            public int Add(ColumnHeader column)
            {
                column.Index = _items.Count;
                column.ListView = _owner;
                _items.Add(column);
                
                if (_owner.IsHandleCreated && _owner._isDetailsView)
                {
                    _owner.UpdateColumns();
                }
                
                return _items.Count - 1;
            }

            public void Clear()
            {
                foreach (var column in _items)
                {
                    column.ListView = null;
                }

                _items.Clear();

                if (_owner.IsHandleCreated && _owner._isDetailsView)
                {
                    _owner.UpdateColumns();
                }
            }

            public bool Contains(ColumnHeader item) => _items.Contains(item);

            public int IndexOf(ColumnHeader item) => _items.IndexOf(item);

            public void RemoveAt(int index)
            {
                var column = _items[index];
                column.ListView = null;
                _items.RemoveAt(index);

                // Update indices
                for (int i = index; i < _items.Count; i++)
                {
                    _items[i].Index = i;
                }

                if (_owner.IsHandleCreated && _owner._isDetailsView)
                {
                    _owner.UpdateColumns();
                }
            }

            public IEnumerator GetEnumerator() => _items.GetEnumerator();

            int IList.Add(object? value)
            {
                return Add((ColumnHeader)value!);
            }

            bool IList.Contains(object? value) => Contains((ColumnHeader)value!);

            int IList.IndexOf(object? value) => IndexOf((ColumnHeader)value!);

            void IList.Insert(int index, object? value) => throw new NotImplementedException();

            void IList.Remove(object? value) => throw new NotImplementedException();

            void ICollection.CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
        }
    }
}
