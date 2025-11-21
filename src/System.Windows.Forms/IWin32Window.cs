namespace System.Windows.Forms
{
    /// <summary>
    /// Provides an interface to expose Win32 HWND handles.
    /// </summary>
    public interface IWin32Window
    {
        /// <summary>
        /// Gets the handle to the window represented by the implementer.
        /// </summary>
        IntPtr Handle { get; }
    }
}
