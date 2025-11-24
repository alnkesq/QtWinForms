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

            // Only perform Qt operations if the owner control already has a handle
            if (_owner.IsHandleCreated)
            {
                PerformQtParenting(item);
            }
        }

        /// <summary>
        /// Performs Qt-specific parenting operations when a control is added to the collection.
        /// Can be overridden by derived classes to customize the parenting behavior.
        /// </summary>
        protected virtual void PerformQtParenting(Control item)
        {
            // Ensure child widget is created
            item.EnsureCreated();

            // Set parent relationship in Qt
            NativeMethods.QWidget_SetParent(item.Handle, _owner.Handle);

            // Apply position and size (Qt needs this after setParent)
            NativeMethods.QWidget_Move(item.Handle, item.Location.X, item.Location.Y);
            NativeMethods.QWidget_Resize(item.Handle, item.Size.Width, item.Size.Height);

            // Initialize anchor bounds now that parent is set
            item.InitializeAnchorBounds();

            // QWidget::setParent hides the widget, so we must show it again if it's supposed to be visible
            if (item.Visible)
            {
                NativeMethods.QWidget_Show(item.Handle);
            }

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
