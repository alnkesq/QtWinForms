using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
    public class MenuStrip : Control
    {
        private readonly List<ToolStripMenuItem> _items = new List<ToolStripMenuItem>();

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // MenuStrip doesn't have its own widget - it's added to a form
                // We'll create a QMenuBar when this is added to a form
                Handle = NativeMethods.QMenuBar_Create(IntPtr.Zero);
                SetCommonProperties();

                // Add any items that were added before handle creation
                foreach (var item in _items)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.EnsureCreated();
                    }
                    AddItemToMenuBar(item);
                }
            }
        }

        private void AddItemToMenuBar(ToolStripMenuItem item)
        {
            // Always use AddAction, as ToolStripMenuItem now wraps everything in a QAction
            NativeMethods.QMenuBar_AddAction(Handle, item.Handle);
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
                        item.EnsureCreated();
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
