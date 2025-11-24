using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
    public class ToolStrip : Control
    {
        private readonly List<ToolStripItem> _items = new List<ToolStripItem>();

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QToolBar_Create(IntPtr.Zero);
                if (_items.Any(x => x.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText && x.Image != null && !string.IsNullOrEmpty(x.Text)))
                    NativeMethods.QToolBar_SetToolButtonStyle(Handle, (int)ToolStripItemDisplayStyle.ImageAndText);
                SetCommonProperties();
                
                foreach (var item in _items)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.EnsureCreated();
                    }
                    AddItemToToolBar(item);
                }
            }
        }

        private void AddItemToToolBar(ToolStripItem item)
        {
            NativeMethods.QToolBar_AddAction(Handle, item.Handle);
        }

        public ToolStripItemCollection Items => new ToolStripItemCollection(this);

        public class ToolStripItemCollection
        {
            private readonly ToolStrip _owner;

            internal ToolStripItemCollection(ToolStrip owner)
            {
                _owner = owner;
            }

            public void Add(ToolStripItem item)
            {
                _owner._items.Add(item);
                
                if (_owner.IsHandleCreated)
                {
                    item.EnsureCreated();
                    _owner.AddItemToToolBar(item);
                }
            }

            public ToolStripButton Add(string text)
            {
                var item = new ToolStripButton { Text = text };
                Add(item);
                return item;
            }

            public void AddSeparator()
            {
                var separator = new ToolStripSeparator();
                Add(separator);
            }
        }
    }
}
