namespace AllControlsSample;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.ShowDialog(this);
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
        progressBar1.Value = trackBar1.Value;
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        dialog.ShowDialog(this);
    }

    private void colorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new ColorDialog();
        dialog.ShowDialog(this);
    }

    private void fontToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new FontDialog();
        dialog.ShowDialog(this);
    }

    private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        dialog.ShowDialog(this);
    }
}
