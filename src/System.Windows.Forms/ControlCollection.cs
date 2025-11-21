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
            
            // Set parent relationship
            item.Parent = _owner;
            
            // Ensure both owner and child widgets are created
            _owner.EnsureCreated();
            item.EnsureCreated();
            
            // Set parent relationship in Qt
            NativeMethods.QWidget_SetParent(item.Handle, _owner.Handle);
            
            // Apply position and size (Qt needs this after setParent)
            NativeMethods.QWidget_Move(item.Handle, item.Location.X, item.Location.Y);
            NativeMethods.QWidget_Resize(item.Handle, item.Size.Width, item.Size.Height);
            
            item.Show();
            
            // Trigger layout to handle docking/anchoring
            _owner.PerformLayout();
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            
            // Clear parent relationship
            item.Parent = null;
            
            if (item.IsHandleCreated)
            {
                NativeMethods.QWidget_SetParent(item.Handle, IntPtr.Zero);
            }
            
            // Trigger layout to reflow remaining controls
            _owner.PerformLayout();
        }
    }
}
