using System;
using System.Collections.ObjectModel;

namespace System.Windows.Forms
{
    public class ControlCollection : Collection<Control>
    {
        private readonly Control _owner;

        public ControlCollection(Control owner)
        {
            _owner = owner;
        }

        protected override void InsertItem(int index, Control item)
        {
            base.InsertItem(index, item);
            
            // Ensure both owner and child widgets are created
            _owner.EnsureCreated();
            item.EnsureCreated();
            
            // Set parent relationship in Qt
            NativeMethods.QWidget_SetParent(item.Handle, _owner.Handle);
            item.Show();
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            
            // Remove parent relationship
            if (item.Handle != IntPtr.Zero)
            {
                NativeMethods.QWidget_SetParent(item.Handle, IntPtr.Zero);
            }
        }
    }
}
