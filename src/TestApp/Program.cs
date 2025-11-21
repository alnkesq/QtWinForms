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
                Console.WriteLine("4. TabControl Test");
                Console.WriteLine();
                Console.Write("Enter choice (1-4, default=1): ");

                string? choice = "3"; // Console.ReadLine();
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
                    
                    case "4":
                        Console.WriteLine("Running TabControl Test...");
                        testForm = CreateTabControlTest();
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
                var result = MessageBox.Show(
                    form,
                    "This is a test message box!\nDo you want to continue?",
                    "MessageBox Test",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    0);
                Console.WriteLine($"MessageBox result: {result}");
            };
            
            var button2 = new Button();
            button2.Text = "Show Info";
            button2.Location = new Point(10, 50);
            button2.Size = new Size(120, 30);
            button2.Click += (s, e) => 
            {
                Console.WriteLine("Button2 clicked!");
                var result = MessageBox.Show(
                    "The text from the textbox is: " + textBox.Text,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Console.WriteLine($"MessageBox result: {result}");
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

        static Form CreateTabControlTest()
        {
            var form = new Form();
            form.Text = "TabControl Test";
            form.Size = new Size(600, 400);

            var tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Tab 1 - Buttons
            var tab1 = new TabPage("Buttons");
            var btn1 = new Button();
            btn1.Text = "Button 1";
            btn1.Location = new Point(20, 20);
            btn1.Size = new Size(100, 30);
            btn1.Click += (s, e) => Console.WriteLine("Button 1 clicked!");
            tab1.Controls.Add(btn1);

            var btn2 = new Button();
            btn2.Text = "Button 2";
            btn2.Location = new Point(20, 60);
            btn2.Size = new Size(100, 30);
            btn2.Click += (s, e) => Console.WriteLine("Button 2 clicked!");
            tab1.Controls.Add(btn2);

            // Tab 2 - Labels and TextBox
            var tab2 = new TabPage("Input");
            var label = new Label();
            label.Text = "Enter your name:";
            label.Location = new Point(20, 20);
            label.Size = new Size(150, 30);
            tab2.Controls.Add(label);

            var textBox = new TextBox();
            textBox.Text = "Type here...";
            textBox.Location = new Point(20, 50);
            textBox.Size = new Size(200, 30);
            tab2.Controls.Add(textBox);

            var greetButton = new Button();
            greetButton.Text = "Greet";
            greetButton.Location = new Point(20, 90);
            greetButton.Size = new Size(100, 30);
            greetButton.Click += (s, e) => 
            {
                label.Text = $"Hello, {textBox.Text}!";
            };
            tab2.Controls.Add(greetButton);

            // Tab 3 - CheckBoxes
            var tab3 = new TabPage("Options");
            var checkBox1 = new CheckBox();
            checkBox1.Text = "Option 1";
            checkBox1.Location = new Point(20, 20);
            checkBox1.Size = new Size(150, 30);
            checkBox1.CheckedChanged += (s, e) => 
                Console.WriteLine($"Option 1: {checkBox1.Checked}");
            tab3.Controls.Add(checkBox1);

            var checkBox2 = new CheckBox();
            checkBox2.Text = "Option 2";
            checkBox2.Location = new Point(20, 60);
            checkBox2.Size = new Size(150, 30);
            checkBox2.Checked = true;
            checkBox2.CheckedChanged += (s, e) => 
                Console.WriteLine($"Option 2: {checkBox2.Checked}");
            tab3.Controls.Add(checkBox2);

            var groupBox = new GroupBox();
            groupBox.Text = "Settings";
            groupBox.Location = new Point(20, 100);
            groupBox.Size = new Size(200, 100);
            
            var checkBox3 = new CheckBox();
            checkBox3.Text = "Advanced Mode";
            checkBox3.Location = new Point(10, 20);
            checkBox3.Size = new Size(150, 30);
            groupBox.Controls.Add(checkBox3);
            
            tab3.Controls.Add(groupBox);

            // Add tabs to TabControl
            tabControl.TabPages.Add(tab1);
            tabControl.TabPages.Add(tab2);
            tabControl.TabPages.Add(tab3);

            form.Controls.Add(tabControl);

            return form;
        }
    }
}
