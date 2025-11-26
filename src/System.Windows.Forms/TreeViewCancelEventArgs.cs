using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Windows.Forms
{
    public class TreeViewCancelEventArgs : CancelEventArgs
    {
        public TreeNode? Node { get; }

        public TreeViewAction Action { get; }

        public TreeViewCancelEventArgs(TreeNode? node, bool cancel, TreeViewAction action)
            : base(cancel)
        {
            Node = node;
            Action = action;
        }
    }

}
