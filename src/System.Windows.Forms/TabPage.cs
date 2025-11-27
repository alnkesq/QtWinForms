using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class TabPage : Panel
    {
        private string _text = string.Empty;

        public TabPage() : base()
        {
        }

        public TabPage(string text) : base()
        {
            _text = text;
        }

        public override string Text
        {
            get => _text;
            set
            {
                _text = value ?? string.Empty;
                // Note: The text is set on the TabControl, not the page itself
                // The parent TabControl will handle updating the tab text
            }
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QWidget_Create(IntPtr.Zero);
                SetCommonProperties();

                // Create handles for any child controls that were added before this control was created
                foreach (Control child in Controls)
                {
                    if (!child.IsHandleCreated)
                    {
                        child.EnsureCreated();
                    }

                    // Set parent relationship in Qt
                    NativeMethods.QWidget_SetParent(child.QtHandle, QtHandle);

                    // Apply position and size (Qt needs this after setParent)
                    NativeMethods.QWidget_Move(child.QtHandle, child.Location.X, child.Location.Y);
                    NativeMethods.QWidget_Resize(child.QtHandle, child.Size.Width, child.Size.Height);

                    // Initialize anchor bounds now that parent is set
                    child.InitializeAnchorBounds();

                    // QWidget::setParent hides the widget, so we must show it again if it's supposed to be visible
                    if (child.Visible)
                    {
                        NativeMethods.QWidget_Show(child.QtHandle);
                    }
                }

                // Trigger layout to handle docking/anchoring
                PerformLayout();
            }
        }
    }
}
