using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TabControl : Control
    {
        private TabPageCollection _tabPages;

        public TabControl() : base()
        {
            _tabPages = new TabPageCollection(this);
            Size = new Size(300, 200);
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QTabWidget_Create(IntPtr.Zero);
                SetCommonProperties();

                // Add any existing tab pages
                // Note: We don't call base.CreateHandle() because TabPages need special handling
                // via QTabWidget_AddTab, not regular QWidget_SetParent
                foreach (TabPage page in _tabPages)
                {
                    AddTabPageToNative(page);
                }

                // Connect the selected index changed event
                ConnectCurrentChangedEvent();
            }
        }

        public TabPageCollection TabPages => _tabPages;

        private void AddTabPageToNative(TabPage page)
        {
            if (IsHandleCreated)
            {
                if (!page.IsHandleCreated)
                {
                    page.EnsureCreated();
                }
                NativeMethods.QTabWidget_AddTab(Handle, page.Handle, page.Text);
            }
        }

        private void RemoveTabPageFromNative(TabPage page)
        {
            if (IsHandleCreated && page.IsHandleCreated)
            {
                int index = _tabPages.IndexOf(page);
                if (index >= 0)
                {
                    NativeMethods.QTabWidget_RemoveTab(Handle, index);
                }
            }
        }

        public class TabPageCollection : ControlCollection
        {
            private TabControl _owner;

            public TabPageCollection(TabControl owner) : base(owner)
            {
                _owner = owner;
            }

            public new TabPage this[int index]
            {
                get => (TabPage)base[index];
            }

            protected override void InsertItem(int index, Control value)
            {
                if (!(value is TabPage))
                {
                    throw new ArgumentException("Only TabPage controls can be added to TabControl");
                }

                base.InsertItem(index, value);
            }
            
            protected override void PerformQtParenting(Control item)
            {
                // TabPages need special handling - they're added to the TabWidget
                // using QTabWidget_AddTab, not regular QWidget_SetParent
                _owner.AddTabPageToNative((TabPage)item);
            }

            public void Add(TabPage page)
            {
                Add((Control)page);
            }

            public void Add(string text)
            {
                Add(new TabPage(text));
            }

            protected override void RemoveItem(int index)
            {
                var item = this[index];
                if (item is TabPage page)
                {
                    _owner.RemoveTabPageFromNative(page);
                }
                base.RemoveItem(index);
            }

            public int IndexOf(TabPage page)
            {
                return base.IndexOf(page);
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (IsHandleCreated)
                {
                    return NativeMethods.QTabWidget_GetCurrentIndex(Handle);
                }
                return -1;
            }
            set
            {
                if (IsHandleCreated)
                {
                    NativeMethods.QTabWidget_SetCurrentIndex(Handle, value);
                }
            }
        }

        public TabPage? SelectedTab
        {
            get
            {
                int index = SelectedIndex;
                if (index >= 0 && index < TabPages.Count)
                {
                    return TabPages[index];
                }
                return null;
            }
            set
            {
                int index = TabPages.IndexOf(value!);
                if (index >= 0)
                {
                    SelectedIndex = index;
                }
            }
        }

        public event EventHandler? SelectedIndexChanged;

        private unsafe void ConnectCurrentChangedEvent()
        {
            delegate* unmanaged[Cdecl]<nint, int, void> callback = &OnCurrentChangedCallback;
            NativeMethods.QTabWidget_ConnectCurrentChanged(Handle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void OnCurrentChangedCallback(nint userData, int index)
        {
            var tabControl = ObjectFromGCHandle<TabControl>(userData);
            tabControl.SelectedIndexChanged?.Invoke(tabControl, EventArgs.Empty);
        }
    }
}
