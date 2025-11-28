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

        private bool _isMoving;

        public void SetChildIndex(Control child, int newIndex)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            int oldIndex = IndexOf(child);
            if (oldIndex == -1) throw new ArgumentException("Control not found in the collection.", nameof(child));

            if (newIndex < 0) throw new ArgumentOutOfRangeException(nameof(newIndex), "Index must be non-negative.");
            
            if (newIndex >= Count) newIndex = Count - 1;

            if (oldIndex == newIndex) return;

            _isMoving = true;
            try
            {
                // Move the item in the collection
                RemoveAt(oldIndex);
                Insert(newIndex, child);
            }
            finally
            {
                _isMoving = false;
            }

            // Update Z-order in Qt
            UpdateZOrder();
        }

        private void UpdateZOrder()
        {
            if (!_owner.IsHandleCreated || Count == 0) return;

            // In WinForms, index 0 is top-most (front), index Count-1 is bottom-most (back).
            // In Qt, raise() brings to front.
            
            // Strategy:
            // 1. Bring the first control (index 0) to the front.
            // 2. Stack subsequent controls under the previous one.
            
            var first = this[0];
            if (first.IsHandleCreated)
            {
                NativeMethods.QWidget_Raise(first.QtHandle);
            }

            for (int i = 1; i < Count; i++)
            {
                var current = this[i];
                var previous = this[i - 1];
                
                if (current.IsHandleCreated && previous.IsHandleCreated)
                {
                    NativeMethods.QWidget_StackUnder(current.QtHandle, previous.QtHandle);
                }
            }
        }

        protected override void InsertItem(int index, Control item)
        {
            base.InsertItem(index, item);

            if (_isMoving) return;

            // Set parent relationship
            item.Parent = _owner;

            // Only perform Qt operations if the owner control already has a handle
            if (_owner.IsHandleCreated)
            {
                PerformQtParenting(item);
            }
            
            // New items are added to the end (bottom of Z-order) by default in WinForms logic if added via Add,
            // but Insert can put them anywhere.
            // We should probably update Z-order if we are inserting not at the end, 
            // but for now let's assume the user will call SetChildIndex if they care about Z-order 
            // or we rely on the fact that new widgets are on top in Qt by default, which contradicts WinForms 
            // (where index 0 is top). 
            // If we just add a child in Qt, it goes to top.
            // If we add to ControlCollection, it goes to end (index Count-1), which is bottom.
            // So we might need to lower it?
            // Let's leave it for now as the request is specifically about SetChildIndex.
        }

        /// <summary>
        /// Performs Qt-specific parenting operations when a control is added to the collection.
        /// Can be overridden by derived classes to customize the parenting behavior.
        /// </summary>
        protected virtual void PerformQtParenting(Control item)
        {
            // Ensure child widget is created
            item.CreateControl();

            // Set parent relationship in Qt
            NativeMethods.QWidget_SetParent(item.QtHandle, _owner.QtHandle);

            // Apply position and size (Qt needs this after setParent)
            NativeMethods.QWidget_Move(item.QtHandle, item.Location.X, item.Location.Y);
            NativeMethods.QWidget_Resize(item.QtHandle, item.Size.Width, item.Size.Height);

            // Initialize anchor bounds now that parent is set
            item.InitializeAnchorBounds();

            // QWidget::setParent hides the widget, so we must show it again if it's supposed to be visible
            if (item.Visible)
            {
                NativeMethods.QWidget_Show(item.QtHandle);
            }

            // Trigger layout to handle docking/anchoring
            _owner.PerformLayout();
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);

            if (_isMoving) return;

            // Clear parent relationship
            item.Parent = null;

            if (item.IsHandleCreated)
            {
                NativeMethods.QWidget_SetParent(item.QtHandle, IntPtr.Zero);
            }

            // Trigger layout to reflow remaining controls
            _owner.PerformLayout();
        }
    }
}
