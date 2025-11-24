using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ToolStripMenuItem : ToolStripDropDownItem
    {
        private EventHandler? _clickHandler;
        private readonly List<ToolStripItem> _dropDownItems = new List<ToolStripItem>();
        private IntPtr _menuHandle = IntPtr.Zero; // QMenu handle if this has children
        private bool _hasChildren = false;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                // If this item has children, create a QMenu; otherwise create a QAction
                if (_hasChildren)
                {
                    _menuHandle = NativeMethods.QMenu_Create(Text);
                    Handle = NativeMethods.QAction_Create(Text);
                    NativeMethods.QAction_SetMenu(Handle, _menuHandle);
                    
                    // Add any items that were added before handle creation
                    foreach (var item in _dropDownItems)
                    {
                        if (!item.IsHandleCreated)
                        {
                            item.EnsureCreated();
                        }
                        NativeMethods.QMenu_AddAction(_menuHandle, item.Handle);
                    }
                }
                else
                {
                    Handle = NativeMethods.QAction_Create(Text);
                    
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
                    NativeMethods.QAction_SetText(Handle, _text);
                }
            }
        }
        private string _text = string.Empty;

        // Public properties to expose menu state
        public bool HasMenu => _hasChildren;
        public IntPtr MenuHandle => _menuHandle;

        public ToolStripItemCollection DropDownItems => new ToolStripItemCollection(this);

        public class ToolStripItemCollection
        {
            private readonly ToolStripMenuItem _owner;

            internal ToolStripItemCollection(ToolStripMenuItem owner)
            {
                _owner = owner;
            }

            public void Add(ToolStripMenuItem item)
            {
                _owner._dropDownItems.Add(item);
                _owner._hasChildren = true;

                // If handle is already created, we need to recreate it as a menu
                if (_owner.IsHandleCreated && _owner._menuHandle == IntPtr.Zero)
                {
                    // This shouldn't happen in normal usage, but handle it just in case
                    // We'd need to recreate the handle as a menu
                    Console.Error.WriteLine("Warning: Adding dropdown items after handle creation requires recreation");
                }

                if (_owner.IsHandleCreated && _owner._menuHandle != IntPtr.Zero)
                {
                    if (!item.IsHandleCreated)
                    {
                        item.EnsureCreated();
                    }
                    NativeMethods.QMenu_AddAction(_owner._menuHandle, item.Handle);
                }
            }

            public void Add(ToolStripSeparator separator)
            {
                _owner._dropDownItems.Add(separator);
                _owner._hasChildren = true;

                if (_owner.IsHandleCreated && _owner._menuHandle != IntPtr.Zero)
                {
                    if (!separator.IsHandleCreated)
                    {
                        separator.EnsureCreated();
                    }
                    NativeMethods.QMenu_AddAction(_owner._menuHandle, separator.Handle);
                }
            }

            public ToolStripMenuItem Add(string text)
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
            NativeMethods.QAction_ConnectTriggered(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void OnClickedCallback(nint userData)
        {
            var menuItem = ObjectFromGCHandle<ToolStripMenuItem>(userData);
            menuItem._clickHandler?.Invoke(menuItem, EventArgs.Empty);
        }
    }
}
