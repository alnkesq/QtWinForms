using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows.Forms
{
    public class TreeNodeMouseClickEventArgs : MouseEventArgs
    {
        public TreeNode? Node { get; }

        public TreeNodeMouseClickEventArgs(TreeNode? node, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Node = node;
        }
    }

}
