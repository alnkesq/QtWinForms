using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ComboBox : Control
    {
        private EventHandler? _selectedIndexChanged;
        private ObjectCollection _items;
        private ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDown;
        private int _selectedIndex = -1;
        private string _text = string.Empty;

        public ComboBox()
        {
            _items = new ObjectCollection(this);
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QComboBox_Create(IntPtr.Zero);
                SetCommonProperties();

                // Apply items
                foreach (var item in _items)
                {
                    NativeMethods.QComboBox_AddItem(Handle, item?.ToString() ?? "");
                }

                // Apply DropDownStyle
                // We only support DropDownList as per request, but we can set editable state based on it.
                // DropDownList -> Not Editable
                // DropDown -> Editable
                bool editable = _dropDownStyle == ComboBoxStyle.DropDown; 
                // However, user said "you only need to implement the ComboBoxStyle.DropDownList mode".
                // So I will ensure it behaves as DropDownList (not editable) if that style is set.
                
                if (_dropDownStyle == ComboBoxStyle.DropDownList)
                {
                    NativeMethods.QComboBox_SetEditable(Handle, false);
                }
                else
                {
                    NativeMethods.QComboBox_SetEditable(Handle, true);
                }

                if (_selectedIndex != -1)
                {
                    NativeMethods.QComboBox_SetSelectedIndex(Handle, _selectedIndex);
                }

                if (!string.IsNullOrEmpty(_text))
                {
                    NativeMethods.QComboBox_SetText(Handle, _text);
                }

                ConnectSelectedIndexChanged();
                ConnectCurrentTextChanged();
            }
        }

        public ComboBoxStyle DropDownStyle
        {
            get => _dropDownStyle;
            set
            {
                if (_dropDownStyle != value)
                {
                    _dropDownStyle = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QComboBox_SetEditable(Handle, _dropDownStyle == ComboBoxStyle.DropDown);
                    }
                }
            }
        }

        public ObjectCollection Items => _items;

        public int SelectedIndex
        {
            get
            {
                if (IsHandleCreated)
                {
                    return NativeMethods.QComboBox_GetSelectedIndex(Handle);
                }
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    if (IsHandleCreated)
                    {
                        NativeMethods.QComboBox_SetSelectedIndex(Handle, value);
                    }
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        public unsafe override string Text
        {
            get
            {
                if (!IsHandleCreated) return _text;
                using var box = new GCHandle<string>(string.Empty);

                [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
                static void Callback(void* utf16, int length, void* userData)
                {
                    var box = GCHandle<string?>.FromIntPtr((nint)userData);
                    string s = Marshal.PtrToStringUni((nint)utf16, length);
                    box.Target = s;
                }
                NativeMethods.QComboBox_GetText_Invoke(Handle, &Callback, GCHandle<string>.ToIntPtr(box));
                return box.Target ?? string.Empty;
            }
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QComboBox_SetText(Handle, _text);
                }
            }
        }

        public event EventHandler? TextChanged;

        protected virtual void OnTextChanged(EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        private unsafe void ConnectCurrentTextChanged()
        {
            delegate* unmanaged[Cdecl]<void*, int, nint, void> callback = &OnCurrentTextChangedCallback;
            NativeMethods.QComboBox_ConnectCurrentTextChanged(Handle, callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnCurrentTextChangedCallback(void* dataUtf16, int length, nint userData)
        {
            var control = ObjectFromGCHandle<ComboBox>(userData);
            string text = new string((char*)dataUtf16, 0, length);
            control._text = text;
            control.OnTextChanged(EventArgs.Empty);
        }

        public event EventHandler SelectedIndexChanged
        {
            add
            {
                _selectedIndexChanged += value;
            }
            remove
            {
                _selectedIndexChanged -= value;
            }
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            _selectedIndexChanged?.Invoke(this, e);
        }

        private unsafe void ConnectSelectedIndexChanged()
        {
            delegate* unmanaged[Cdecl]<nint, int, void> callback = &OnSelectedIndexChangedCallback;
            NativeMethods.QComboBox_ConnectSelectedIndexChanged(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static unsafe void OnSelectedIndexChangedCallback(nint userData, int index)
        {
            var control = ObjectFromGCHandle<ComboBox>(userData);
            control._selectedIndex = index; // Update internal cache
            control.OnSelectedIndexChanged(EventArgs.Empty);
        }

        public class ObjectCollection : IList
        {
            private ComboBox _owner;
            private ArrayList _innerList = new ArrayList();

            public ObjectCollection(ComboBox owner)
            {
                _owner = owner;
            }

            public int Add(object item)
            {
                int index = _innerList.Add(item);
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QComboBox_AddItem(_owner.Handle, item?.ToString() ?? "");
                }
                return index;
            }

            public void Clear()
            {
                _innerList.Clear();
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QComboBox_Clear(_owner.Handle);
                }
            }

            public bool Contains(object item) => _innerList.Contains(item);
            public int IndexOf(object item) => _innerList.IndexOf(item);
            public void Insert(int index, object item)
            {
                _innerList.Insert(index, item);
                // QComboBox doesn't easily support insert without rebuilding or using model, 
                // but for now let's assume append only or rebuild.
                // Actually QComboBox has insertItem.
                if (_owner.IsHandleCreated)
                {
                    NativeMethods.QComboBox_InsertItem(_owner.Handle, index, item?.ToString() ?? "");
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
                    NativeMethods.QComboBox_RemoveItem(_owner.Handle, index);
                }
            }

            public object this[int index]
            {
                get => _innerList[index];
                set
                {
                    _innerList[index] = value;
                    // Update item at index
                    // QComboBox setItemText
                    if (_owner.IsHandleCreated)
                    {
                        // NativeMethods.QComboBox_SetItemText(_owner.Handle, index, value.ToString());
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
    }
}
