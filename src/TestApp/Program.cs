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
                Console.WriteLine("Qt WinForms Test Application");
                Console.WriteLine("Choose a test:");
                Console.WriteLine("1. Dock Test");
                Console.WriteLine("2. Anchor Test");
                Console.WriteLine("3. Original Test");
                Console.WriteLine();
                Console.Write("Enter choice (1-3, default=1): ");
                
                string? choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice)) choice = "1";

                Form testForm;
                
                switch (choice.Trim())
                {
                    case "1":
                        Console.WriteLine("Running Dock Test...");
                        testForm = TestDock.CreateDockTestForm();
                        break;
                    
                    case "2":
                        Console.WriteLine("Running Anchor Test...");
                        testForm = TestAnchor.CreateAnchorTestForm();
                        break;
                    
                    case "3":
                        Console.WriteLine("Running Original Test...");
                        testForm = CreateOriginalTest();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid choice, running Dock Test...");
                        testForm = TestDock.CreateDockTestForm();
                        break;
                }

                Application.Run(testForm);
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

        static Form CreateOriginalTest()
        {
            var form = new Form();
            var textBox = new TextBox();

            var lbl = new Label();
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

            var grp = new GroupBox();
            grp.Text = "Group box";
            grp.Location = new Point(100, 300);
            grp.Size = new Size(200, 100);
            grp.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            form.Controls.Add(grp);

            lbl.Text = "Label";
            lbl.Location = new Point(10, 10);
            lbl.Size = new Size(120, 30);
            grp.Controls.Add(lbl);

            form.Controls.Add(textBox);
            
            form.Controls.Add(panel);

            var f2 = new Form1();
            f2.Visible = true;
            
            return form;
        }
    }
}
