using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class TreeViewEventArgs : EventArgs
    {
        public TreeNode? Node { get; }
        public TreeViewAction Action { get; }

        public TreeViewEventArgs(TreeNode? node)
        {
            Node = node;
            Action = TreeViewAction.Unknown;
        }

        public TreeViewEventArgs(TreeNode? node, TreeViewAction action)
        {
            Node = node;
            Action = action;
        }
    }
}
