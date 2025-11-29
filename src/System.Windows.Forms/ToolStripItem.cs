using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace System.Windows.Forms
{
    public abstract class ToolStripItem : Control
    {
        private Image? _image;
        private string _text = string.Empty;
        private string? _toolTipText = null; // null means use Text automatically
        private ToolStripItemDisplayStyle _displayStyle = ToolStripItemDisplayStyle.ImageAndText;

        public Image? Image
        {
            get => _image;
            set
            {
                _image = value;
                if (IsHandleCreated)
                {
                    UpdateImage();
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
                    UpdateTextAndTooltip();
                }
            }
        }

        public string ToolTipText
        {
            get => _toolTipText ?? _text; // Return Text if tooltip not explicitly set
            set
            {
                _toolTipText = value; // Can be null to reset to automatic
                if (IsHandleCreated)
                {
                    NativeMethods.QAction_SetToolTip(QtHandle, ToolTipText);
                }
            }
        }

        public ToolStripItemDisplayStyle DisplayStyle
        {
            get => _displayStyle;
            set
            {
                _displayStyle = value;
                if (IsHandleCreated)
                {
                    UpdateTextAndTooltip();
                }
            }
        }

        protected override void UpdateVisibleCore(bool value)
        {
            NativeMethods.QAction_SetVisible(QtHandle, value);
        }

        protected void UpdateImage()
        {
            if (_image != null)
            {
                NativeMethods.QAction_SetIcon(QtHandle, _image.GetQIcon());
            }
            else
            {
                NativeMethods.QAction_SetIcon(QtHandle, IntPtr.Zero);
            }
        }

        protected void UpdateTextAndTooltip()
        {
            // Determine what text to show based on DisplayStyle
            string displayText = string.Empty;
            if (_displayStyle == ToolStripItemDisplayStyle.Text || _displayStyle == ToolStripItemDisplayStyle.ImageAndText)
            {
                displayText = _text;
            }

            NativeMethods.QAction_SetText(QtHandle, displayText);

            // Only show tooltip if text is not already visible (Image or None mode)
            // or if tooltip was explicitly set
            string tooltipText = string.Empty;
            if (_toolTipText != null)
            {
                // Explicitly set tooltip
                tooltipText = _toolTipText;
            }
            else if (_displayStyle == ToolStripItemDisplayStyle.Image || _displayStyle == ToolStripItemDisplayStyle.None)
            {
                // Automatic tooltip when text is not visible
                tooltipText = _text;
            }

            NativeMethods.QAction_SetToolTip(QtHandle, tooltipText);
        }

        [Obsolete(NotImplementedWarning)] public Color ImageTransparentColor { get; set; }

        public void PerformClick()
        { 
            if(Enabled) OnClick(EventArgs.Empty); 
        }
    }
}

