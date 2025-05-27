namespace Winforms_AdvTest;

public partial class Form1 : Form
{

    private TextBox tbInput;
    private RichTextBox rtbStoryBox;

    public void Initialize()
    {
        tbInput = new TextBox();
        tbInput.Location = new System.Drawing.Point(0, 100);
        tbInput.ReadOnly = false;
        tbInput.Text = "What now?";

        //Hook up event
        tbInput.KeyDown += tbInput_KeyDown;

        rtbStoryBox = new RichTextBox();
        rtbStoryBox.Dock = DockStyle.Top;
        rtbStoryBox.ForeColor = Color.Black;
        rtbStoryBox.BackColor = Color.LightSalmon;
        rtbStoryBox.ReadOnly = true;
        rtbStoryBox.Text = "Just a test";

        this.Controls.Add(tbInput);
        this.Controls.Add(rtbStoryBox);
    }

    private void tbInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string userInput = tbInput.Text;

            rtbStoryBox.Text = Program.UserAction(userInput);
            tbInput.Clear();
            tbInput.Text = "What now?";
            tbInput.SelectAll();
        }
    }

    private void Init_BackGround()
    {
        //Set the application path
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string fileDataDir = "gfx";
        string fileName = "forest2.jpg";
        //Combine application path with where the csv file is (\Data\scrubbed.csv)
        //Should eliminate hardcoding of file path as long as it's in the \Data dir with the compiled .exe a level above
        string filePath = Path.Combine(projectRoot, fileDataDir, fileName);
        Image myImage = new Bitmap(filePath);

        this.BackgroundImage = myImage;
    }

    public Form1()
    {
        InitializeComponent();

        this.Name = "MainForm";
        this.Text = "Main";
        this.Size = new System.Drawing.Size(500, 500);
        this.StartPosition = FormStartPosition.CenterScreen;

        Init_BackGround();
        Initialize();
    }
}
