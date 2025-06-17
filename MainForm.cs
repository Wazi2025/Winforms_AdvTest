using Winforms_AdvTest.classes;

namespace Winforms_AdvTest;

public partial class Form1 : Form
{

    static public TextBox tbInput = new TextBox();
    static public RichTextBox rtbStoryBox = new RichTextBox();
    static public PictureBox pictureBox = new PictureBox();

    public void Initialize()
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string fileDataDir = "gfx";

        //Combine application path with where the assets are (gfx dir)        
        string filePathBridge = Path.Combine(projectRoot, fileDataDir, "spaceShipBridge.jpg");
        string filePathDockingBay = Path.Combine(projectRoot, fileDataDir, "dockingBay.jpg");
        string filePathStorageRoom = Path.Combine(projectRoot, fileDataDir, "storageRoom.jpg");

        Program.bridge.RoomGfxPath = filePathBridge;
        Program.dockingBay.RoomGfxPath = filePathDockingBay;
        Program.storageRoom.RoomGfxPath = filePathStorageRoom;

        //PictureBox pictureBox = new PictureBox();
        pictureBox.Load(filePathBridge);
        //Set the application path
        // string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        // string fileDataDir = "gfx";
        // string fileName = "spaceShipBridge.jpg";

        // //Combine application path with where the assets are (gfx dir)        
        // string filePath = Path.Combine(projectRoot, fileDataDir, fileName);

        //PictureBox pictureBox = new PictureBox();
        // pictureBox.Image = Image.FromFile(filePath);
        pictureBox.Size = new System.Drawing.Size(500, 500);

        //pictureBox.SizeMode = PictureBoxSizeMode.Normal;

        //tbInput = new TextBox();
        tbInput.Location = new System.Drawing.Point(0, 300);
        tbInput.ReadOnly = false;
        tbInput.Width = 200;
        //tbInput.Text = "What now?";

        //Hook up event
        tbInput.KeyDown += tbInput_KeyDown;

        //rtbStoryBox = new RichTextBox();
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

    private void Init_BackGround()
    {
        //Set the application path
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        string fileDataDir = "gfx";
        string fileName = "spaceShipBridge.jpg";

        //Combine application path with where the assets are (gfx dir)        
        string filePath = Path.Combine(projectRoot, fileDataDir, fileName);
        // Image myImage = new Bitmap(filePath);

        // this.BackgroundImage = myImage;
        PictureBox pictureBox = new PictureBox();

        pictureBox.Image = Image.FromFile(filePath);
    }

    public Form1()
    {
        InitializeComponent();

        this.Name = "MainForm";
        this.Text = "The Adventure";
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.CenterScreen;

        //Init_BackGround();
        Initialize();
    }
}
