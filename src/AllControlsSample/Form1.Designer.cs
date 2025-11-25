using System.Drawing;
using System.Windows.Forms;

namespace AllControlsSample;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        button1 = new Button();
        toolStrip1 = new ToolStrip();
        toolStripButton1 = new ToolStripButton();
        toolStripButton2 = new ToolStripButton();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        openFolderToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        recentToolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem2 = new ToolStripMenuItem();
        toolStripMenuItem3 = new ToolStripMenuItem();
        toolStripMenuItem4 = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        colorToolStripMenuItem = new ToolStripMenuItem();
        fontToolStripMenuItem = new ToolStripMenuItem();
        helpToolStripMenuItem = new ToolStripMenuItem();
        messageBoxToolStripMenuItem = new ToolStripMenuItem();
        progressBar1 = new ProgressBar();
        progressBar2 = new ProgressBar();
        groupBox1 = new GroupBox();
        trackBar1 = new TrackBar();
        groupBox2 = new GroupBox();
        checkBox2 = new CheckBox();
        checkBox1 = new CheckBox();
        radioButton2 = new RadioButton();
        radioButton1 = new RadioButton();
        linkLabel1 = new LinkLabel();
        comboBox1 = new ComboBox();
        textBox3 = new TextBox();
        contextMenuStrip1 = new ContextMenuStrip(components);
        toolStripMenuItem1 = new ToolStripMenuItem();
        submenuItem1ToolStripMenuItem = new ToolStripMenuItem();
        submenuItem2ToolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem5 = new ToolStripMenuItem();
        toolStripMenuItem6 = new ToolStripMenuItem();
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        tabPage2 = new TabPage();
        textBox1 = new TextBox();
        groupBox3 = new GroupBox();
        textBox2 = new TextBox();
        dateTimePicker1 = new DateTimePicker();
        numericUpDown1 = new NumericUpDown();
        listBox1 = new ListBox();
        toolStrip1.SuspendLayout();
        menuStrip1.SuspendLayout();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
        groupBox2.SuspendLayout();
        contextMenuStrip1.SuspendLayout();
        tabControl1.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new Point(477, 431);
        button1.Name = "button1";
        button1.Size = new Size(86, 23);
        button1.TabIndex = 0;
        button1.Text = "button1";
        button1.UseVisualStyleBackColor = true;
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2 });
        toolStrip1.Location = new Point(0, 24);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(887, 25);
        toolStrip1.TabIndex = 1;
        toolStrip1.Text = "toolStrip1";
        // 
        // toolStripButton1
        // 
        toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
        toolStripButton1.ImageTransparentColor = Color.Magenta;
        toolStripButton1.Name = "toolStripButton1";
        toolStripButton1.Size = new Size(23, 22);
        toolStripButton1.Text = "toolStripButton1";
        // 
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
        toolStripButton2.ImageTransparentColor = Color.Magenta;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new Size(23, 22);
        toolStripButton2.Text = "toolStripButton2";
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(887, 24);
        menuStrip1.TabIndex = 2;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, openFolderToolStripMenuItem, saveToolStripMenuItem, recentToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "&File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(146, 22);
        openToolStripMenuItem.Text = "&Open...";
        openToolStripMenuItem.Click += openToolStripMenuItem_Click;
        // 
        // openFolderToolStripMenuItem
        // 
        openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
        openFolderToolStripMenuItem.Size = new Size(146, 22);
        openFolderToolStripMenuItem.Text = "Open folder...";
        openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(146, 22);
        saveToolStripMenuItem.Text = "&Save...";
        saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
        // 
        // recentToolStripMenuItem
        // 
        recentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
        recentToolStripMenuItem.Name = "recentToolStripMenuItem";
        recentToolStripMenuItem.Size = new Size(146, 22);
        recentToolStripMenuItem.Text = "&Recent";
        // 
        // toolStripMenuItem2
        // 
        toolStripMenuItem2.Name = "toolStripMenuItem2";
        toolStripMenuItem2.Size = new Size(80, 22);
        toolStripMenuItem2.Text = "1";
        // 
        // toolStripMenuItem3
        // 
        toolStripMenuItem3.Name = "toolStripMenuItem3";
        toolStripMenuItem3.Size = new Size(80, 22);
        toolStripMenuItem3.Text = "2";
        // 
        // toolStripMenuItem4
        // 
        toolStripMenuItem4.Name = "toolStripMenuItem4";
        toolStripMenuItem4.Size = new Size(80, 22);
        toolStripMenuItem4.Text = "3";
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { colorToolStripMenuItem, fontToolStripMenuItem });
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(39, 20);
        editToolStripMenuItem.Text = "Edit";
        // 
        // colorToolStripMenuItem
        // 
        colorToolStripMenuItem.Name = "colorToolStripMenuItem";
        colorToolStripMenuItem.Size = new Size(112, 22);
        colorToolStripMenuItem.Text = "C&olor...";
        colorToolStripMenuItem.Click += colorToolStripMenuItem_Click;
        // 
        // fontToolStripMenuItem
        // 
        fontToolStripMenuItem.Name = "fontToolStripMenuItem";
        fontToolStripMenuItem.Size = new Size(112, 22);
        fontToolStripMenuItem.Text = "&Font...";
        fontToolStripMenuItem.Click += fontToolStripMenuItem_Click;
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { messageBoxToolStripMenuItem });
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        helpToolStripMenuItem.Size = new Size(44, 20);
        helpToolStripMenuItem.Text = "Help";
        // 
        // messageBoxToolStripMenuItem
        // 
        messageBoxToolStripMenuItem.Name = "messageBoxToolStripMenuItem";
        messageBoxToolStripMenuItem.Size = new Size(180, 22);
        messageBoxToolStripMenuItem.Text = "MessageBox";
        messageBoxToolStripMenuItem.Click += messageBoxToolStripMenuItem_Click;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(30, 35);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(190, 23);
        progressBar1.TabIndex = 3;
        progressBar1.Value = 50;
        // 
        // progressBar2
        // 
        progressBar2.Location = new Point(30, 64);
        progressBar2.Name = "progressBar2";
        progressBar2.Size = new Size(190, 23);
        progressBar2.Style = ProgressBarStyle.Marquee;
        progressBar2.TabIndex = 4;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(trackBar1);
        groupBox1.Controls.Add(progressBar1);
        groupBox1.Controls.Add(progressBar2);
        groupBox1.Location = new Point(43, 264);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(240, 148);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "groupBox1";
        // 
        // trackBar1
        // 
        trackBar1.Location = new Point(30, 102);
        trackBar1.Maximum = 100;
        trackBar1.Name = "trackBar1";
        trackBar1.Size = new Size(190, 45);
        trackBar1.TabIndex = 5;
        trackBar1.TickFrequency = 10;
        trackBar1.Value = 50;
        trackBar1.Scroll += trackBar1_Scroll;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(checkBox2);
        groupBox2.Controls.Add(checkBox1);
        groupBox2.Controls.Add(radioButton2);
        groupBox2.Controls.Add(radioButton1);
        groupBox2.Location = new Point(316, 269);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(247, 143);
        groupBox2.TabIndex = 6;
        groupBox2.TabStop = false;
        groupBox2.Text = "groupBox2";
        // 
        // checkBox2
        // 
        checkBox2.AutoSize = true;
        checkBox2.Checked = true;
        checkBox2.CheckState = CheckState.Checked;
        checkBox2.Location = new Point(20, 112);
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new Size(83, 19);
        checkBox2.TabIndex = 3;
        checkBox2.Text = "checkBox2";
        checkBox2.UseVisualStyleBackColor = true;
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point(20, 87);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(83, 19);
        checkBox1.TabIndex = 2;
        checkBox1.Text = "checkBox1";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // radioButton2
        // 
        radioButton2.AutoSize = true;
        radioButton2.Checked = true;
        radioButton2.Location = new Point(20, 52);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new Size(94, 19);
        radioButton2.TabIndex = 1;
        radioButton2.TabStop = true;
        radioButton2.Text = "radioButton2";
        radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton1
        // 
        radioButton1.AutoSize = true;
        radioButton1.Location = new Point(20, 27);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new Size(94, 19);
        radioButton1.TabIndex = 0;
        radioButton1.Text = "radioButton1";
        radioButton1.UseVisualStyleBackColor = true;
        // 
        // linkLabel1
        // 
        linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        linkLabel1.AutoSize = true;
        linkLabel1.Location = new Point(818, 466);
        linkLabel1.Name = "linkLabel1";
        linkLabel1.Size = new Size(57, 15);
        linkLabel1.TabIndex = 7;
        linkLabel1.TabStop = true;
        linkLabel1.Text = "LinkLabel";
        // 
        // comboBox1
        // 
        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3" });
        comboBox1.Location = new Point(15, 22);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(201, 23);
        comboBox1.TabIndex = 8;
        // 
        // textBox3
        // 
        textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        textBox3.Location = new Point(697, 111);
        textBox3.Multiline = true;
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(178, 111);
        textBox3.TabIndex = 10;
        textBox3.Text = "multi\r\nline\r\ntextbox";
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem5, toolStripMenuItem6 });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(187, 70);
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { submenuItem1ToolStripMenuItem, submenuItem2ToolStripMenuItem });
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(186, 22);
        toolStripMenuItem1.Text = "Context submenu";
        // 
        // submenuItem1ToolStripMenuItem
        // 
        submenuItem1ToolStripMenuItem.Name = "submenuItem1ToolStripMenuItem";
        submenuItem1ToolStripMenuItem.Size = new Size(161, 22);
        submenuItem1ToolStripMenuItem.Text = "Submenu item 1";
        // 
        // submenuItem2ToolStripMenuItem
        // 
        submenuItem2ToolStripMenuItem.Name = "submenuItem2ToolStripMenuItem";
        submenuItem2ToolStripMenuItem.Size = new Size(161, 22);
        submenuItem2ToolStripMenuItem.Text = "Submenu item 2";
        // 
        // toolStripMenuItem5
        // 
        toolStripMenuItem5.Name = "toolStripMenuItem5";
        toolStripMenuItem5.Size = new Size(186, 22);
        toolStripMenuItem5.Text = "Context menu item 1";
        // 
        // toolStripMenuItem6
        // 
        toolStripMenuItem6.Name = "toolStripMenuItem6";
        toolStripMenuItem6.Size = new Size(186, 22);
        toolStripMenuItem6.Text = "Context menu item 2";
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Location = new Point(39, 64);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(240, 184);
        tabControl1.TabIndex = 11;
        // 
        // tabPage1
        // 
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(232, 156);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "tabPage1";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(232, 156);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "tabPage2";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(15, 50);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(201, 23);
        textBox1.TabIndex = 12;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(textBox2);
        groupBox3.Controls.Add(dateTimePicker1);
        groupBox3.Controls.Add(numericUpDown1);
        groupBox3.Controls.Add(comboBox1);
        groupBox3.Controls.Add(textBox1);
        groupBox3.Location = new Point(312, 64);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(247, 180);
        groupBox3.TabIndex = 13;
        groupBox3.TabStop = false;
        groupBox3.Text = "groupBox3";
        // 
        // textBox2
        // 
        textBox2.Location = new Point(16, 138);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(200, 23);
        textBox2.TabIndex = 10;
        textBox2.UseSystemPasswordChar = true;
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new Point(16, 109);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new Size(200, 23);
        dateTimePicker1.TabIndex = 14;
        // 
        // numericUpDown1
        // 
        numericUpDown1.Location = new Point(16, 79);
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size(200, 23);
        numericUpDown1.TabIndex = 13;
        // 
        // listBox1
        // 
        listBox1.Anchor = AnchorStyles.Right;
        listBox1.FormattingEnabled = true;
        listBox1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4" });
        listBox1.Location = new Point(697, 228);
        listBox1.Name = "listBox1";
        listBox1.SelectionMode = SelectionMode.MultiExtended;
        listBox1.Size = new Size(178, 94);
        listBox1.TabIndex = 14;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(887, 490);
        ContextMenuStrip = contextMenuStrip1;
        Controls.Add(listBox1);
        Controls.Add(textBox3);
        Controls.Add(groupBox3);
        Controls.Add(tabControl1);
        Controls.Add(linkLabel1);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(button1);
        Controls.Add(toolStrip1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Form1";
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        contextMenuStrip1.ResumeLayout(false);
        tabControl1.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button button1;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButton1;
    private ToolStripButton toolStripButton2;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem recentToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ProgressBar progressBar1;
    private ProgressBar progressBar2;
    private GroupBox groupBox1;
    private TrackBar trackBar1;
    private GroupBox groupBox2;
    private CheckBox checkBox2;
    private CheckBox checkBox1;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem colorToolStripMenuItem;
    private ToolStripMenuItem fontToolStripMenuItem;
    private ToolStripMenuItem openFolderToolStripMenuItem;
    private LinkLabel linkLabel1;
    private ComboBox comboBox1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem submenuItem1ToolStripMenuItem;
    private ToolStripMenuItem submenuItem2ToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem5;
    private ToolStripMenuItem toolStripMenuItem6;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TextBox textBox1;
    private GroupBox groupBox3;
    private DateTimePicker dateTimePicker1;
    private NumericUpDown numericUpDown1;
    private TextBox textBox3;
    private TextBox textBox2;
    private ToolStripMenuItem messageBoxToolStripMenuItem;
    private ListBox listBox1;
}
