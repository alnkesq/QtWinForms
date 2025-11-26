using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TreeView : Control, ITreeNodeOrTreeView
    {
        private TreeNode.TreeNodeCollection _nodes;
        private TreeNode? _selectedNode;

        public TreeView()
        {
            _nodes = new TreeNode.TreeNodeCollection(this);
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
                    node._parent = this;
                    node.EnsureNativeItem();
                }

                ConnectItemSelectionChanged();
                ConnectItemExpanded();
            }
        }

        public void ExpandAll()
        {
            foreach (TreeNode child in Nodes)
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            foreach (TreeNode child in Nodes)
            {
                child.CollapseAll();
            }
        }

        public void BeginUpdate() { }
        public void EndUpdate() { }

        public TreeViewCancelEventHandler? BeforeExpand;
        protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            BeforeExpand?.Invoke(this, e);
        }
        public TreeViewEventHandler? AfterExpand;
        protected virtual void OnAfterExpand(TreeViewEventArgs e)
        {
            AfterExpand?.Invoke(this, e);
        }
        public TreeViewEventHandler? AfterSelect;
        protected virtual void OnAfterSelect(TreeViewEventArgs e)
        {
            AfterSelect?.Invoke(this, e);
        }
        
        private ImageList? _imageList;
        public ImageList? ImageList 
        { 
            get => _imageList;
            set
            {
                _imageList = value;
                // When ImageList changes, update all nodes
                if (IsHandleCreated)
                {
                    UpdateAllNodeIcons();
                }
            }
        }
        
        [Obsolete(NotImplementedWarning)] public bool HideSelection { get; set; }

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

        bool ITreeNodeOrTreeView.IsNativeHandleCreated => IsHandleCreated;

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
            
            // Store the previous selected node to update its icon
            TreeNode? previousNode = control._selectedNode;
            
            control._selectedNode = control.FindNodeByNativeItem(nativeItem);
            
            // Update icons for both old and new selected nodes
            if (previousNode != null)
            {
                previousNode.UpdateIcon(); // Revert to normal icon
            }
            if (control._selectedNode != null)
            {
                control._selectedNode.UpdateIcon(); // Show selected icon
            }
            
            // Fire AfterSelect event
            if (control._selectedNode != null)
            {
                var args = new TreeViewEventArgs(control._selectedNode, TreeViewAction.ByMouse);
                control.OnAfterSelect(args);
            }
        }

        private unsafe void ConnectItemExpanded()
        {
            delegate* unmanaged[Cdecl]<nint, nint, void> callback = &OnItemExpandedCallback;
            NativeMethods.QTreeWidget_ConnectItemExpanded(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnItemExpandedCallback(nint userData, nint itemPtr)
        {
            var control = ObjectFromGCHandle<TreeView>(userData);
            TreeNode? node = control.FindNodeByNativeItem(itemPtr);
            if (node != null)
            {
                var cancelArgs = new TreeViewCancelEventArgs(node, false, TreeViewAction.Expand);
                control.OnBeforeExpand(cancelArgs);
                
                // If the event handler cancelled the expansion, collapse it back
                if (cancelArgs.Cancel)
                {
                    NativeMethods.QTreeWidgetItem_SetExpanded(itemPtr, false);
                }
                else
                {
                    // Only fire AfterExpand if the expansion wasn't cancelled
                    var args = new TreeViewEventArgs(node, TreeViewAction.Expand);
                    control.OnAfterExpand(args);
                }
            }
        }
        
        private void UpdateAllNodeIcons()
        {
            foreach (TreeNode node in _nodes)
            {
                UpdateNodeIconsRecursive(node);
            }
        }
        
        private void UpdateNodeIconsRecursive(TreeNode node)
        {
            node.UpdateIcon();
            foreach (TreeNode child in node.Nodes)
            {
                UpdateNodeIconsRecursive(child);
            }
        }
        
        internal IntPtr GetQIconFromImageList(int imageIndex)
        {
            if (_imageList == null || imageIndex < 0 || imageIndex >= _imageList.Images.Count)
                return IntPtr.Zero;
                
            var image = _imageList.Images[imageIndex];

            return image.GetQIcon();
        }
    }
}
