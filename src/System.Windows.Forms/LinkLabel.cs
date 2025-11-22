using System;
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
        public event LinkLabelLinkClickedEventHandler LinkClicked;

        private delegate void LinkClickedCallbackDelegate(IntPtr userData, [MarshalAs(UnmanagedType.LPStr)] string link);
        private LinkClickedCallbackDelegate _linkClickedCallback;

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                Handle = NativeMethods.QLinkLabel_Create(IntPtr.Zero, GetHtmlText());
                SetCommonProperties();

                _linkClickedCallback = new LinkClickedCallbackDelegate(OnLinkClickedCallback);
                NativeMethods.QLinkLabel_ConnectLinkClicked(Handle, Marshal.GetFunctionPointerForDelegate(_linkClickedCallback), IntPtr.Zero);
            }
        }

        protected override void UpdateNativeText()
        {
            NativeMethods.QLabel_SetText(Handle, GetHtmlText());
        }

        private string GetHtmlText()
        {
            return $"<a href=\"#\">{_text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")}</a>";
        }

        private void OnLinkClickedCallback(IntPtr userData, string link)
        {
            LinkClicked?.Invoke(this, new LinkLabelLinkClickedEventArgs(string.Empty));
        }
    }
}
