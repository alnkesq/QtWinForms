using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
    public class MenuStrip : Control
    {
        public MenuStrip()
        {
            Dock = DockStyle.Top;
        }
        private readonly List<ToolStripMenuItem> _items = new List<ToolStripMenuItem>();
        protected override Size DefaultSize => new Size(200, 24);

        protected override void CreateHandle()
        {

            // MenuStrip doesn't have its own widget - it's added to a form
            // We'll create a QMenuBar when this is added to a form
            QtHandle = NativeMethods.QMenuBar_Create(IntPtr.Zero);
            SetCommonProperties();

            // Add any items that were added before handle creation
            foreach (var item in _items)
            {
                if (!item.IsHandleCreated)
                {
                    item.CreateControl();
                }
                AddItemToMenuBar(item);
            }

        }

        private void AddItemToMenuBar(ToolStripMenuItem item)
        {
            // Always use AddAction, as ToolStripMenuItem now wraps everything in a QAction
            NativeMethods.QMenuBar_AddAction(QtHandle, item.QtHandle);
        }

        public ToolStripItemCollection Items => new ToolStripItemCollectionImpl(this);


        private class ToolStripItemCollectionImpl : ToolStripItemCollection
        {
            private readonly MenuStrip _owner;

            internal ToolStripItemCollectionImpl(MenuStrip owner)
            {
                _owner = owner;
            }

            public override void Add(ToolStripItem item)
            {
                var menuItem = (ToolStripMenuItem)item;
                _owner._items.Add(menuItem);

                if (_owner.IsHandleCreated)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.CreateControl();
                    }
                    _owner.AddItemToMenuBar(menuItem);
                }
            }

            public override ToolStripMenuItem Add(string text)
            {
                var item = new ToolStripMenuItem { Text = text };
                Add(item);
                return item;
            }
        }
    }
}
