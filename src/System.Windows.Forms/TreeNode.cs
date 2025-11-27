using System.Collections;
using System.Drawing;

namespace System.Windows.Forms
{
    public class TreeNode : ITreeNodeOrTreeView
    {
        private string _text = "";
        private TreeNodeCollection? _nodes;
        internal IntPtr _nativeItem = IntPtr.Zero;
        internal ITreeNodeOrTreeView? _parent;

        public TreeNode? Parent => _parent as TreeNode;

        public object? Tag { get; set; }

        private int _selectedImageIndex = -1;
        private string _selectedImageKey = string.Empty;

        public int SelectedImageIndex
        {
            get
            {
                if (_selectedImageIndex == -1 && !string.IsNullOrEmpty(_selectedImageKey))
                {
                    TreeView? tv = GetTreeView();
                    if (tv?.ImageList?.Images.nameToIndex != null &&
                        tv.ImageList.Images.nameToIndex.TryGetValue(_selectedImageKey, out int index))
                    {
                        return index;
                    }
                }
                return _selectedImageIndex;
            }
            set
            {
                if (_selectedImageIndex != value)
                {
                    _selectedImageIndex = value;
                    _selectedImageKey = string.Empty;
                    UpdateIcon();
                }
            }
        }

        public string SelectedImageKey
        {
            get => _selectedImageKey;
            set
            {
                if (_selectedImageKey != value)
                {
                    _selectedImageKey = value ?? string.Empty;
                    _selectedImageIndex = -1;
                    UpdateIcon();
                }
            }
        }

        private int _imageIndex = -1;
        private string _imageKey = string.Empty;

        public int ImageIndex
        {
            get
            {
                if (_imageIndex == -1 && !string.IsNullOrEmpty(_imageKey))
                {
                    TreeView? tv = GetTreeView();
                    if (tv?.ImageList?.Images.nameToIndex != null &&
                        tv.ImageList.Images.nameToIndex.TryGetValue(_imageKey, out int index))
                    {
                        return index;
                    }
                }
                return _imageIndex;
            }
            set
            {
                if (_imageIndex != value)
                {
                    _imageIndex = value;
                    _imageKey = string.Empty;
                    UpdateIcon();
                }
            }
        }

        public string ImageKey
        {
            get => _imageKey;
            set
            {
                if (_imageKey != value)
                {
                    _imageKey = value ?? string.Empty;
                    _imageIndex = -1;
                    UpdateIcon();
                }
            }
        }

        public bool IsExpanded => _nativeItem != default && NativeMethods.QTreeWidgetItem_IsExpanded(_nativeItem) != 0;

