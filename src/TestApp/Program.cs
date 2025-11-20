using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var form = new Form();
                var textBox = new TextBox();
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
                button2.Click += (s, e) => 
                {
                    Console.WriteLine("Textbox: " + textBox.Text);
                    Console.WriteLine("Button2 clicked!");
                };
                
                var panel = new Panel();
                panel.Location = new Point(500, 90);
                panel.Size = new Size(200, 100);

                panel.BackColor = Color.Red;
                panel.Controls.Add(button);
                panel.Controls.Add(button2);

                var lbl = new Label();
                lbl.Text = "Label";
                lbl.Location = new Point(10, 10);
                lbl.Size = new Size(120, 30);
                form.Controls.Add(lbl);
                
                var checkBox = new CheckBox();
                checkBox.Text = "Enable Feature";
                checkBox.Location = new Point(10, 50);
                checkBox.Size = new Size(150, 30);
                checkBox.Checked = true;
                checkBox.CheckedChanged += (s, e) => {
                    lbl.Text = $"CheckBox: {checkBox.Checked}";
                    button.Enabled = checkBox.Checked;
                };
                form.Controls.Add(checkBox);
                
                textBox.Text = "Enter text here...";
                textBox.Location = new Point(10, 90);
                textBox.Size = new Size(200, 30);
                form.Controls.Add(textBox);
                
                form.Controls.Add(panel);

                var f2 = new Form1();
                f2.Visible = true;
                form.Visible = true;
                Application.Run();
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
