using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class LinkLabelLinkClickedEventArgs : EventArgs
    {
        public LinkLabelLinkClickedEventArgs(string link)
        {
            Link = link;
        }
        public string Link { get; }
    }

    public delegate void LinkLabelLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e);

    public class LinkLabel : Label
    {
        public event LinkLabelLinkClickedEventHandler? LinkClicked;

        private delegate void LinkClickedCallbackDelegate(IntPtr userData);
        [Obsolete(NotImplementedWarning)] public Color LinkColor { get; set; }

        protected unsafe override void CreateHandle()
        {
            QtHandle = NativeMethods.QLinkLabel_Create(IntPtr.Zero, GetHtmlText());
            SetCommonProperties();

            delegate* unmanaged[Cdecl]<nint, void> callback = &OnLinkClickedCallback;
            NativeMethods.QLinkLabel_ConnectLinkClicked(QtHandle, (IntPtr)callback, GCHandlePtr);
        }

        protected override void UpdateNativeText()
        {
            NativeMethods.QLabel_SetText(QtHandle, GetHtmlText());
        }

        private string GetHtmlText()
        {
            return $"<a href=\"#\">{_text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")}</a>";
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void OnLinkClickedCallback(IntPtr userData)
        {
            var control = ObjectFromGCHandle<LinkLabel>(userData);
            control.LinkClicked?.Invoke(control, new LinkLabelLinkClickedEventArgs(string.Empty));
        }
    }
}