        [Obsolete(Control.NotImplementedWarning)] public Color ForeColor { get; set; }
        [Obsolete(Control.NotImplementedWarning)] public Color BackColor { get; set; }
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
                    if (_nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidgetItem_SetText(_nativeItem, 0, _text);
                    }
                }
            }
        }

        private string _toolTipText = "";
        public string ToolTipText
        {
            get => _toolTipText;
            set
            {
                if (_toolTipText != value)
                {
                    _toolTipText = value ?? string.Empty;
                    if (_nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidgetItem_SetToolTip(_nativeItem, 0, _toolTipText);
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

        public TreeNode? FirstNode => Nodes.Count != 0 ? Nodes[0] : null;
        public TreeNode? LastNode => Nodes.Count != 0 ? Nodes[Nodes.Count - 1] : null;

        bool ITreeNodeOrTreeView.IsNativeHandleCreated => _nativeItem != default;

        internal void EnsureNativeItem()
        {
            if (_parent == null) throw new InvalidOperationException();
            if (!_parent.IsNativeHandleCreated) throw new InvalidOperationException();
            if (_nativeItem == IntPtr.Zero)
            {
                if (_parent is TreeView treeView)
                {
                    // Top-level node
                    _nativeItem = NativeMethods.QTreeWidget_AddTopLevelItem(treeView.QtHandle, _text);
                }
                else
                {
                    // Child node
                    //_parent.EnsureNativeItem();
                    _nativeItem = NativeMethods.QTreeWidgetItem_AddChild(((TreeNode)_parent)._nativeItem, _text);
                }

                UpdateIcon();

                if (!string.IsNullOrEmpty(_toolTipText))
                {
                    NativeMethods.QTreeWidgetItem_SetToolTip(_nativeItem, 0, _toolTipText);
                }

                if (_nodes != null)
                {
                    foreach (TreeNode child in _nodes)
                    {
                        child._parent = this;
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

        public void Remove()
        {
            if (_parent == null) return;
            if (_parent is TreeNode parentNode) parentNode.Nodes.Remove(this);
            else ((TreeView)_parent).Nodes.Remove(this);
        }

#pragma warning disable CS8767
        public class TreeNodeCollection : IList
        {
            private readonly ITreeNodeOrTreeView _owner;
            private readonly ArrayList _innerList = new ArrayList();

            internal TreeNodeCollection(ITreeNodeOrTreeView owner)
            {
                _owner = owner;
            }

            internal ITreeNodeOrTreeView Owner => _owner;

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
                    node._parent = Owner;

                    if (Owner.IsNativeHandleCreated)
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
                    if (node._nativeItem != IntPtr.Zero && _owner != null && _owner.IsNativeHandleCreated)
                    {
                        NativeMethods.QTreeWidgetItem_RemoveChild(((TreeNode)_owner)._nativeItem, node._nativeItem);
                        node._nativeItem = IntPtr.Zero;
                    }
                    node._parent = null;
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
                    node._parent = Owner;

                    if (Owner.IsNativeHandleCreated)
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
                if (node._nativeItem != IntPtr.Zero && _owner != null && _owner.IsNativeHandleCreated)
                {
                    NativeMethods.QTreeWidgetItem_RemoveChild(((TreeNode)_owner)._nativeItem, node._nativeItem);
                    node._nativeItem = IntPtr.Zero;
                }
                node._parent = null;
                _innerList.RemoveAt(index);
            }

            public TreeNode this[int index]
            {
                get => (TreeNode)_innerList[index]!;
                set
                {
                    TreeNode oldNode = (TreeNode)_innerList[index]!;
                    if (oldNode._nativeItem != IntPtr.Zero && _owner != null && _owner.IsNativeHandleCreated)
                    {
                        NativeMethods.QTreeWidgetItem_RemoveChild(((TreeNode)_owner)._nativeItem, oldNode._nativeItem);
                        oldNode._nativeItem = IntPtr.Zero;
                    }
                    oldNode._parent = null;

                    _innerList[index] = value;
                    value._parent = Owner;

                    if (Owner.IsNativeHandleCreated)
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

        internal void UpdateIcon()
        {
            if (_nativeItem == IntPtr.Zero)
                return;

            // Get the TreeView
            TreeView? treeView = GetTreeView();
            if (treeView == null || treeView.ImageList == null)
                return;

            // Determine which icon to use based on selection state
            bool isSelected = treeView.SelectedNode == this;

            // Resolve indices
            int actualImageIndex = ImageIndex;
            int actualSelectedImageIndex = SelectedImageIndex;

            int iconIndex = isSelected && actualSelectedImageIndex >= 0 ? actualSelectedImageIndex : actualImageIndex;

            if (iconIndex < 0)
                return;

            // Get the QIcon from the ImageList (reused if already created)
            IntPtr qIcon = treeView.GetQIconFromImageList(iconIndex);
            if (qIcon != IntPtr.Zero)
            {
                NativeMethods.QTreeWidgetItem_SetIcon(_nativeItem, 0, qIcon);
            }
        }

        private TreeView? GetTreeView()
        {
            ITreeNodeOrTreeView? current = _parent;
            while (current != null)
            {
                if (current is TreeView tv)
                    return tv;
                if (current is TreeNode node)
                    current = node._parent;
                else
                    break;
            }
            return null;
        }
    }

    interface ITreeNodeOrTreeView
    {
        public bool IsNativeHandleCreated { get; }
    }
}
