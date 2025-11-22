using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1;

namespace TestApp
{
    class Program
    {

        [STAThread]
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
                Console.WriteLine("5. ProgressBar Test");
                Console.WriteLine("6. RadioButton Test");
                Console.WriteLine("7. FormClosing Test");
                Console.WriteLine("8. MenuStrip Test");
                Console.WriteLine("9. ComboBox Test");
                Console.WriteLine("10. FolderBrowserDialog Test");
                Console.WriteLine("11. FileDialog Test");
                Console.WriteLine("12. NumericUpDown Test");
                Console.WriteLine();
                Console.Write("Enter choice (default=1): ");

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
                    
                    case "4":
                        Console.WriteLine("Running TabControl Test...");
                        testForm = CreateTabControlTest();
                        break;

                    case "5":
                        Console.WriteLine("Running ProgressBar Test...");
                        testForm = CreateProgressBarTest();
                        break;

                    case "6":
                        Console.WriteLine("Running RadioButton Test...");
                        testForm = CreateRadioButtonTest();
                        break;

                    case "7":
                        Console.WriteLine("Running FormClosing Test...");
                        testForm = CreateFormClosingTest();
                        break;

                    case "8":
                        Console.WriteLine("Running MenuStrip Test...");
                        testForm = CreateMenuStripTest();
                        break;

                    case "9":
                        Console.WriteLine("Running ComboBox Test...");
                        testForm = CreateComboBoxTest();
                        break;

                    case "10":
                        Console.WriteLine("Running FolderBrowserDialog Test...");
                        testForm = CreateFolderBrowserDialogTest();
                        break;

                    case "11":
                        Console.WriteLine("Running FileDialog Test...");
                        testForm = CreateFileDialogTest();
                        break;

                    case "12":
                        Console.WriteLine("Running NumericUpDown Test...");
                        testForm = CreateNumericUpDownTest();
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
            var multiline = new TextBox { Multiline = true };

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
                    "The text from the textbox is: " + textBox.Text + ",\nmultiline: " + multiline.Text,
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
            
            multiline.Text = "Multiline\nText";
            multiline.Location = new Point(10, 450);
            multiline.Size = new Size(200, 100);

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
            form.Controls.Add(multiline);
            
            
            var btnState = new Button();
            btnState.Text = "Toggle State";
            btnState.Location = new Point(10, 130);
            btnState.Size = new Size(120, 30);
            btnState.Click += (s, e) => {
                if (form.WindowState == FormWindowState.Normal)
                    form.WindowState = FormWindowState.Maximized;
                else if (form.WindowState == FormWindowState.Maximized)
                    form.WindowState = FormWindowState.Minimized;
                else
                    form.WindowState = FormWindowState.Normal;
            };
            form.Controls.Add(btnState);

            var linkLabel = new LinkLabel();
            linkLabel.Text = "LinkLabel";
            linkLabel.Location = new Point(10, 170);
            linkLabel.Size = new Size(100, 30);
            linkLabel.LinkClicked += (s, e) => {
                MessageBox.Show($"Link clicked!");
            };
            form.Controls.Add(linkLabel);

            form.Controls.Add(panel);

            grp.ForeColor = Color.Blue;
            var f2 = new Form1();
            f2.Visible = true;
            
            return form;
        }

