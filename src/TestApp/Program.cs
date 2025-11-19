using System;
using System.Windows.Forms;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Application.InitializeQt();
                Console.WriteLine("Starting TestApp...");
                
                var form = new Form();
                form.Text = "Hello Qt from C#";
                
                var button = new Button();
                button.Text = "Click Me!";
                button.Click += (s, e) => {
                    Console.WriteLine("Button clicked!");
                };
                
                form.Controls.Add(button);
                
                Application.Run(form);
                Console.WriteLine("TestApp Exiting...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner: {ex.InnerException.Message}");
                }
            }
        }
    }
}
