using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ListBox : Control
    {
        private ObjectCollection _items;
        private SelectionMode _selectionMode = SelectionMode.One;
        private EventHandler? _selectedIndexChanged;


        [Obsolete(NotImplementedWarning)] public bool FormattingEnabled { get; set; }

        public ListBox()
        {
            _items = new ObjectCollection(this);
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QListWidget_Create(IntPtr.Zero);
                SetCommonProperties();

                // Apply items
                foreach (var item in _items)
                {
                    NativeMethods.QListWidget_AddItem(QtHandle, item?.ToString() ?? "");
                }

                // Apply SelectionMode
                NativeMethods.QListWidget_SetSelectionMode(QtHandle, (int)_selectionMode);

                ConnectSelectedIndexChanged();
            }
        }

        public ObjectCollection Items => _items;

        public SelectionMode SelectionMode
        {
            get => _selectionMode;
            set
            {
                if (_selectionMode != value)
                {
                    _selectionMode = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QListWidget_SetSelectionMode(QtHandle, (int)value);
                    }
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (IsHandleCreated)
                {
                    return NativeMethods.QListWidget_GetCurrentRow(QtHandle);
                }
                return -1;
            }
            set
            {
                if (IsHandleCreated)
                {
                    NativeMethods.QListWidget_SetCurrentRow(QtHandle, value);
                }
            }
        }

        public object? SelectedItem
        {
            get
            {
                int index = SelectedIndex;
                if (index >= 0 && index < _items.Count)
                {
                    return _items[index];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    int index = _items.IndexOf(value);
                    if (index != -1)
                    {
                        SelectedIndex = index;
                    }
                }
            }
        }

        public unsafe SelectedIndexCollection SelectedIndices
        {
            get
            {
                if (!IsHandleCreated)
                {
                    return new SelectedIndexCollection(Array.Empty<int>());
                }

                using var box = new GCHandle<int[]>(Array.Empty<int>());

                [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
                static void Callback(int* indices, int count, void* userData)
                {
                    var box = GCHandle<int[]>.FromIntPtr((nint)userData);
                    int[] result = new int[count];
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = indices[i];
                    }
                    box.Target = result;
                }

                NativeMethods.QListWidget_GetSelectedRows(QtHandle, &Callback, GCHandle<int[]>.ToIntPtr(box));
                return new SelectedIndexCollection(box.Target ?? Array.Empty<int>());
            }
        }

        public SelectedObjectCollection SelectedItems
        {
            get
            {
                var indices = SelectedIndices;
                var items = new object[indices.Count];
                for (int i = 0; i < indices.Count; i++)
                {
                    items[i] = _items[indices[i]];
                }
                return new SelectedObjectCollection(items);
            }
        }

        public event EventHandler? SelectedIndexChanged
        {
            add => _selectedIndexChanged += value;
            remove => _selectedIndexChanged -= value;
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            _selectedIndexChanged?.Invoke(this, e);
        }

        private unsafe void ConnectSelectedIndexChanged()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnSelectedIndexChangedCallback;
            NativeMethods.QListWidget_ConnectCurrentRowChanged(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnSelectedIndexChangedCallback(nint userData)
        {
            var control = ObjectFromGCHandle<ListBox>(userData);
            control.OnSelectedIndexChanged(EventArgs.Empty);
        }

#pragma warning disable CS8767
        public class ObjectCollection : IList
        {
            private ListBox _owner;
            private ArrayList _innerList = new ArrayList();

            public ObjectCollection(ListBox owner)
            {
                _owner = owner;
            }

            public int Add(object item)
            {
                int index = _innerList.Add(item);
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QListWidget_AddItem(_owner.QtHandle, item?.ToString() ?? "");
                }
                return index;
            }

            public void AddRange(IEnumerable items)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
            public void Clear()
            {
                _innerList.Clear();
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QListWidget_Clear(_owner.QtHandle);
                }
            }

            public bool Contains(object item) => _innerList.Contains(item);
            public int IndexOf(object item) => _innerList.IndexOf(item);

            public void Insert(int index, object item)
            {
                _innerList.Insert(index, item);
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QListWidget_InsertItem(_owner.QtHandle, index, item?.ToString() ?? "");
                }
            }

            public void Remove(object item)
            {
                int index = IndexOf(item);
                if (index != -1)
                {
                    RemoveAt(index);
                }
            }

            public void RemoveAt(int index)
            {
                _innerList.RemoveAt(index);
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QListWidget_RemoveItem(_owner.QtHandle, index);
                }
            }

            public object this[int index]
            {
                get => _innerList[index]!;
                set
                {
                    _innerList[index] = value;
                    if (_owner.IsHandleCreated)
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            public int Count => _innerList.Count;
            public bool IsReadOnly => _innerList.IsReadOnly;
            public bool IsFixedSize => _innerList.IsFixedSize;
            public object SyncRoot => _innerList.SyncRoot;
            public bool IsSynchronized => _innerList.IsSynchronized;

            public void CopyTo(Array array, int index) => _innerList.CopyTo(array, index);
            public IEnumerator GetEnumerator() => _innerList.GetEnumerator();
        }
#pragma warning restore CS8767
        public class SelectedIndexCollection : IList
        {
            private int[] _indices;

            internal SelectedIndexCollection(int[] indices)
            {
                _indices = indices;
            }

            public int this[int index] => _indices[index];

            object? IList.this[int index]
            {
                get => _indices[index];
                set => throw new NotSupportedException();
            }

            public int Count => _indices.Length;
            public bool IsReadOnly => true;
            public bool IsFixedSize => true;
            public object SyncRoot => _indices.SyncRoot;
            public bool IsSynchronized => _indices.IsSynchronized;

            public bool Contains(object? value) => value is int i && Array.IndexOf(_indices, i) >= 0;
            public int IndexOf(object? value) => value is int i ? Array.IndexOf(_indices, i) : -1;
            public void CopyTo(Array array, int index) => _indices.CopyTo(array, index);
            public IEnumerator GetEnumerator() => _indices.GetEnumerator();

            public int Add(object? value) => throw new NotSupportedException();
            public void Clear() => throw new NotSupportedException();
            public void Insert(int index, object? value) => throw new NotSupportedException();
            public void Remove(object? value) => throw new NotSupportedException();
            public void RemoveAt(int index) => throw new NotSupportedException();
        }

        public class SelectedObjectCollection : IList
        {
            private object[] _items;

            internal SelectedObjectCollection(object[] items)
            {
                _items = items;
            }

            public object this[int index] => _items[index];

            object? IList.this[int index]
            {
                get => _items[index];
                set => throw new NotSupportedException();
            }

            public int Count => _items.Length;
            public bool IsReadOnly => true;
            public bool IsFixedSize => true;
            public object SyncRoot => _items.SyncRoot;
            public bool IsSynchronized => _items.IsSynchronized;

            public bool Contains(object? value) => Array.IndexOf(_items, value) >= 0;
            public int IndexOf(object? value) => Array.IndexOf(_items, value);
            public void CopyTo(Array array, int index) => _items.CopyTo(array, index);
            public IEnumerator GetEnumerator() => _items.GetEnumerator();

            public int Add(object? value) => throw new NotSupportedException();
            public void Clear() => throw new NotSupportedException();
            public void Insert(int index, object? value) => throw new NotSupportedException();
            public void Remove(object? value) => throw new NotSupportedException();
            public void RemoveAt(int index) => throw new NotSupportedException();
        }
    }

    public enum SelectionMode
    {
        None = 0,
        One = 1,
        MultiSimple = 2,
        MultiExtended = 3
    }
}
