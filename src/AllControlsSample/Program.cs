using System.Windows.Forms;

namespace AllControlsSample;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

#if QTWINFORMS
        Application.SetQtWinFormsNativeDirectory(@"..\..\..\..\..\..\QtBackend\build\Release\");
#endif
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}