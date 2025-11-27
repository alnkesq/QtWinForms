
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    public class ToolStripMenuItem : ToolStripDropDownItem
    {
        private EventHandler? _clickHandler;
        private bool _hasChildren = false;

        [Obsolete(NotImplementedWarning)] public bool CheckOnClick { get; set; }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // If this item has children, create a QMenu; otherwise create a QAction
                if (_hasChildren)
                {
                    DropDown.EnsureCreated();
                    QtHandle = NativeMethods.QAction_Create(Text);
                    NativeMethods.QAction_SetMenu(QtHandle, DropDown.QtHandle);
                }
                else
                {
                    QtHandle = NativeMethods.QAction_Create(Text);

                    // Connect click event if handler is already attached
                    if (_clickHandler != null)
                    {
                        ConnectClickEvent();
                    }
                }
            }
        }

        public override string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                if (IsHandleCreated)
                {
                    NativeMethods.QAction_SetText(QtHandle, _text);
                }
            }
        }
        private string _text = string.Empty;

        // Public properties to expose menu state
        public bool HasMenu => _hasChildren;

        public ToolStripItemCollection DropDownItems => new ToolStripItemCollectionImpl(this);



        private class ToolStripItemCollectionImpl : ToolStripItemCollection
        {
            private readonly ToolStripMenuItem _owner;

            internal ToolStripItemCollectionImpl(ToolStripMenuItem owner)
            {
                _owner = owner;
            }

            public override void Add(ToolStripItem item)
            {
                bool wasEmpty = !_owner.HasMenu;
                _owner.DropDown.Items.Add(item);
                _owner._hasChildren = true;
                
                // If we are transitioning from no children to having children, we might need to recreate the handle
                // to attach the menu.
                if (_owner.IsHandleCreated && wasEmpty)
                {
                     // This is tricky. QAction::setMenu can be called anytime.
                     // But we need to ensure DropDown is created.
                     _owner.DropDown.EnsureCreated();
                     NativeMethods.QAction_SetMenu(_owner.QtHandle, _owner.DropDown.QtHandle);
                }
            }


            public override ToolStripMenuItem Add(string text)
            {
                var item = new ToolStripMenuItem { Text = text };
                Add(item);
                return item;
            }
        }

        public event EventHandler Click
        {
            add
            {
                if (_clickHandler == null && IsHandleCreated && !_hasChildren)
                {
                    ConnectClickEvent();
                }
                _clickHandler += value;
            }
            remove
            {
                _clickHandler -= value;
            }
        }

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QAction_ConnectTriggered(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void OnClickedCallback(nint userData)
        {
            var menuItem = ObjectFromGCHandle<ToolStripMenuItem>(userData);
            menuItem._clickHandler?.Invoke(menuItem, EventArgs.Empty);
        }

        [Obsolete(NotImplementedWarning)] public string? ShortcutKeyDisplayString { get; set; }
    }
}