        static Form CreateRadioButtonTest()
        {
            var form = new Form();
            form.Text = "RadioButton Test";
            form.Size = new Size(400, 300);

            var group1 = new GroupBox();
            group1.Text = "Group 1";
            group1.Location = new Point(20, 20);
            group1.Size = new Size(150, 150);
            form.Controls.Add(group1);

            var rb1 = new RadioButton();
            rb1.Text = "Option 1";
            rb1.Location = new Point(20, 30);
            rb1.Size = new Size(100, 30);
            rb1.Checked = true;
            group1.Controls.Add(rb1);

            var rb2 = new RadioButton();
            rb2.Text = "Option 2";
            rb2.Location = new Point(20, 70);
            rb2.Size = new Size(100, 30);
            group1.Controls.Add(rb2);

            var rb3 = new RadioButton();
            rb3.Text = "Option 3";
            rb3.Location = new Point(20, 110);
            rb3.Size = new Size(100, 30);
            group1.Controls.Add(rb3);

            var group2 = new GroupBox();
            group2.Text = "Group 2";
            group2.Location = new Point(200, 20);
            group2.Size = new Size(150, 150);
            form.Controls.Add(group2);

            var rb4 = new RadioButton();
            rb4.Text = "Choice A";
            rb4.Location = new Point(20, 30);
            rb4.Size = new Size(100, 30);
            group2.Controls.Add(rb4);

            var rb5 = new RadioButton();
            rb5.Text = "Choice B";
            rb5.Location = new Point(20, 70);
            rb5.Size = new Size(100, 30);
            rb5.Checked = true;
            group2.Controls.Add(rb5);

            var label = new Label();
            label.Text = "Selection: Option 1, Choice B";
            label.Location = new Point(20, 200);
            label.Size = new Size(300, 30);
            form.Controls.Add(label);

            EventHandler updateLabel = (s, e) => {
                string g1 = rb1.Checked ? "Option 1" : (rb2.Checked ? "Option 2" : "Option 3");
                string g2 = rb4.Checked ? "Choice A" : "Choice B";
                label.Text = $"Selection: {g1}, {g2}";
            };

            rb1.CheckedChanged += updateLabel;
            rb2.CheckedChanged += updateLabel;
            rb3.CheckedChanged += updateLabel;
            rb4.CheckedChanged += updateLabel;
            rb5.CheckedChanged += updateLabel;

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

        static Form CreateProgressBarTest()
        {
            var form = new Form();
            form.Text = "ProgressBar Test";
            form.Size = new Size(400, 300);

            var progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 20);
            progressBar.Size = new Size(300, 30);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 50;
            form.Controls.Add(progressBar);

            var btnIncrease = new Button();
            btnIncrease.Text = "Increase";
            btnIncrease.Location = new Point(20, 60);
            btnIncrease.Size = new Size(100, 30);
            btnIncrease.Click += (s, e) => {
                if (progressBar.Value + 10 <= progressBar.Maximum)
                    progressBar.Value += 10;
            };
            form.Controls.Add(btnIncrease);

            var btnDecrease = new Button();
            btnDecrease.Text = "Decrease";
            btnDecrease.Location = new Point(130, 60);
            btnDecrease.Size = new Size(100, 30);
            btnDecrease.Click += (s, e) => {
                if (progressBar.Value - 10 >= progressBar.Minimum)
                    progressBar.Value -= 10;
            };
            form.Controls.Add(btnDecrease);

            var btnMarquee = new Button();
            btnMarquee.Text = "Toggle Marquee";
            btnMarquee.Location = new Point(240, 60);
            btnMarquee.Size = new Size(120, 30);
            btnMarquee.Click += (s, e) => {
                if (progressBar.Style == ProgressBarStyle.Blocks)
                    progressBar.Style = ProgressBarStyle.Marquee;
                else
                    progressBar.Style = ProgressBarStyle.Blocks;
            };
            form.Controls.Add(btnMarquee);

            return form;
        }

