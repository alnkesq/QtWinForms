namespace System.Windows.Forms
{
    /// <summary>
    /// Specifies options on a MessageBox.
    /// </summary>
    [Flags]
    public enum MessageBoxOptions
    {
        /// <summary>
        /// The message box is displayed on the active desktop.
        /// </summary>
        DefaultDesktopOnly = 0x00020000,

        /// <summary>
        /// The message box text is right-aligned.
        /// </summary>
        RightAlign = 0x00080000,

        /// <summary>
        /// Specifies that the message box text is displayed with right to left reading order.
        /// </summary>
        RtlReading = 0x00100000,

        /// <summary>
        /// The message box is displayed on the active desktop. This constant is the same as DefaultDesktopOnly.
        /// </summary>
        ServiceNotification = 0x00200000
    }
}
