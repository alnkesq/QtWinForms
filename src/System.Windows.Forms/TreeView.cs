using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TreeView : Control
    {
        private RootTreeNodeCollection _nodes;
        private TreeNode? _selectedNode;

        public TreeView()
        {
            _nodes = new RootTreeNodeCollection(this);
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QTreeWidget_Create(IntPtr.Zero);
                SetCommonProperties();

                // Add all existing nodes
                foreach (TreeNode node in _nodes)
                {
                    node._treeView = this;
                    node.EnsureNativeItem();
                }

                ConnectItemSelectionChanged();
            }
        }

        public TreeNode.TreeNodeCollection Nodes => _nodes;

        public TreeNode? SelectedNode
        {
            get
            {
                if (IsHandleCreated)
                {
                    IntPtr nativeItem = NativeMethods.QTreeWidget_GetCurrentItem(Handle);
                    if (nativeItem != IntPtr.Zero)
                    {
                        return FindNodeByNativeItem(nativeItem);
                    }
                }
                return _selectedNode;
            }
            set
            {
                _selectedNode = value;
                if (IsHandleCreated && value != null)
                {
                    value.EnsureNativeItem();
                    if (value._nativeItem != IntPtr.Zero)
                    {
                        NativeMethods.QTreeWidget_SetCurrentItem(Handle, value._nativeItem);
                    }
                }
            }
        }

        private TreeNode? FindNodeByNativeItem(IntPtr nativeItem)
        {
            return FindNodeByNativeItemRecursive(_nodes, nativeItem);
        }

        private TreeNode? FindNodeByNativeItemRecursive(TreeNode.TreeNodeCollection nodes, IntPtr nativeItem)
        {
            foreach (TreeNode node in nodes)
            {
                if (node._nativeItem == nativeItem)
                {
                    return node;
                }
                
                TreeNode? found = FindNodeByNativeItemRecursive(node.Nodes, nativeItem);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }

        private unsafe void ConnectItemSelectionChanged()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnItemSelectionChangedCallback;
            NativeMethods.QTreeWidget_ConnectItemSelectionChanged(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnItemSelectionChangedCallback(nint userData)
        {
            var control = ObjectFromGCHandle<TreeView>(userData);
            IntPtr nativeItem = NativeMethods.QTreeWidget_GetCurrentItem(control.Handle);
            control._selectedNode = control.FindNodeByNativeItem(nativeItem);
        }

        // Special collection for root nodes that doesn't have a parent TreeNode
        internal class RootTreeNodeCollection : TreeNode.TreeNodeCollection
        {
            private TreeView _treeView;

            internal RootTreeNodeCollection(TreeView treeView) : base(null!)
            {
                _treeView = treeView;
            }

            protected override TreeView? GetTreeView() => _treeView;
            protected override TreeNode? GetParentNode() => null;
        }
    }
}
