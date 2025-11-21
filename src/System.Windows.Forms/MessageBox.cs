using System;

namespace System.Windows.Forms
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    public static class MessageBox
    {

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the MessageBoxOptions values that specifies which display and association options will be used for the message box.</param>
        /// <returns>One of the DialogResult values.</returns>
        public static DialogResult Show(
            IWin32Window? owner,
            string? text,
            string? caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton,
            MessageBoxOptions options)
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

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, and icon.
        /// </summary>
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, and buttons.
        /// </summary>
        public static DialogResult Show(string? text, string? caption, MessageBoxButtons buttons)
        {
            return Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Displays a message box with the specified text and caption.
        /// </summary>
        public static DialogResult Show(string? text, string? caption)
        {
            return Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Displays a message box with the specified text.
        /// </summary>
        public static DialogResult Show(string? text)
        {
            return Show(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }
    }
}
