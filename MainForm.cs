using Winforms_AdvTest.classes;

namespace Winforms_AdvTest;

public partial class Form1 : Form
{

    //Instantiate tbInput (for player input), rtbStoryBox (for room description, items, inventory etc..) and pictureBox (for images)
    static public TextBox tbInput = new TextBox();
    static public RichTextBox rtbStoryBox = new RichTextBox();
    static public PictureBox pictureBox = new PictureBox();

    public void Initialize()
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string fileDataDir = "gfx";

        string filePathStartScreen = Path.Combine(projectRoot, fileDataDir, "hhg2.png");

        pictureBox.Load(filePathStartScreen);
        pictureBox.Size = new System.Drawing.Size(500, 500);

        tbInput.Location = new System.Drawing.Point(0, 300);
        tbInput.ReadOnly = false;
        tbInput.Width = 200;

        //Hook up event
        tbInput.KeyDown += tbInput_KeyDown;

        rtbStoryBox.Dock = DockStyle.Top;
        rtbStoryBox.ForeColor = Color.Black;
        rtbStoryBox.BackColor = Color.WhiteSmoke;
        rtbStoryBox.ReadOnly = true;
        rtbStoryBox.Text = "Hello there, stranger. Type 'help' for possible commands";
        rtbStoryBox.Height = 300;

        TableLayoutPanel table = new TableLayoutPanel();
        table.ColumnCount = 1;
        table.RowCount = 3;
        table.Controls.Add(tbInput, 0, 2);
        table.Controls.Add(rtbStoryBox, 0, 1);
        table.Controls.Add(pictureBox, 0, 0);

        table.Dock = DockStyle.Top;
        table.AutoSize = true;

        this.Controls.Add(table);
    }

    private void tbInput_KeyDown(object sender, KeyEventArgs e)
    {
        string userInput;

        if (e.KeyCode == Keys.Enter)
        {
            userInput = tbInput.Text.ToLower();

            rtbStoryBox.Text = Program.player.Action(userInput, Program.player);
            tbInput.Clear();
            tbInput.Text = "What now?";
            tbInput.SelectAll();
        }
    }


    public Form1()
    {
        InitializeComponent();

        this.Name = "MainForm";
        this.Text = "The Adventure";
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        //Call method
        Initialize();
    }
}
