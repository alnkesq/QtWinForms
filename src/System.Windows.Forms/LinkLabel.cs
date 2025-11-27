using System;
using System.Drawing;
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
        [Obsolete(NotImplementedWarning)] public Color LinkColor { get; set; }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QLinkLabel_Create(IntPtr.Zero, GetHtmlText());
                SetCommonProperties();

                _linkClickedCallback = new LinkClickedCallbackDelegate(OnLinkClickedCallback);
                NativeMethods.QLinkLabel_ConnectLinkClicked(QtHandle, Marshal.GetFunctionPointerForDelegate(_linkClickedCallback), IntPtr.Zero);
            }
        }

        protected override void UpdateNativeText()
        {
            NativeMethods.QLabel_SetText(QtHandle, GetHtmlText());
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
