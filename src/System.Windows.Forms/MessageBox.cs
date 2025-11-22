using System;

namespace System.Windows.Forms
{

    public static class MessageBox
    {
        private static DialogResult ShowCore(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool showHelp)
        {
            IntPtr ownerHandle = owner?.Handle ?? IntPtr.Zero;

            int result = NativeMethods.QMessageBox_Show(
                ownerHandle,
                text ?? string.Empty,
                caption ?? string.Empty,
                (int)buttons,
                (int)icon,
                (int)defaultButton,
                (int)options);

            return (DialogResult)result;
        }

        private static DialogResult ShowCore(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, object? hpi)
        {
            return ShowCore(owner, text, caption, buttons, MessageBoxIcon.Stop, defaultButton, options, false);
        }

        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
        }

        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object? param)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object? param)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, null);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, options, showHelp: false);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return ShowCore(null, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, showHelp: false);
        }
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return ShowCore(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }

        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons)
        {
            return ShowCore(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }

        public static DialogResult Show(string? text, string? caption)
        {
            return ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }
        public static DialogResult Show(string? text)
        {
            return ShowCore(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }

        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, options, showHelp: false);
        }

        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return ShowCore(owner, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, showHelp: false);
        }

        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return ShowCore(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption, MessageBoxButtons buttons)
        {
            return ShowCore(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }
        public static DialogResult Show(IWin32Window? owner, string? text, string? caption)
        {
            return ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }
        public static DialogResult Show(IWin32Window? owner, string? text)
        {
            return ShowCore(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, showHelp: false);
        }


    }
}
