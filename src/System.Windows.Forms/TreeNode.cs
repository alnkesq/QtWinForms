using System.Collections;

namespace System.Windows.Forms
{
    public class TreeNode
    {
        private string _text = "";
        private TreeNodeCollection? _nodes;
        internal TreeView? _treeView;
        internal IntPtr _nativeItem = IntPtr.Zero;
        internal TreeNode? _parent;

        public TreeNode()
        {
        }

        public TreeNode(string text)
        {
            _text = text;
        }

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value ?? string.Empty;
                    if (_treeView != null && _treeView.IsHandleCreated && _nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidgetItem_SetText(_nativeItem, 0, _text);
                    }
                }
            }
        }

        public TreeNodeCollection Nodes
        {
            get
            {
                if (_nodes == null)
                {
                    _nodes = new TreeNodeCollection(this);
                }
                return _nodes;
            }
        }

        internal void EnsureNativeItem()
        {
            if (_nativeItem == IntPtr.Zero && _treeView != null && _treeView.IsHandleCreated)
            {
                if (_parent == null)
                {
                    // Top-level node
                    _nativeItem = NativeMethods.QTreeWidget_AddTopLevelItem(_treeView.Handle, _text);
                }
                else
                {
                    // Child node
                    _parent.EnsureNativeItem();
                    _nativeItem = NativeMethods.QTreeWidgetItem_AddChild(_parent._nativeItem, _text);
                }

                // Add any existing child nodes
                if (_nodes != null)
                {
                    foreach (TreeNode child in _nodes)
                    {
                        child.EnsureNativeItem();
                    }
                }
            }
        }

        public void Expand()
        {
            if (_nativeItem != IntPtr.Zero)
            {
                NativeMethods.QTreeWidgetItem_SetExpanded(_nativeItem, true);
            }
        }

        public void Collapse()
        {
            if (_nativeItem != IntPtr.Zero)
            {
                NativeMethods.QTreeWidgetItem_SetExpanded(_nativeItem, false);
            }
        }

        public void ExpandAll()
        {
            Expand();
            foreach (TreeNode child in Nodes)
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            Collapse();
            foreach (TreeNode child in Nodes)
            {
                child.CollapseAll();
            }
        }

#pragma warning disable CS8767
        public class TreeNodeCollection : IList
        {
            private TreeNode? _owner;
            private ArrayList _innerList = new ArrayList();

            internal TreeNodeCollection(TreeNode? owner)
            {
                _owner = owner;
            }

            protected virtual TreeView? GetTreeView() => _owner?._treeView;
            protected virtual TreeNode? GetParentNode() => _owner;

            public TreeNode Add(string text)
            {
                TreeNode node = new TreeNode(text);
                Add(node);
                return node;
            }

            public int Add(object? value)
            {
                if (value is TreeNode node)
                {
                    int index = _innerList.Add(node);
                    node._parent = GetParentNode();
                    node._treeView = GetTreeView();
                    
                    if (GetTreeView() != null && GetTreeView()!.IsHandleCreated)
                    {
                        node.EnsureNativeItem();
                    }
                    
                    return index;
                }
                throw new ArgumentException("Value must be a TreeNode");
            }

            public void Clear()
            {
                foreach (TreeNode node in _innerList)
                {
                    if (node._nativeItem != IntPtr.Zero && _owner != null && _owner._nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidgetItem_RemoveChild(_owner._nativeItem, node._nativeItem);
                        node._nativeItem = IntPtr.Zero;
                    }
                    node._parent = null;
                    node._treeView = null;
                }
                _innerList.Clear();
            }

            public bool Contains(object? value) => _innerList.Contains(value);
            public int IndexOf(object? value) => _innerList.IndexOf(value);

            public void Insert(int index, object? value)
            {
                if (value is TreeNode node)
                {
                    _innerList.Insert(index, node);
                    node._parent = GetParentNode();
                    node._treeView = GetTreeView();
                    
                    if (GetTreeView() != null && GetTreeView()!.IsHandleCreated)
                    {
                        node.EnsureNativeItem();
                    }
                }
                else
                {
                    throw new ArgumentException("Value must be a TreeNode");
                }
            }

            public void Remove(object? value)
            {
                if (value is TreeNode node)
                {
                    int index = IndexOf(node);
                    if (index != -1)
                    {
                        RemoveAt(index);
                    }
                }
            }

            public void RemoveAt(int index)
            {
                TreeNode node = (TreeNode)_innerList[index]!;
                if (node._nativeItem != IntPtr.Zero && _owner != null && _owner._nativeItem != IntPtr.Zero)
                {
                    NativeMethods.QTreeWidgetItem_RemoveChild(_owner._nativeItem, node._nativeItem);
                    node._nativeItem = IntPtr.Zero;
                }
                node._parent = null;
                node._treeView = null;
                _innerList.RemoveAt(index);
            }

            public TreeNode this[int index]
            {
                get => (TreeNode)_innerList[index]!;
                set
                {
                    TreeNode oldNode = (TreeNode)_innerList[index]!;
                    if (oldNode._nativeItem != IntPtr.Zero && _owner != null && _owner._nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidgetItem_RemoveChild(_owner._nativeItem, oldNode._nativeItem);
                        oldNode._nativeItem = IntPtr.Zero;
                    }
                    oldNode._parent = null;
                    oldNode._treeView = null;

                    _innerList[index] = value;
                    value._parent = GetParentNode();
                    value._treeView = GetTreeView();
                    
                    if (GetTreeView() != null && GetTreeView()!.IsHandleCreated)
                    {
                        value.EnsureNativeItem();
                    }
                }
            }

            object? IList.this[int index]
            {
                get => this[index];
                set
                {
                    if (value is TreeNode node)
                    {
                        this[index] = node;
                    }
                    else
                    {
                        throw new ArgumentException("Value must be a TreeNode");
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
    }
}
