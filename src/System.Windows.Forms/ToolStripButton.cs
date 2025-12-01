using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a button on a ToolStrip.
    /// </summary>
    public class ToolStripButton : ToolStripItem
    {
        internal override bool IsQWidgetCreated => false;
        protected override void CreateHandle()
        {
            QtHandle = NativeMethods.QAction_Create(string.Empty);
            UpdateImage();
            UpdateTextAndTooltip();
            if (!_enabled)
            {
                NativeMethods.QAction_SetEnabled(QtHandle, _enabled);
            }
            ConnectClickEvent();
        }

        public override bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (IsHandleCreated)
                {
                    NativeMethods.QAction_SetEnabled(QtHandle, value);
                }
            }
        }
        
        [Obsolete(NotImplementedWarning)] public bool CheckOnClick { get; set; }
        [Obsolete(NotImplementedWarning)] public bool Checked { get; set; }

        private unsafe void ConnectClickEvent()
        {
            delegate* unmanaged[Cdecl]<nint, void> callback = &OnClickedCallback;
            NativeMethods.QAction_ConnectTriggered(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe void OnClickedCallback(nint userData)
        {
            var button = ObjectFromGCHandle<ToolStripButton>(userData);
            button.OnClick(EventArgs.Empty);
        }
    }
}

