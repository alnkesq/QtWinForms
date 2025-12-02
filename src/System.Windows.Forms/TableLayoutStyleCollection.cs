using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class TableLayoutStyleCollection : IList
    {
        private readonly List<TableLayoutStyle> _items = new List<TableLayoutStyle>();
        private readonly TableLayoutPanel? _owner;

        internal TableLayoutStyleCollection(TableLayoutPanel? owner = null)
        {
            _owner = owner;
        }

        public int Count => _items.Count;

        public TableLayoutStyle this[int index]
        {
            get => _items[index];
            set
            {
                _items[index] = value;
                _owner?.ApplyStyles();
            }
        }

        public int Add(TableLayoutStyle style)
        {
            _items.Add(style);
            _owner?.ApplyStyles();
            return _items.Count - 1;
        }

        public void Clear()
        {
            _items.Clear();
            _owner?.ApplyStyles();
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            _owner?.ApplyStyles();
        }

        // IList implementation
        bool IList.IsFixedSize => false;
        bool IList.IsReadOnly => false;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;

        object? IList.this[int index]
        {
            get => this[index];
            set => this[index] = (TableLayoutStyle)value!;
        }

        int IList.Add(object? value)
        {
            return Add((TableLayoutStyle)value!);
        }

        bool IList.Contains(object? value) => _items.Contains((TableLayoutStyle)value!);
        int IList.IndexOf(object? value) => _items.IndexOf((TableLayoutStyle)value!);
        void IList.Insert(int index, object? value) => _items.Insert(index, (TableLayoutStyle)value!);
        void IList.Remove(object? value) => _items.Remove((TableLayoutStyle)value!);
        void ICollection.CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }
}
