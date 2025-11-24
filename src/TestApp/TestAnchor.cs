using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestApp
{
    /// <summary>
    /// Test application demonstrating Anchor functionality
    /// </summary>
    class TestAnchor
    {
        public static Form CreateAnchorTestForm()
        {
            var form = new Form();
            form.Text = "Anchor Test - Resize to see anchoring behavior";
            form.Size = new Size(800, 600);

            // Top-Left anchored (default) - stays in place
            var topLeftButton = new Button();
            topLeftButton.Text = "Top | Left";
            topLeftButton.Location = new Point(10, 10);
            topLeftButton.Size = new Size(120, 30);
            topLeftButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            topLeftButton.BackColor = Color.FromArgb(255, 200, 200, 255);
            topLeftButton.Click += (_, _) => form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(topLeftButton);

            // Top-Right anchored - stays in top-right corner
            var topRightButton = new Button();
            topRightButton.Text = "Top | Right";
            topRightButton.Location = new Point(660, 10);
            topRightButton.Size = new Size(120, 30);
            topRightButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topRightButton.BackColor = Color.FromArgb(255, 255, 200, 200);
            topRightButton.Click += (_, _) => form.WindowState = FormWindowState.Normal;
            form.Controls.Add(topRightButton);

            // Bottom-Left anchored - stays in bottom-left corner
            var bottomLeftButton = new Button();
            bottomLeftButton.Text = "Bottom | Left";
            bottomLeftButton.Location = new Point(10, 550);
            bottomLeftButton.Size = new Size(120, 30);
            bottomLeftButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bottomLeftButton.BackColor = Color.FromArgb(255, 200, 255, 200);
            form.Controls.Add(bottomLeftButton);

            // Bottom-Right anchored - stays in bottom-right corner
            var bottomRightButton = new Button();
            bottomRightButton.Text = "Bottom | Right";
            bottomRightButton.Location = new Point(660, 550);
            bottomRightButton.Size = new Size(120, 30);
            bottomRightButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bottomRightButton.BackColor = Color.FromArgb(255, 255, 255, 200);
            form.Controls.Add(bottomRightButton);

            // Horizontal stretch - anchored left and right
            var hStretchButton = new Button();
            hStretchButton.Text = "Left | Right (Horizontal Stretch)";
            hStretchButton.Location = new Point(10, 100);
            hStretchButton.Size = new Size(770, 30);
            hStretchButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            hStretchButton.BackColor = Color.FromArgb(255, 220, 220, 255);
            form.Controls.Add(hStretchButton);

            // Vertical stretch - anchored top and bottom
            var vStretchButton = new Button();
            vStretchButton.Text = "Top | Bottom";
            vStretchButton.Location = new Point(350, 50);
            vStretchButton.Size = new Size(100, 490);
            vStretchButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            vStretchButton.BackColor = Color.FromArgb(255, 255, 220, 220);
            form.Controls.Add(vStretchButton);

            // All sides anchored - stretches in all directions
            var allStretchPanel = new Panel();
            allStretchPanel.BackColor = Color.FromArgb(255, 240, 240, 240);
            allStretchPanel.Location = new Point(150, 150);
            allStretchPanel.Size = new Size(500, 350);
            allStretchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            var centerLabel = new Label();
            centerLabel.Text = "All Sides Anchored - Stretches in all directions";
            centerLabel.Location = new Point(50, 150);
            centerLabel.Size = new Size(400, 30);
            allStretchPanel.Controls.Add(centerLabel);

            form.Controls.Add(allStretchPanel);

            return form;
        }
    }
}
