using Winforms_AdvTest.classes;

namespace Winforms_AdvTest;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Initialize();
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }

    //instantiate rooms
    public static Room bridge = new Room("Bridge", "The control panels blink in a rhythmic pattern. You notice a computer with a thin slot in it. You're on the bridge of your ship.");
    public static Room dockingBay = new Room("Docking Bay", "You're in the docking bay. There's a shuttle here.");
    public static Room storageRoom = new Room("Storage Room", "Crates and boxes fill this storage room. It's dimly lit.");

    //Instantiate Player        
    public static Player player = new Player(bridge);

    //public static PictureBox pictureBox = new PictureBox();

    public static int Initialize()
    {
        // Connecting rooms 
        dockingBay.AddExit(player.south, bridge);
        storageRoom.AddExit(player.north, bridge);

        //Bridge room has two exits
        bridge.AddExit(player.south, storageRoom);
        bridge.AddExit(player.north, dockingBay);

        //Add items to rooms
        bridge.Items.Add("keycard", bridge);
        bridge.Items.Add("newspaper", bridge);
        dockingBay.Items.Add("blaster", dockingBay);
        storageRoom.Items.Add("broom", dockingBay);
        storageRoom.Items.Add("bucket", dockingBay);


        // string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        // string fileDataDir = "gfx";

        // //Combine application path with where the assets are (gfx dir)        
        // string filePathBridge = Path.Combine(projectRoot, fileDataDir, "spaceShipBridge.jpg");
        // string filePathDockingBay = Path.Combine(projectRoot, fileDataDir, "dockingBay.jpg");
        // string filePathStorageRoom = Path.Combine(projectRoot, fileDataDir, "storageRoom.jpg");

        // bridge.RoomGfx = Image.FromFile(filePathBridge);
        // dockingBay.RoomGfx = Image.FromFile(filePathDockingBay);
        // storageRoom.RoomGfx = Image.FromFile(filePathStorageRoom);

        //Add items to player inventory
        // player.Inventory.Add("some pocket lint");
        // player.Inventory.Add("a perfectly ordinary babel fish");

        //return all items in all rooms so we can use it in Main in the ending message
        return bridge.Items.Count + dockingBay.Items.Count + storageRoom.Items.Count;
    }
    static public void GameStart()
    {
        bool gameStarted = false;
        int items;

        //Call method
        //items is all items in all rooms combined from Initialize method
        items = Initialize();

        // while (true)
        // {
        //Prolly add some more descriptive text here, lol!            
        // if (!gameStarted)
        // {
        //     Console.WriteLine("Hello there, stranger. Type 'help' for possible commands");
        //     gameStarted = true;
        // }

        // Console.WriteLine("What now?");
        // string input = Console.ReadLine().Trim().ToLower();

        // //Break loop if user inputs 'quit' or 'exit'
        // if (input == player.exit || input == player.quit)
        //     break;


        if (player.Inventory.Count == items)
        //if (player.Inventory.Count == 1) //testing
        {
            string winner2 = $"Congratulations! You managed to collect {player.Inventory.Count} of {items} items. An amazing performance!\n";

            // //Add some "animation" ;-)
            // foreach (char c in winner2)
            // {
            //     Console.Write(c);
            //     Thread.Sleep(20);
            // }

        }

        //player.Action(input, player);
        // }
    }

    static public string UserAction(string userInput)
    {


        return "test";

        // userInput = userInput.ToLower();

        // if (userInput == "north")
        // {
        //     return "You go North!";
        // }
        // else if (userInput == "south")
        // {
        //     return "You brave the southern path!";
        // }
        // else if (userInput == "exit")
        // {
        //     Application.Exit();
        //     return "";
        // }
        // else
        // {
        //     return "No such path!";
        // }
    }//End UserAction
}