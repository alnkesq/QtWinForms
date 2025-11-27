using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
    public class ToolStrip : Control
    {
        public ToolStrip()
        {
            Dock = DockStyle.Top;
        }
        private readonly List<ToolStripItem> _items = new List<ToolStripItem>();

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = CreateNativeControlCore();
                SetCommonProperties();

                foreach (var item in _items)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.EnsureCreated();
                    }
                    AddNativeItem(item);
                }
            }
        }

        protected virtual IntPtr CreateNativeControlCore()
        {
            var handle = NativeMethods.QToolBar_Create(IntPtr.Zero);
            if (_items.Any(x => x.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText && x.Image != null && !string.IsNullOrEmpty(x.Text)))
                NativeMethods.QToolBar_SetToolButtonStyle(handle, (int)ToolStripItemDisplayStyle.ImageAndText);
            return handle;
        }

        protected virtual void AddNativeItem(ToolStripItem item)
        {
            NativeMethods.QToolBar_AddAction(Handle, item.Handle);
        }


        [Obsolete(NotImplementedWarning)] public ToolStripGripStyle GripStyle { get; set; }
        [Obsolete(NotImplementedWarning)] public ToolStripRenderMode RenderMode { get; set; }

        public ToolStripItemCollection Items => new ToolStripItemCollectionImpl(this);

        private class ToolStripItemCollectionImpl : ToolStripItemCollection
        {
            private readonly ToolStrip _owner;

            internal ToolStripItemCollectionImpl(ToolStrip owner)
            {
                _owner = owner;
            }

            public override void Add(ToolStripItem item)
            {
                _owner._items.Add(item);

                if (_owner.IsHandleCreated)
                {
                    item.EnsureCreated();
                    _owner.AddNativeItem(item);
                }
            }

            public override ToolStripButton Add(string text)
            {
                var item = new ToolStripButton { Text = text };
                Add(item);
                return item;
            }
        }

    }
}
