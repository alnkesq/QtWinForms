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
            // Check if the item has a menu (submenu)
            if (item.HasMenu)
            {
                NativeMethods.QMenuBar_AddMenu(Handle, item.MenuHandle);
            }
            else
            {
                NativeMethods.QMenuBar_AddAction(Handle, item.Handle);
            }
        }

        public ToolStripMenuItemCollection Items => new ToolStripMenuItemCollection(this);

        public class ToolStripMenuItemCollection
        {
            private readonly MenuStrip _owner;

            internal ToolStripMenuItemCollection(MenuStrip owner)
            {
                _owner = owner;
            }

            public void Add(ToolStripMenuItem item)
            {
                _owner._items.Add(item);
                
                if (_owner.IsHandleCreated)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.EnsureCreated();
                    }
                    _owner.AddItemToMenuBar(item);
                }
            }

            public ToolStripMenuItem Add(string text)
            {
                var item = new ToolStripMenuItem { Text = text };
                Add(item);
                return item;
            }
        }
    }
}
