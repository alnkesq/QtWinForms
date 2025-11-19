using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var form = new Form();
                form.Text = "Hello Qt from C#";
                
                var button = new Button();
                button.Text = "Click Me!";
                button.Location = new Point(10, 10);
                button.Size = new Size(120, 30);
                button.Click += (s, e) => {
                    Console.WriteLine("Button clicked!");
                };
                
                var button2 = new Button();
                button2.Text = "button2";
                button2.Location = new Point(10, 50);
                button2.Size = new Size(120, 30);
                button2.Click += (s, e) => {
                    Console.WriteLine("Button2 clicked!");
                };
                
                var panel = new Panel();
                panel.Location = new Point(500, 90);
                panel.Size = new Size(200, 100);

                panel.BackColor = Color.Red;
                panel.Controls.Add(button);
                panel.Controls.Add(button2);
                
                form.Controls.Add(panel);
                
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