        static Form CreateFormClosingTest()
        {
            var form = new Form();
            form.Text = "FormClosing Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Try to close this form.\nIt will ask for confirmation.";
            label.Location = new Point(20, 20);
            label.Size = new Size(350, 60);
            form.Controls.Add(label);

            var checkBox = new CheckBox();
            checkBox.Text = "Allow closing without confirmation";
            checkBox.Location = new Point(20, 100);
            checkBox.Size = new Size(300, 30);
            form.Controls.Add(checkBox);

            var closeButton = new Button();
            closeButton.Text = "Close Form";
            closeButton.Location = new Point(20, 150);
            closeButton.Size = new Size(120, 30);
            closeButton.Click += (s, e) => form.Close();
            form.Controls.Add(closeButton);

            form.FormClosing += (s, e) =>
            {
                Console.WriteLine($"FormClosing event fired! CloseReason: {e.CloseReason}");
                
                if (!checkBox.Checked)
                {
                    var result = MessageBox.Show(
                        form,
                        "Are you sure you want to close this form?",
                        "Confirm Close",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                    if (result != DialogResult.Yes)
                    {
                        Console.WriteLine("Close cancelled by user!");
                        e.Cancel = true;
                    }
                    else
                    {
                        Console.WriteLine("Close confirmed by user.");
                    }
                }
                else
                {
                    Console.WriteLine("Closing without confirmation (checkbox is checked).");
                }
            };

            form.FormClosed += (s, e) =>
            {
                Console.WriteLine($"FormClosed event fired! CloseReason: {e.CloseReason}");
                Console.WriteLine("Form has been successfully closed.");
            };

            return form;
        }

        static Form CreateMenuStripTest()
        {
            var form = new Form();
            form.Text = "MenuStrip Test";
            form.Size = new Size(600, 400);

            // Create menu strip
            var menuStrip = new MenuStrip();
            
            // File menu with submenus
            var fileMenu = new ToolStripMenuItem { Text = "File" };
            
            var saveItem = new ToolStripMenuItem { Text = "Save" };
            saveItem.Click += (s, e) => 
            {
                Console.WriteLine("Save clicked!");
                MessageBox.Show(form, "Save file", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fileMenu.DropDownItems.Add(saveItem);
            
            var openItem = new ToolStripMenuItem { Text = "Open" };
            openItem.Click += (s, e) => 
            {
                Console.WriteLine("Open clicked!");
                MessageBox.Show(form, "Open file", "Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fileMenu.DropDownItems.Add(openItem);
            
            // Add separator
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            
            // Recent submenu
            var recentMenu = new ToolStripMenuItem { Text = "Recent" };
            
            var recent1 = new ToolStripMenuItem { Text = "1" };
            recent1.Click += (s, e) => 
            {
                Console.WriteLine("Recent 1 clicked!");
                MessageBox.Show(form, "Opening recent file 1", "Recent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            recentMenu.DropDownItems.Add(recent1);
            
            var recent2 = new ToolStripMenuItem { Text = "2" };
            recent2.Click += (s, e) => 
            {
                Console.WriteLine("Recent 2 clicked!");
                MessageBox.Show(form, "Opening recent file 2", "Recent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            recentMenu.DropDownItems.Add(recent2);
            
            var recent3 = new ToolStripMenuItem { Text = "3" };
            recent3.Click += (s, e) => 
            {
                Console.WriteLine("Recent 3 clicked!");
                MessageBox.Show(form, "Opening recent file 3", "Recent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            recentMenu.DropDownItems.Add(recent3);
            
            fileMenu.DropDownItems.Add(recentMenu);
            
            menuStrip.Items.Add(fileMenu);

            // Edit menu (simple, no submenus)
            var editMenu = new ToolStripMenuItem { Text = "Edit" };
            editMenu.Click += (s, e) => Console.WriteLine("Edit menu clicked!");
            menuStrip.Items.Add(editMenu);

            // Help menu
            var helpMenu = new ToolStripMenuItem { Text = "Help" };
            helpMenu.Click += (s, e) =>
            {
                MessageBox.Show(
                    form,
                    "MenuStrip Test Application\nVersion 1.0\n\nThis demonstrates MenuStrip with submenus.\nTry the File > Recent menu!",
                    "About",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };
            menuStrip.Items.Add(helpMenu);

            // Set the menu strip on the form
            form.MainMenuStrip = menuStrip;

            // Add some content to the form
            var label = new Label();
            label.Text = "Click on the menu items above!\nTry File > Save, File > Open, and File > Recent > 1, 2, 3";
            label.Location = new Point(50, 80);
            label.Size = new Size(500, 60);
            form.Controls.Add(label);

            var button = new Button();
            button.Text = "Show Message";
            button.Location = new Point(50, 150);
            button.Size = new Size(150, 30);
            button.Click += (s, e) =>
            {
                MessageBox.Show(
                    form,
                    "This is a test form with a MenuStrip and submenus!",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };
            form.Controls.Add(button);

            return form;
        }

        static Form CreateComboBoxTest()
        {
            var form = new Form();
            form.Text = "ComboBox Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Select an item:";
            label.Location = new Point(20, 20);
            label.Size = new Size(150, 30);
            form.Controls.Add(label);

            var comboBox = new ComboBox();
            comboBox.Location = new Point(20, 50);
            comboBox.Size = new Size(200, 30);
            comboBox.Items.Add("Item 1");
            comboBox.Items.Add("Item 2");
            comboBox.Items.Add("Item 3");
            comboBox.Items.Add("Item 4");
            comboBox.Items.Add("Item 5");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            form.Controls.Add(comboBox);

            var selectionLabel = new Label();
            selectionLabel.Text = "Selection: None";
            selectionLabel.Location = new Point(20, 100);
            selectionLabel.Size = new Size(300, 30);
            form.Controls.Add(selectionLabel);

            comboBox.SelectedIndexChanged += (s, e) =>
            {
                if (comboBox.SelectedIndex != -1)
                {
                    selectionLabel.Text = $"Selection: {comboBox.Items[comboBox.SelectedIndex]} (Index: {comboBox.SelectedIndex})";
                }
                else
                {
                    selectionLabel.Text = "Selection: None";
                }
            };

            var btnAdd = new Button();
            btnAdd.Text = "Add Item";
            btnAdd.Location = new Point(20, 140);
            btnAdd.Size = new Size(100, 30);
            btnAdd.Click += (s, e) =>
            {
                comboBox.Items.Add($"Item {comboBox.Items.Count + 1}");
            };
            form.Controls.Add(btnAdd);

            var btnClear = new Button();
            btnClear.Text = "Clear Items";
            btnClear.Location = new Point(130, 140);
            btnClear.Size = new Size(100, 30);
            btnClear.Click += (s, e) =>
            {
                comboBox.Items.Clear();
                selectionLabel.Text = "Selection: None";
            };
            form.Controls.Add(btnClear);

            var btnToggleStyle = new Button();
            btnToggleStyle.Text = "Toggle Style";
            btnToggleStyle.Location = new Point(240, 140);
            btnToggleStyle.Size = new Size(100, 30);
            btnToggleStyle.Click += (s, e) =>
            {
                if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    label.Text = "Select or Type:";
                }
                else
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    label.Text = "Select an item:";
                }
            };
            form.Controls.Add(btnToggleStyle);

            var textLabel = new Label();
            textLabel.Text = "Text: ";
            textLabel.Location = new Point(20, 180);
            textLabel.Size = new Size(300, 30);
            form.Controls.Add(textLabel);

            comboBox.TextChanged += (s, e) =>
            {
                textLabel.Text = $"Text: {comboBox.Text}";
            };

            return form;
        }

        static Form CreateFolderBrowserDialogTest()
        {
            var form = new Form();
            form.Text = "FolderBrowserDialog Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Selected Path: None";
            label.Location = new Point(20, 20);
            label.Size = new Size(350, 30);
            form.Controls.Add(label);

            var btnShow = new Button();
            btnShow.Text = "Select Folder";
            btnShow.Location = new Point(20, 60);
            btnShow.Size = new Size(150, 30);
            btnShow.Click += (s, e) =>
            {
                var dialog = new FolderBrowserDialog();
                dialog.Description = "Select a folder for testing";
                dialog.ShowNewFolderButton = true;
                
                if (dialog.ShowDialog(form) == DialogResult.OK)
                {
                    label.Text = $"Selected Path: {dialog.SelectedPath}";
                }
                else
                {
                    label.Text = "Selection cancelled";
                }
            };
            form.Controls.Add(btnShow);

            return form;
        }

        static Form CreateFileDialogTest()
        {
            var form = new Form();
            form.Text = "FileDialog Test";
            form.Size = new Size(500, 300);

            var label = new Label();
            label.Text = "Selected File: None";
            label.Location = new Point(20, 20);
            label.Size = new Size(450, 30);
            form.Controls.Add(label);

            var btnOpen = new Button();
            btnOpen.Text = "Open File";
            btnOpen.Location = new Point(20, 60);
            btnOpen.Size = new Size(150, 30);
            btnOpen.Click += (s, e) =>
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Open a file";
                dialog.Filter = "Image Files (*.bmp, *.jpg, *.gif)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";


                if (dialog.ShowDialog(form) == DialogResult.OK)
                {
                    label.Text = $"Opened: {dialog.FileName}";
                }
                else
                {
                    label.Text = "Open cancelled";
                }
            };
            form.Controls.Add(btnOpen);

            var btnSave = new Button();
            btnSave.Text = "Save File";
            btnSave.Location = new Point(180, 60);
            btnSave.Size = new Size(150, 30);
            btnSave.Click += (s, e) =>
            {
                var dialog = new SaveFileDialog();
                dialog.Title = "Save a file";
                dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FileName = "test.txt";
                
                if (dialog.ShowDialog(form) == DialogResult.OK)
                {
                    label.Text = $"Saved: {dialog.FileName}";
                }
                else
                {
                    label.Text = "Save cancelled";
                }
            };
            form.Controls.Add(btnSave);

            return form;
        }

        static Form CreateNumericUpDownTest()
        {
            var form = new Form();
            form.Text = "NumericUpDown Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Value: 0";
            label.Location = new Point(20, 20);
            label.Size = new Size(200, 30);
            form.Controls.Add(label);

            var numeric = new NumericUpDown();
            numeric.Location = new Point(20, 60);
            numeric.Size = new Size(120, 30);
            numeric.Minimum = -100;
            numeric.Maximum = 100;
            numeric.Value = 0;
            numeric.Increment = 5;
            numeric.ValueChanged += (s, e) =>
            {
                label.Text = $"Value: {numeric.Value}";
            };
            form.Controls.Add(numeric);

            var btnSet = new Button();
            btnSet.Text = "Set to 50";
            btnSet.Location = new Point(150, 60);
            btnSet.Size = new Size(100, 30);
            btnSet.Click += (s, e) =>
            {
                numeric.Value = 50;
            };
            form.Controls.Add(btnSet);

            var btnRange = new Button();
            btnRange.Text = "Set Range 0-10";
            btnRange.Location = new Point(20, 100);
            btnRange.Size = new Size(120, 30);
            btnRange.Click += (s, e) =>
            {
                numeric.Minimum = 0;
                numeric.Maximum = 10;
                label.Text = $"Range: {numeric.Minimum} to {numeric.Maximum}, Value: {numeric.Value}";
            };
            form.Controls.Add(btnRange);

            return form;
        }
    }
}
