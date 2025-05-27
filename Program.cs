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
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }

    static public string UserAction(string userInput)
    {

        userInput = userInput.ToLower();

        if (userInput == "north")
        {
            return "You go North!";
        }
        else if (userInput == "south")
        {
            return "You brave the southern path!";
        }
        else if (userInput == "exit")
        {
            Application.Exit();
            return "";
        }
        else
        {
            return "No such path!";
        }
    }//End UserAction
}