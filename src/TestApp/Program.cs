using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
                Application.SetQtWinFormsNativeDirectory(@"..\..\..\..\..\QtBackend\build\Release\");
                
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
                Console.WriteLine("13. ListBox Test");
                Console.WriteLine("14. ColorDialog Test");
                Console.WriteLine("15. DateTimePicker Test");
                Console.WriteLine("16. Font Test");
                Console.WriteLine("17. PictureBox Test");
                Console.WriteLine("18. Form Properties Test");
                Console.WriteLine("19. SynchronizationContext Test");
                Console.WriteLine("20. ToolStrip Test");
                Console.WriteLine("21. ContextMenu Test");
                Console.WriteLine("22. Key Events Test");
                Console.WriteLine("23. TreeView Test");
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

                    case "13":
                        Console.WriteLine("Running ListBox Test...");
                        testForm = CreateListBoxTest();
                        break;

                    case "14":
                        Console.WriteLine("Running ColorDialog Test...");
                        testForm = CreateColorDialogTest();
                        break;

                    case "15":
                        Console.WriteLine("Running DateTimePicker Test...");
                        testForm = CreateDateTimePickerTest();
                        break;

                    case "16":
                        Console.WriteLine("Running Font Test...");
                        testForm = CreateFontTest();
                        break;

                    case "17":
                        Console.WriteLine("Running PictureBox Test...");
                        testForm = CreatePictureBoxTest();
                        break;

                    case "18":
                        Console.WriteLine("Running Form Properties Test...");
                        testForm = CreateFormPropertiesTest();
                        break;

                    case "19":
                        Console.WriteLine("Running SynchronizationContext Test...");
                        testForm = CreateSynchronizationContextTest();
                        break;

                    case "20":
                        Console.WriteLine("Running ToolStrip Test...");
                        testForm = CreateToolStripTest();
                        break;

                    case "21":
                        Console.WriteLine("Running ContextMenu Test...");
                        testForm = CreateContextMenuTest();
                        break;

                    case "22":
                        Console.WriteLine("Running Key Events Test...");
                        testForm = CreateKeyEventsTest();
                        break;

                    case "23":
                        Console.WriteLine("Running TreeView Test...");
                        testForm = CreateTreeViewTest();
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
            button.Click += (s, e) =>
            {
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
            checkBox.CheckedChanged += (s, e) =>
            {
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

            var chkPassword = new CheckBox();
            chkPassword.Text = "Password Char";
            chkPassword.Location = new Point(220, 90);
            chkPassword.Size = new Size(150, 30);
            chkPassword.CheckedChanged += (s, e) =>
            {
                textBox.UseSystemPasswordChar = chkPassword.Checked;
            };
            form.Controls.Add(chkPassword);

            var chkVisible = new CheckBox();
            chkVisible.Text = "Toggle Button2 Visible";
            chkVisible.Location = new Point(220, 130);
            chkVisible.Size = new Size(150, 30);
            chkVisible.Checked = true;
            chkVisible.CheckedChanged += (s, e) =>
            {
                button2.Visible = chkVisible.Checked;
            };
            form.Controls.Add(chkVisible);


            var btnState = new Button();
            btnState.Text = "Toggle State";
            btnState.Location = new Point(10, 130);
            btnState.Size = new Size(120, 30);
            btnState.Click += (s, e) =>
            {
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
            linkLabel.LinkClicked += (s, e) =>
            {
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

            EventHandler updateLabel = (s, e) =>
            {
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

            // Update window title when tab changes
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                var selectedTab = tabControl.SelectedTab;
                if (selectedTab != null)
                {
                    form.Text = $"TabControl Test - {selectedTab.Text}";
                }
            };

            // Set initial title
            if (tabControl.SelectedTab != null)
            {
                form.Text = $"TabControl Test - {tabControl.SelectedTab.Text}";
            }

            form.Controls.Add(tabControl);

            return form;
        }

        static Form CreateProgressBarTest()
        {
            var form = new Form();
            form.Text = "ProgressBar Test";
            form.Size = new Size(400, 350);

            var progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 20);
            progressBar.Size = new Size(300, 30);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 50;
            form.Controls.Add(progressBar);

            var valueLabel = new Label();
            valueLabel.Text = $"Value: {progressBar.Value}";
            valueLabel.Location = new Point(330, 20);
            valueLabel.Size = new Size(60, 30);
            form.Controls.Add(valueLabel);

            var trackBar = new TrackBar();
            trackBar.Location = new Point(20, 60);
            trackBar.Size = new Size(300, 45);
            trackBar.Minimum = 0;
            trackBar.Maximum = 100;
            trackBar.Value = 50;
            trackBar.ValueChanged += (s, e) =>
            {
                progressBar.Value = trackBar.Value;
                valueLabel.Text = $"Value: {trackBar.Value}";
            };
            form.Controls.Add(trackBar);

            var trackBarLabel = new Label();
            trackBarLabel.Text = "Use TrackBar to adjust:";
            trackBarLabel.Location = new Point(20, 110);
            trackBarLabel.Size = new Size(200, 30);
            form.Controls.Add(trackBarLabel);

            var btnIncrease = new Button();
            btnIncrease.Text = "Increase";
            btnIncrease.Location = new Point(20, 150);
            btnIncrease.Size = new Size(100, 30);
            btnIncrease.Click += (s, e) =>
            {
                if (progressBar.Value + 10 <= progressBar.Maximum)
                {
                    progressBar.Value += 10;
                    trackBar.Value = progressBar.Value;
                    valueLabel.Text = $"Value: {progressBar.Value}";
                }
            };
            form.Controls.Add(btnIncrease);

            var btnDecrease = new Button();
            btnDecrease.Text = "Decrease";
            btnDecrease.Location = new Point(130, 150);
            btnDecrease.Size = new Size(100, 30);
            btnDecrease.Click += (s, e) =>
            {
                if (progressBar.Value - 10 >= progressBar.Minimum)
                {
                    progressBar.Value -= 10;
                    trackBar.Value = progressBar.Value;
                    valueLabel.Text = $"Value: {progressBar.Value}";
                }
            };
            form.Controls.Add(btnDecrease);

            var btnMarquee = new Button();
            btnMarquee.Text = "Toggle Marquee";
            btnMarquee.Location = new Point(240, 150);
            btnMarquee.Size = new Size(120, 30);
            btnMarquee.Click += (s, e) =>
            {
                if (progressBar.Style == ProgressBarStyle.Blocks)
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                    trackBar.Enabled = false;
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    trackBar.Enabled = true;
                }
            };
            form.Controls.Add(btnMarquee);

            var rangeLabel = new Label();
            rangeLabel.Text = "Range Test:";
            rangeLabel.Location = new Point(20, 190);
            rangeLabel.Size = new Size(100, 30);
            form.Controls.Add(rangeLabel);

            var btnRange1 = new Button();
            btnRange1.Text = "Range 0-100";
            btnRange1.Location = new Point(20, 220);
            btnRange1.Size = new Size(100, 30);
            btnRange1.Click += (s, e) =>
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                trackBar.Minimum = 0;
                trackBar.Maximum = 100;
                progressBar.Value = 50;
                trackBar.Value = 50;
                valueLabel.Text = $"Value: {progressBar.Value}";
            };
            form.Controls.Add(btnRange1);

            var btnRange2 = new Button();
            btnRange2.Text = "Range 0-10";
            btnRange2.Location = new Point(130, 220);
            btnRange2.Size = new Size(100, 30);
            btnRange2.Click += (s, e) =>
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 10;
                trackBar.Minimum = 0;
                trackBar.Maximum = 10;
                progressBar.Value = 5;
                trackBar.Value = 5;
                valueLabel.Text = $"Value: {progressBar.Value}";
            };
            form.Controls.Add(btnRange2);

            var btnRange3 = new Button();
            btnRange3.Text = "Range 50-150";
            btnRange3.Location = new Point(240, 220);
            btnRange3.Size = new Size(100, 30);
            btnRange3.Click += (s, e) =>
            {
                progressBar.Minimum = 50;
                progressBar.Maximum = 150;
                trackBar.Minimum = 50;
                trackBar.Maximum = 150;
                progressBar.Value = 100;
                trackBar.Value = 100;
                valueLabel.Text = $"Value: {progressBar.Value}";
            };
            form.Controls.Add(btnRange3);

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
            var fileMenu = new ToolStripMenuItem { Text = "&File" };

            var saveItem = new ToolStripMenuItem { Text = "&Save" };
            saveItem.Click += (s, e) =>
            {
                Console.WriteLine("Save clicked!");
                MessageBox.Show(form, "Save file", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fileMenu.DropDownItems.Add(saveItem);

            var openItem = new ToolStripMenuItem { Text = "&Open" };
            openItem.Click += (s, e) =>
            {
                Console.WriteLine("Open clicked!");
                MessageBox.Show(form, "Open file", "Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            fileMenu.DropDownItems.Add(openItem);

            // Add separator
            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            // Recent submenu
            var recentMenu = new ToolStripMenuItem { Text = "&Recent" };

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
            var editMenu = new ToolStripMenuItem { Text = "&Edit" };
            editMenu.Click += (s, e) => Console.WriteLine("Edit menu clicked!");
            menuStrip.Items.Add(editMenu);

            // Help menu
            var helpMenu = new ToolStripMenuItem { Text = "&Help" };
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
            button.Text = "S&how Message";
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

        static Form CreateListBoxTest()
        {
            var form = new Form();
            form.Text = "ListBox Test";
            form.Size = new Size(600, 500);

            // Single selection ListBox
            var label1 = new Label();
            label1.Text = "Single Selection ListBox:";
            label1.Location = new Point(20, 20);
            label1.Size = new Size(200, 30);
            form.Controls.Add(label1);

            var listBox1 = new ListBox();
            listBox1.Location = new Point(20, 50);
            listBox1.Size = new Size(200, 150);
            listBox1.SelectionMode = SelectionMode.One;
            listBox1.Items.Add("Apple");
            listBox1.Items.Add("Banana");
            listBox1.Items.Add("Cherry");
            listBox1.Items.Add("Date");
            listBox1.Items.Add("Elderberry");
            form.Controls.Add(listBox1);

            var selectionLabel1 = new Label();
            selectionLabel1.Text = "Selection: None";
            selectionLabel1.Location = new Point(20, 210);
            selectionLabel1.Size = new Size(200, 60);
            form.Controls.Add(selectionLabel1);

            listBox1.SelectedIndexChanged += (s, e) =>
            {
                if (listBox1.SelectedIndex != -1)
                {
                    selectionLabel1.Text = $"Selection:\nIndex: {listBox1.SelectedIndex}\nItem: {listBox1.SelectedItem}";
                }
                else
                {
                    selectionLabel1.Text = "Selection: None";
                }
            };

            // Multi-selection ListBox
            var label2 = new Label();
            label2.Text = "Multi-Selection ListBox:";
            label2.Location = new Point(250, 20);
            label2.Size = new Size(200, 30);
            form.Controls.Add(label2);

            var listBox2 = new ListBox();
            listBox2.Location = new Point(250, 50);
            listBox2.Size = new Size(200, 150);
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            listBox2.Items.Add("Red");
            listBox2.Items.Add("Green");
            listBox2.Items.Add("Blue");
            listBox2.Items.Add("Yellow");
            listBox2.Items.Add("Purple");
            listBox2.Items.Add("Orange");
            form.Controls.Add(listBox2);

            var selectionLabel2 = new Label();
            selectionLabel2.Text = "Selection: None";
            selectionLabel2.Location = new Point(250, 210);
            selectionLabel2.Size = new Size(200, 100);
            form.Controls.Add(selectionLabel2);

            listBox2.SelectedIndexChanged += (s, e) =>
            {
                var indices = listBox2.SelectedIndices;
                var items = listBox2.SelectedItems;

                if (indices.Count > 0)
                {
                    string indicesStr = "";
                    string itemsStr = "";

                    for (int i = 0; i < indices.Count; i++)
                    {
                        if (i > 0)
                        {
                            indicesStr += ", ";
                            itemsStr += ", ";
                        }
                        indicesStr += indices[i].ToString();
                        itemsStr += items[i].ToString();
                    }

                    selectionLabel2.Text = $"Count: {indices.Count}\nIndices: {indicesStr}\nItems: {itemsStr}";
                }
                else
                {
                    selectionLabel2.Text = "Selection: None";
                }
            };

            // Buttons for testing
            var btnAdd = new Button();
            btnAdd.Text = "Add Item";
            btnAdd.Location = new Point(20, 280);
            btnAdd.Size = new Size(100, 30);
            btnAdd.Click += (s, e) =>
            {
                listBox1.Items.Add($"Item {listBox1.Items.Count + 1}");
            };
            form.Controls.Add(btnAdd);

            var btnRemove = new Button();
            btnRemove.Text = "Remove Selected";
            btnRemove.Location = new Point(130, 280);
            btnRemove.Size = new Size(120, 30);
            btnRemove.Click += (s, e) =>
            {
                if (listBox1.SelectedIndex != -1)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            };
            form.Controls.Add(btnRemove);

            var btnClear = new Button();
            btnClear.Text = "Clear All";
            btnClear.Location = new Point(20, 320);
            btnClear.Size = new Size(100, 30);
            btnClear.Click += (s, e) =>
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            };
            form.Controls.Add(btnClear);

            var btnToggleMode = new Button();
            btnToggleMode.Text = "Toggle Mode";
            btnToggleMode.Location = new Point(250, 280);
            btnToggleMode.Size = new Size(120, 30);
            btnToggleMode.Click += (s, e) =>
            {
                if (listBox2.SelectionMode == SelectionMode.MultiExtended)
                {
                    listBox2.SelectionMode = SelectionMode.One;
                    label2.Text = "Single Selection ListBox:";
                }
                else
                {
                    listBox2.SelectionMode = SelectionMode.MultiExtended;
                    label2.Text = "Multi-Selection ListBox:";
                }
            };
            form.Controls.Add(btnToggleMode);

            var btnSelectFirst = new Button();
            btnSelectFirst.Text = "Select First";
            btnSelectFirst.Location = new Point(20, 360);
            btnSelectFirst.Size = new Size(100, 30);
            btnSelectFirst.Click += (s, e) =>
            {
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            };
            form.Controls.Add(btnSelectFirst);

            var btnSelectByItem = new Button();
            btnSelectByItem.Text = "Select 'Cherry'";
            btnSelectByItem.Location = new Point(130, 360);
            btnSelectByItem.Size = new Size(120, 30);
            btnSelectByItem.Click += (s, e) =>
            {
                listBox1.SelectedItem = "Cherry";
            };
            form.Controls.Add(btnSelectByItem);

            return form;
        }

        static Form CreateColorDialogTest()
        {
            var form = new Form();
            form.Text = "ColorDialog Test";
            form.Size = new Size(500, 400);

            var label = new Label();
            label.Text = "Click the button to select a color:";
            label.Location = new Point(20, 20);
            label.Size = new Size(300, 30);
            form.Controls.Add(label);

            var colorPanel = new Panel();
            colorPanel.Location = new Point(20, 60);
            colorPanel.Size = new Size(200, 100);
            colorPanel.BackColor = Color.Blue;
            form.Controls.Add(colorPanel);

            var colorLabel = new Label();
            colorLabel.Text = $"Current Color: Blue (ARGB: {Color.Blue.ToArgb():X8})";
            colorLabel.Location = new Point(20, 170);
            colorLabel.Size = new Size(400, 30);
            form.Controls.Add(colorLabel);

            var btnPickColor = new Button();
            btnPickColor.Text = "Pick Color";
            btnPickColor.Location = new Point(20, 210);
            btnPickColor.Size = new Size(120, 30);
            btnPickColor.Click += (s, e) =>
            {
                using (var colorDialog = new ColorDialog())
                {
                    colorDialog.Color = colorPanel.BackColor;
                    colorDialog.AllowFullOpen = true;

                    if (colorDialog.ShowDialog(form) == DialogResult.OK)
                    {
                        colorPanel.BackColor = colorDialog.Color;
                        colorLabel.Text = $"Current Color: {colorDialog.Color.Name} (ARGB: {colorDialog.Color.ToArgb():X8})";
                        Console.WriteLine($"Selected color: {colorDialog.Color}");
                    }
                    else
                    {
                        Console.WriteLine("Color selection cancelled");
                    }
                }
            };
            form.Controls.Add(btnPickColor);

            var btnRed = new Button();
            btnRed.Text = "Red";
            btnRed.Location = new Point(150, 210);
            btnRed.Size = new Size(80, 30);
            btnRed.Click += (s, e) =>
            {
                colorPanel.BackColor = Color.Red;
                colorLabel.Text = $"Current Color: Red (ARGB: {Color.Red.ToArgb():X8})";
            };
            form.Controls.Add(btnRed);

            var btnGreen = new Button();
            btnGreen.Text = "Green";
            btnGreen.Location = new Point(240, 210);
            btnGreen.Size = new Size(80, 30);
            btnGreen.Click += (s, e) =>
            {
                colorPanel.BackColor = Color.Green;
                colorLabel.Text = $"Current Color: Green (ARGB: {Color.Green.ToArgb():X8})";
            };
            form.Controls.Add(btnGreen);

            var btnBlue = new Button();
            btnBlue.Text = "Blue";
            btnBlue.Location = new Point(330, 210);
            btnBlue.Size = new Size(80, 30);
            btnBlue.Click += (s, e) =>
            {
                colorPanel.BackColor = Color.Blue;
                colorLabel.Text = $"Current Color: Blue (ARGB: {Color.Blue.ToArgb():X8})";
            };
            form.Controls.Add(btnBlue);

            var instructionLabel = new Label();
            instructionLabel.Text = "The ColorDialog allows you to pick any color.\nThe selected color will be displayed in the panel above.";
            instructionLabel.Location = new Point(20, 260);
            instructionLabel.Size = new Size(450, 60);
            form.Controls.Add(instructionLabel);

            return form;
        }
        static Form CreateDateTimePickerTest()
        {
            var form = new Form();
            form.Text = "DateTimePicker Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Select a date:";
            label.Location = new Point(20, 20);
            label.Size = new Size(150, 30);
            form.Controls.Add(label);

            var dtp = new DateTimePicker();
            dtp.Location = new Point(20, 50);
            dtp.Size = new Size(200, 30);
            dtp.MinDate = DateTime.Now.AddDays(-7);
            dtp.MaxDate = DateTime.Now.AddDays(7);
            form.Controls.Add(dtp);

            var valueLabel = new Label();
            valueLabel.Text = $"Selected: {dtp.Value}";
            valueLabel.Location = new Point(20, 100);
            valueLabel.Size = new Size(300, 30);
            form.Controls.Add(valueLabel);

            dtp.ValueChanged += (s, e) =>
            {
                valueLabel.Text = $"Selected: {dtp.Value}";
            };

            var btnSetNow = new Button();
            btnSetNow.Text = "Set to Now";
            btnSetNow.Location = new Point(20, 150);
            btnSetNow.Size = new Size(100, 30);
            btnSetNow.Click += (s, e) =>
            {
                dtp.Value = DateTime.Now;
            };
            form.Controls.Add(btnSetNow);

            var btnSetMin = new Button();
            btnSetMin.Text = "Set to Min";
            btnSetMin.Location = new Point(130, 150);
            btnSetMin.Size = new Size(100, 30);
            btnSetMin.Click += (s, e) =>
            {
                dtp.Value = dtp.MinDate;
            };
            form.Controls.Add(btnSetMin);

            var btnSetMax = new Button();
            btnSetMax.Text = "Set to Max";
            btnSetMax.Location = new Point(240, 150);
            btnSetMax.Size = new Size(100, 30);
            btnSetMax.Click += (s, e) =>
            {
                dtp.Value = dtp.MaxDate;
            };
            form.Controls.Add(btnSetMax);

            return form;
        }

        static Form CreateFontTest()
        {
            var form = new Form();
            form.Text = "Font Test";
            form.Size = new Size(500, 500);

            int y = 20;

            // Default Font
            var lblDefault = new Label();
            lblDefault.Text = "Default Font";
            lblDefault.Location = new Point(20, y);
            lblDefault.Size = new Size(400, 30);
            form.Controls.Add(lblDefault);
            y += 40;

            // Bold
            var lblBold = new Label();
            lblBold.Text = "Bold Font";
            lblBold.Location = new Point(20, y);
            lblBold.Size = new Size(400, 30);
            lblBold.Font = new Font("Arial", 12, FontStyle.Bold);
            form.Controls.Add(lblBold);
            y += 40;

            // Italic
            var lblItalic = new Label();
            lblItalic.Text = "Italic Font";
            lblItalic.Location = new Point(20, y);
            lblItalic.Size = new Size(400, 30);
            lblItalic.Font = new Font("Times New Roman", 12, FontStyle.Italic);
            form.Controls.Add(lblItalic);
            y += 40;

            // Underline
            var lblUnderline = new Label();
            lblUnderline.Text = "Underline Font";
            lblUnderline.Location = new Point(20, y);
            lblUnderline.Size = new Size(400, 30);
            lblUnderline.Font = new Font("Courier New", 12, FontStyle.Underline);
            form.Controls.Add(lblUnderline);
            y += 40;

            // Strikeout
            var lblStrikeout = new Label();
            lblStrikeout.Text = "Strikeout Font";
            lblStrikeout.Location = new Point(20, y);
            lblStrikeout.Size = new Size(400, 30);
            lblStrikeout.Font = new Font("Verdana", 12, FontStyle.Strikeout);
            form.Controls.Add(lblStrikeout);
            y += 40;

            // Large Size
            var lblLarge = new Label();
            lblLarge.Text = "Large Font (20pt)";
            lblLarge.Location = new Point(20, y);
            lblLarge.Size = new Size(400, 40);
            lblLarge.Font = new Font("Arial", 20);
            form.Controls.Add(lblLarge);
            y += 50;

            // Mixed
            var lblMixed = new Label();
            lblMixed.Text = "Bold + Italic + Underline";
            lblMixed.Location = new Point(20, y);
            lblMixed.Size = new Size(400, 30);
            lblMixed.Font = new Font("Arial", 10, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            form.Controls.Add(lblMixed);
            y += 40;

            // Dynamic Change
            var btnChange = new Button();
            btnChange.Text = "Change My Font";
            btnChange.Location = new Point(20, y);
            btnChange.Size = new Size(200, 40);
            btnChange.Click += (s, e) =>
            {
                if (btnChange.Font.Bold)
                {
                    btnChange.Font = new Font("Arial", 10, FontStyle.Regular);
                }
                else
                {
                    btnChange.Font = new Font("Arial", 14, FontStyle.Bold | FontStyle.Italic);
                }
            };
            form.Controls.Add(btnChange);
            y += 50;

            // Font Dialog Test
            var lblSelected = new Label();
            lblSelected.Text = "Sample Text for Font Dialog";
            lblSelected.Location = new Point(20, y);
            lblSelected.Size = new Size(400, 40);
            form.Controls.Add(lblSelected);
            y += 50;

            var btnPickFont = new Button();
            btnPickFont.Text = "Pick Font...";
            btnPickFont.Location = new Point(20, y);
            btnPickFont.Size = new Size(150, 30);
            btnPickFont.Click += (s, e) =>
            {
                using (var fontDialog = new FontDialog())
                {
                    fontDialog.Font = lblSelected.Font;
                    if (fontDialog.ShowDialog(form) == DialogResult.OK)
                    {
                        lblSelected.Font = fontDialog.Font;
                        lblSelected.Text = $"Font: {fontDialog.Font.Name}, {fontDialog.Font.Size}pt";
                    }
                }
            };
            form.Controls.Add(btnPickFont);

            return form;
        }
        static Form CreatePictureBoxTest()
        {
            var form = new Form();
            form.Text = "PictureBox Test";
            form.Size = new Size(600, 500);

            var pb = new PictureBox();
            pb.Location = new Point(20, 20);
            pb.Size = new Size(200, 200);
            pb.BackColor = Color.LightGray;
            form.Controls.Add(pb);

            // Create a bitmap in code
            var bmp = new Bitmap(100, 100);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.FillEllipse(Brushes.Red, 10, 10, 80, 80);
                g.DrawRectangle(Pens.Blue, 0, 0, 99, 99);
                g.DrawLine(Pens.Green, 0, 0, 100, 100);
                g.DrawLine(Pens.Green, 100, 0, 0, 100);
            }
            pb.Image = bmp;

            var cmbMode = new ComboBox();
            cmbMode.Location = new Point(240, 20);
            cmbMode.Size = new Size(150, 30);
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.Items.Add("Normal");
            cmbMode.Items.Add("StretchImage");
            cmbMode.Items.Add("AutoSize");
            cmbMode.Items.Add("CenterImage");
            cmbMode.Items.Add("Zoom");
            cmbMode.SelectedIndex = 0;
            cmbMode.SelectedIndexChanged += (s, e) =>
            {
                pb.SizeMode = (PictureBoxSizeMode)cmbMode.SelectedIndex;
                // Reset size if not AutoSize, to demonstrate
                if (pb.SizeMode != PictureBoxSizeMode.AutoSize)
                {
                    pb.Size = new Size(200, 200);
                }
            };
            form.Controls.Add(cmbMode);

            var btnLoad = new Button();
            btnLoad.Text = "Load Image...";
            btnLoad.Location = new Point(240, 60);
            btnLoad.Size = new Size(150, 30);
            btnLoad.Click += (s, e) =>
            {
                var dlg = new OpenFileDialog();
                dlg.Filter = "Images|*.png;*.jpg;*.bmp|All files|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pb.ImageLocation = dlg.FileName;
                }
            };
            form.Controls.Add(btnLoad);

            var btnClear = new Button();
            btnClear.Text = "Clear Image";
            btnClear.Location = new Point(240, 100);
            btnClear.Size = new Size(150, 30);
            btnClear.Click += (s, e) =>
            {
                pb.Image = null;
                pb.ImageLocation = null;
            };
            form.Controls.Add(btnClear);

            return form;
        }

        static Form CreateFormPropertiesTest()
        {
            var form = new Form();
            form.Text = "Form Properties Test";
            form.Size = new Size(400, 400);

            var chkMinimize = new CheckBox();
            chkMinimize.Text = "MinimizeBox";
            chkMinimize.Location = new Point(20, 20);
            chkMinimize.Size = new Size(200, 30);
            chkMinimize.Checked = form.MinimizeBox;
            chkMinimize.CheckedChanged += (s, e) => form.MinimizeBox = chkMinimize.Checked;
            form.Controls.Add(chkMinimize);

            var chkMaximize = new CheckBox();
            chkMaximize.Text = "MaximizeBox";
            chkMaximize.Location = new Point(20, 60);
            chkMaximize.Size = new Size(200, 30);
            chkMaximize.Checked = form.MaximizeBox;
            chkMaximize.CheckedChanged += (s, e) => form.MaximizeBox = chkMaximize.Checked;
            form.Controls.Add(chkMaximize);

            var chkTaskbar = new CheckBox();
            chkTaskbar.Text = "ShowInTaskbar";
            chkTaskbar.Location = new Point(20, 100);
            chkTaskbar.Size = new Size(200, 30);
            chkTaskbar.Checked = form.ShowInTaskbar;
            chkTaskbar.CheckedChanged += (s, e) => form.ShowInTaskbar = chkTaskbar.Checked;
            form.Controls.Add(chkTaskbar);

            var chkIcon = new CheckBox();
            chkIcon.Text = "ShowIcon";
            chkIcon.Location = new Point(20, 140);
            chkIcon.Size = new Size(200, 30);
            chkIcon.Checked = form.ShowIcon;
            chkIcon.CheckedChanged += (s, e) => form.ShowIcon = chkIcon.Checked;
            form.Controls.Add(chkIcon);

            var chkTopMost = new CheckBox();
            chkTopMost.Text = "TopMost";
            chkTopMost.Location = new Point(20, 180);
            chkTopMost.Size = new Size(200, 30);
            chkTopMost.Checked = form.TopMost;
            chkTopMost.CheckedChanged += (s, e) => form.TopMost = chkTopMost.Checked;
            form.Controls.Add(chkTopMost);

            var lblStyle = new Label();
            lblStyle.Text = "FormBorderStyle:";
            lblStyle.Location = new Point(20, 220);
            lblStyle.Size = new Size(150, 30);
            form.Controls.Add(lblStyle);

            var cmbStyle = new ComboBox();
            cmbStyle.Location = new Point(180, 220);
            cmbStyle.Size = new Size(150, 30);
            cmbStyle.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbStyle.Items.Add("None");
            cmbStyle.Items.Add("FixedSingle");
            cmbStyle.Items.Add("Fixed3D");
            cmbStyle.Items.Add("FixedDialog");
            cmbStyle.Items.Add("Sizable");
            cmbStyle.Items.Add("FixedToolWindow");
            cmbStyle.Items.Add("SizableToolWindow");

            cmbStyle.SelectedIndex = (int)form.FormBorderStyle;

            cmbStyle.SelectedIndexChanged += (s, e) =>
            {
                form.FormBorderStyle = (FormBorderStyle)cmbStyle.SelectedIndex;
            };
            form.Controls.Add(cmbStyle);

            var label = new Label();
            label.Text = "Toggle properties to see effects.";
            label.Location = new Point(20, 260);
            label.Size = new Size(300, 30);
            form.Controls.Add(label);

            return form;
        }
        static Form CreateSynchronizationContextTest()
        {
            var form = new Form();
            form.Text = "SynchronizationContext Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Waiting for background update...";
            label.Location = new Point(20, 20);
            label.Size = new Size(350, 30);
            form.Controls.Add(label);

            var button = new Button();
            button.Text = "Start Background Task";
            button.Location = new Point(20, 60);
            button.Size = new Size(200, 30);

            button.Click += (s, e) =>
            {
                button.Enabled = false;
                label.Text = "Starting background task...";

                var context = System.Threading.SynchronizationContext.Current!;

                System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                {
                    System.Threading.Thread.Sleep(500);
                    try
                    {
                        context.Send(state =>
                        {
                            throw new Exception();
                        }, null);
                    }
                    catch (Exception)
                    {
                        Console.Error.WriteLine("Successfully caught exception from SynchronizationContext.Send");
                    }

                    System.Threading.Thread.Sleep(500);
                    context.Send(state =>
                    {
                        label.Text = "Updated from background thread (Send)!";
                    }, null);

                    System.Threading.Thread.Sleep(500);
                    context.Post(state =>
                    {
                        label.Text = "Updated from background thread (Post)!";
                    }, null);

                    System.Threading.Thread.Sleep(500);
                    button.Invoke(() =>
                    {
                        label.Text = "Updated from background thread (Control.Invoke)!";
                        button.Enabled = true;
                    });
                });
            };

            form.Controls.Add(button);
            return form;
        }

        static Form CreateToolStripTest()
        {
            var form = new Form();
            form.Text = "ToolStrip Test";
            form.Size = new Size(600, 400);

            // Create ToolStrip
            var toolStrip = new ToolStrip();
            toolStrip.Location = new Point(0, 0);
            toolStrip.Size = new Size(600, 40);
            form.Controls.Add(toolStrip);

            // Create a label to show button click feedback
            var label = new Label();
            label.Text = "Click a toolbar button...";
            label.Location = new Point(20, 60);
            label.Size = new Size(500, 30);
            form.Controls.Add(label);

            // Create a multiline textbox for content
            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Location = new Point(20, 100);
            textBox.Size = new Size(550, 250);
            textBox.Text = "This is a test application with a ToolStrip.\n\nTry clicking the toolbar buttons above!";
            form.Controls.Add(textBox);

            // Add toolbar buttons
            var btnNew = new ToolStripButton();
            btnNew.Text = "New";
            btnNew.Click += (s, e) =>
            {
                label.Text = "New button clicked!";
                textBox.Text = "";
                Console.WriteLine("New button clicked!");
            };
            toolStrip.Items.Add(btnNew);

            var btnOpen = new ToolStripButton();
            btnOpen.Text = "Open";
            var icon = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(icon))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.Blue, 2, 2, 20, 20);
            }
            btnOpen.Image = icon;
            btnOpen.Click += (s, e) =>
            {
                label.Text = "Open button clicked!";
                var result = MessageBox.Show(
                    form,
                    "Open file dialog would appear here.",
                    "Open",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Console.WriteLine("Open button clicked!");
            };
            toolStrip.Items.Add(btnOpen);

            var btnSave = new ToolStripButton();
            btnSave.Text = "Save";
            btnSave.Image = icon;
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Click += (s, e) =>
            {
                label.Text = "Save button clicked!";
                MessageBox.Show(
                    form,
                    "File saved successfully!",
                    "Save",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Console.WriteLine("Save button clicked!");
            };
            toolStrip.Items.Add(btnSave);

            // Add separator
            toolStrip.Items.Add(new ToolStripSeparator());

            var btnCopy = new ToolStripButton();
            btnCopy.Text = "Copy";
            btnCopy.Click += (s, e) =>
            {
                label.Text = "Copy button clicked!";
                Console.WriteLine("Copy button clicked!");
            };
            toolStrip.Items.Add(btnCopy);

            var btnPaste = new ToolStripButton();
            btnPaste.Text = "Paste";
            btnPaste.Click += (s, e) =>
            {
                label.Text = "Paste button clicked!";
                Console.WriteLine("Paste button clicked!");
            };
            toolStrip.Items.Add(btnPaste);

            // Add separator
            toolStrip.Items.Add(new ToolStripSeparator());

            var btnHelp = new ToolStripButton();
            btnHelp.Text = "Help";
            btnHelp.Click += (s, e) =>
            {
                label.Text = "Help button clicked!";
                MessageBox.Show(
                    form,
                    "ToolStrip Test Application\nVersion 1.0\n\nThis demonstrates a ToolStrip with text-only buttons.",
                    "Help",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Console.WriteLine("Help button clicked!");
            };
            toolStrip.Items.Add(btnHelp);

            return form;
        }

        static Form CreateContextMenuTest()
        {
            var form = new Form();
            form.Text = "ContextMenuStrip Test";
            form.Size = new Size(400, 300);

            var label = new Label
            {
                Text = "Right-click me for Context Menu",
                Location = new Point(50, 50),
                AutoSize = true
            };

            var contextMenu = new ContextMenuStrip();

            var item1 = new ToolStripMenuItem { Text = "Item 1" };
            item1.Click += (s, e) => MessageBox.Show("Item 1 clicked");
            contextMenu.Items.Add(item1);

            var item2 = new ToolStripMenuItem { Text = "Item 2" };
            item2.Click += (s, e) => MessageBox.Show("Item 2 clicked");
            contextMenu.Items.Add(item2);

            contextMenu.Items.Add(new ToolStripSeparator());

            var subMenu = new ToolStripMenuItem { Text = "SubMenu" };
            var subItem1 = new ToolStripMenuItem { Text = "SubItem 1" };
            subItem1.Click += (s, e) => MessageBox.Show("SubItem 1 clicked");
            subMenu.DropDownItems.Add(subItem1);

            var subItem2 = new ToolStripMenuItem { Text = "SubItem 2" };
            subItem2.Click += (s, e) => MessageBox.Show("SubItem 2 clicked");
            subMenu.DropDownItems.Add(subItem2);

            contextMenu.Items.Add(subMenu);

            label.ContextMenuStrip = contextMenu;

            form.Controls.Add(label);

            var label2 = new Label
            {
                Text = "I have no context menu",
                Location = new Point(50, 100),
                AutoSize = true
            };
            form.Controls.Add(label2);

            var chkShowItem2 = new CheckBox();
            chkShowItem2.Text = "Show Item 2";
            chkShowItem2.Location = new Point(50, 150);
            chkShowItem2.Checked = true;
            form.Controls.Add(chkShowItem2);

            contextMenu.Opening += (s, e) =>
            {
                item2.Visible = chkShowItem2.Checked;
                Console.WriteLine($"ContextMenu Opening: Item 2 Visible = {item2.Visible}");
            };

            var chkShowSubItem2 = new CheckBox();
            chkShowSubItem2.Text = "Show SubItem 2";
            chkShowSubItem2.Location = new Point(50, 190);
            chkShowSubItem2.Checked = true;
            form.Controls.Add(chkShowSubItem2);

            subMenu.DropDown.Opening += (s, e) =>
            {
                subItem2.Visible = chkShowSubItem2.Checked;
                Console.WriteLine($"SubMenu Opening: SubItem 2 Visible = {subItem2.Visible}");
            };

            return form;
        }
        static Form CreateKeyEventsTest()
        {
            var form = new Form();
            form.Text = "Key Events Test";
            form.Size = new Size(400, 300);

            var label = new Label();
            label.Text = "Type in the TextBox below.\nPress 'A' to test suppression.";
            label.Location = new Point(20, 20);
            label.Size = new Size(350, 40);
            form.Controls.Add(label);

            var textBox = new TextBox();
            textBox.Location = new Point(20, 70);
            textBox.Size = new Size(350, 30);
            form.Controls.Add(textBox);

            var logLabel = new Label();
            logLabel.Text = "Log:";
            logLabel.Location = new Point(20, 110);
            logLabel.Size = new Size(350, 150);
            form.Controls.Add(logLabel);

            textBox.PreviewKeyDown += (s, e) =>
            {
                Console.WriteLine($"PreviewKeyDown: {e.KeyCode}, Modifiers: {e.Modifiers}");
            };

            textBox.KeyDown += (s, e) =>
            {
                Console.WriteLine($"KeyDown: {e.KeyCode}, Modifiers: {e.Modifiers}");
                logLabel.Text = $"Key: {e.KeyCode}\nModifiers: {e.Modifiers}";

                if (e.KeyCode == Keys.A)
                {
                    e.SuppressKeyPress = true;
                    logLabel.Text += "\n(Suppressed)";
                }
            };

            return form;
        }

        static Form CreateTreeViewTest()
        {
            var form = new Form();
            form.Text = "TreeView Test";
            form.Size = new Size(600, 500);

            var treeView = new TreeView();
            treeView.Location = new Point(20, 20);
            treeView.Size = new Size(250, 400);
            form.Controls.Add(treeView);

            var imageList = new ImageList();

            imageList.Images.Add(MakeMonochromeImage(Color.PeachPuff));
            imageList.Images.Add(MakeMonochromeImage(Color.Peru));
            imageList.Images.Add(MakeMonochromeImage(Color.Orchid));
            imageList.Images.Add(MakeMonochromeImage(Color.DarkOrchid));
            treeView.ImageList = imageList;

            // Add root nodes
            var node1 = treeView.Nodes.Add("Root Node 1");
            var node2 = treeView.Nodes.Add("Root Node 2");
            var node3 = treeView.Nodes.Add("Root Node 3");

            // Add child nodes to Root Node 1
            var child1_1 = node1.Nodes.Add("Child 1.1");
            var child1_2 = node1.Nodes.Add("Child 1.2");
            var child1_3 = node1.Nodes.Add("Child 1.3");

            // Add grandchild nodes
            child1_1.Nodes.Add("Grandchild 1.1.1");
            child1_1.Nodes.Add("Grandchild 1.1.2");

            // Add child nodes to Root Node 2
            node2.Nodes.Add("Child 2.1");
            node2.Nodes.Add("Child 2.2");

            // Add child nodes to Root Node 3
            var child3_1 = node3.Nodes.Add("Child 3.1");
            child3_1.Nodes.Add("Grandchild 3.1.1");
            child3_1.Nodes.Add("Grandchild 3.1.2");
            child3_1.Nodes.Add("Grandchild 3.1.3");

            // Label to show selected node
            var selectedLabel = new Label();
            selectedLabel.Text = "Selected: None";
            selectedLabel.Location = new Point(290, 20);
            selectedLabel.Size = new Size(280, 30);
            form.Controls.Add(selectedLabel);

            // TextBox to modify selected node text
            var textBox = new TextBox();
            textBox.Location = new Point(290, 60);
            textBox.Size = new Size(280, 30);
            form.Controls.Add(textBox);

            // AfterSelect event handler
            treeView.AfterSelect += (s, e) =>
            {
                if (e.Node != null)
                {
                    selectedLabel.Text = $"Selected: {e.Node.Text}";
                    textBox.Text = e.Node.Text;
                }
            };

            foreach (TreeNode item in treeView.Nodes)
            {
                item.ImageIndex = 0;
                item.SelectedImageIndex = 1;
                foreach (TreeNode child in item.Nodes)
                {
                    child.ImageIndex = 2;
                    child.SelectedImageIndex = 3;
                }
            }

            // Button to update selected node text
            var updateButton = new Button();
            updateButton.Text = "Update Selected Node";
            updateButton.Location = new Point(290, 100);
            updateButton.Size = new Size(130, 30);
            updateButton.Click += (s, e) =>
            {
                if (treeView.SelectedNode != null && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    treeView.SelectedNode.Text = textBox.Text;
                    selectedLabel.Text = $"Selected: {treeView.SelectedNode.Text}";
                }
            };
            form.Controls.Add(updateButton);

            // Button to add child to selected node
            var addChildButton = new Button();
            addChildButton.Text = "Add Child";
            addChildButton.Location = new Point(290, 140);
            addChildButton.Size = new Size(130, 30);
            addChildButton.Click += (s, e) =>
            {
                if (treeView.SelectedNode != null)
                {
                    var newChild = treeView.SelectedNode.Nodes.Add("New Child");
                    selectedLabel.Text = $"Added child to: {treeView.SelectedNode.Text}";
                }
                else
                {
                    var newRoot = treeView.Nodes.Add("New Root");
                    selectedLabel.Text = "Added new root node";
                }
            };
            form.Controls.Add(addChildButton);

            var deleteNode = new Button();
            deleteNode.Text = "Delete";
            deleteNode.Location = new Point(430, 140);
            deleteNode.Size = new Size(130, 30);
            deleteNode.Click += (s, e) =>
            {
                treeView.SelectedNode!.Remove();
            };
            form.Controls.Add(deleteNode);
            // Button to select a specific node programmatically
            var selectButton = new Button();
            selectButton.Text = "Select Child 1.1";
            selectButton.Location = new Point(290, 180);
            selectButton.Size = new Size(130, 30);
            selectButton.Click += (s, e) =>
            {
                treeView.SelectedNode = child1_1;
                textBox.Text = child1_1.Text;
                selectedLabel.Text = $"Selected: {child1_1.Text}";
            };
            form.Controls.Add(selectButton);

            // Button to expand selected node
            var expandSelectedButton = new Button();
            expandSelectedButton.Text = "Expand Selected";
            expandSelectedButton.Location = new Point(290, 220);
            expandSelectedButton.Size = new Size(130, 30);
            expandSelectedButton.Click += (s, e) =>
            {
                if (treeView.SelectedNode != null)
                {
                    treeView.SelectedNode.Expand();
                }
            };
            form.Controls.Add(expandSelectedButton);

            // Button to collapse selected node
            var collapseSelectedButton = new Button();
            collapseSelectedButton.Text = "Collapse Selected";
            collapseSelectedButton.Location = new Point(430, 220);
            collapseSelectedButton.Size = new Size(130, 30);
            collapseSelectedButton.Click += (s, e) =>
            {
                if (treeView.SelectedNode != null)
                {
                    treeView.SelectedNode.Collapse();
                }
            };
            form.Controls.Add(collapseSelectedButton);

            // Checkbox to enable dynamic child replacement on expand
            var dynamicExpandCheckbox = new CheckBox();
            dynamicExpandCheckbox.Text = "Replace children on expand";
            dynamicExpandCheckbox.Location = new Point(290, 390);
            dynamicExpandCheckbox.Size = new Size(280, 30);
            form.Controls.Add(dynamicExpandCheckbox);

            // Button to expand all nodes
            var expandAllButton = new Button();
            expandAllButton.Text = "Expand All";
            expandAllButton.Location = new Point(290, 260);
            expandAllButton.Size = new Size(130, 30);
            expandAllButton.Click += (s, e) =>
            {
                dynamicExpandCheckbox.Checked = false;
                treeView.ExpandAll();
            };
            form.Controls.Add(expandAllButton);

            // Button to collapse all nodes
            var collapseAllButton = new Button();
            collapseAllButton.Text = "Collapse All";
            collapseAllButton.Location = new Point(430, 260);
            collapseAllButton.Size = new Size(130, 30);
            collapseAllButton.Click += (s, e) =>
            {
                treeView.CollapseAll();
            };
            form.Controls.Add(collapseAllButton);

            // Button to set tooltip for selected node
            var tooltipButton = new Button();
            tooltipButton.Text = "Set ToolTip";
            tooltipButton.Location = new Point(290, 340);
            tooltipButton.Size = new Size(130, 30);
            tooltipButton.Click += (s, e) =>
            {
                if (treeView.SelectedNode != null)
                {
                    treeView.SelectedNode.ToolTipText = "Tooltip set at " + DateTime.Now.ToShortTimeString();
                    MessageBox.Show(form, "Tooltip set for " + treeView.SelectedNode.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            form.Controls.Add(tooltipButton);


            // Label to show AfterExpand events
            var afterExpandLabel = new Label();
            afterExpandLabel.Text = "AfterExpand: (none)";
            afterExpandLabel.Location = new Point(20, 430);
            afterExpandLabel.Size = new Size(550, 30);
            form.Controls.Add(afterExpandLabel);

            // BeforeExpand event handler
            treeView.BeforeExpand += (s, e) =>
            {
                if (dynamicExpandCheckbox.Checked && e.Node != null)
                {
                    // Clear existing children
                    e.Node.Nodes.Clear();

                    // Add a single child with timestamp
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    e.Node.Nodes.Add($"Parent was expanded at {timestamp}");

                    selectedLabel.Text = $"BeforeExpand: {e.Node.Text} at {timestamp}";
                }
            };

            // AfterExpand event handler
            treeView.AfterExpand += (s, e) =>
            {
                if (e.Node != null)
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    afterExpandLabel.Text = $"AfterExpand: {e.Node.Text} at {timestamp} (Children: {e.Node.Nodes.Count})";
                }
            };

            return form;
        }

        private static Bitmap MakeMonochromeImage(Color color)
        {
            var imageSharpColor = new Rgba32(color.R, color.G, color.B);
            var image = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(16, 16);
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    image[x, y] = imageSharpColor;
                }
            }

            return new Bitmap(image);
        }
    }
}
