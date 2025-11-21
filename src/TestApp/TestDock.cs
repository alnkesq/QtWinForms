using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestApp
{
    /// <summary>
    /// Test application demonstrating Dock functionality
    /// </summary>
    class TestDock
    {
        public static Form CreateDockTestForm()
        {
            var form = new Form();
            form.Text = "Dock Test - Resize to see docking behavior";
            form.Size = new Size(800, 600);
            form.WindowState = FormWindowState.Maximized;
            // Top docked panel
            var topPanel = new Panel();
            topPanel.BackColor = Color.FromArgb(255, 100, 150, 200);
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 60;
            
            var topLabel = new Label();
            topLabel.Text = "Docked to Top";
            topLabel.Location = new Point(10, 20);
            topLabel.Size = new Size(200, 30);
            topPanel.Controls.Add(topLabel);
            
            form.Controls.Add(topPanel);

            // Bottom docked panel
            var bottomPanel = new Panel();
            bottomPanel.BackColor = Color.FromArgb(255, 200, 150, 100);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 60;
            
            var bottomLabel = new Label();
            bottomLabel.Text = "Docked to Bottom";
            bottomLabel.Location = new Point(10, 20);
            bottomLabel.Size = new Size(200, 30);
            bottomPanel.Controls.Add(bottomLabel);
            
            form.Controls.Add(bottomPanel);

            // Left docked panel
            var leftPanel = new Panel();
            leftPanel.BackColor = Color.FromArgb(255, 150, 200, 150);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = 150;
            
            var leftLabel = new Label();
            leftLabel.Text = "Docked Left";
            leftLabel.Location = new Point(10, 100);
            leftLabel.Size = new Size(120, 30);
            leftPanel.Controls.Add(leftLabel);
            
            form.Controls.Add(leftPanel);

            // Right docked panel
            var rightPanel = new Panel();
            rightPanel.BackColor = Color.FromArgb(255, 200, 200, 150);
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Width = 150;
            
            var rightLabel = new Label();
            rightLabel.Text = "Docked Right";
            rightLabel.Location = new Point(10, 100);
            rightLabel.Size = new Size(120, 30);
            rightPanel.Controls.Add(rightLabel);
            
            form.Controls.Add(rightPanel);

            // Fill docked panel (should occupy remaining space)
            var fillPanel = new Panel();
            fillPanel.BackColor = Color.FromArgb(255, 240, 240, 240);
            fillPanel.Dock = DockStyle.Fill;
            
            var fillLabel = new Label();
            fillLabel.Text = "Docked to Fill - Occupies remaining space";
            fillLabel.Location = new Point(50, 50);
            fillLabel.Size = new Size(300, 30);
            fillPanel.Controls.Add(fillLabel);
            
            form.Controls.Add(fillPanel);

            return form;
        }
    }
}
