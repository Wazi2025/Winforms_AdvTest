using Winforms_AdvTest;

namespace Winforms_AdvTest.classes;

public class Player
{
    public List<string> Inventory { get; set; }
    Room CurrentRoom { get; set; }
    readonly string look = "look";
    readonly string get = "get";
    readonly string use = "use";
    readonly string inv = "i";
    readonly string help = "help";
    readonly public string north = "north";
    readonly public string south = "south";
    readonly public string east = "east";
    readonly public string west = "west";
    readonly public string exit = "exit";
    readonly public string quit = "quit";

    public Player(Room startRoom)
    {
        Inventory = new List<string>();
        CurrentRoom = startRoom;
    }

    public string Action(string playerAction, Player player)
    {
        string description = "";

        if (CurrentRoom.Exits.ContainsKey(playerAction))
        {
            CurrentRoom = CurrentRoom.Exits[playerAction];
            description = $"You move {playerAction} and enter the {CurrentRoom.Name}.\n" + CurrentRoom.Description;

            //Call methods
            description += IterateItems(player);
            description += DisplayExits(CurrentRoom);
            return description;
        }
        else if (playerAction == look)
        {
            description = $"{CurrentRoom.Name}\n" + CurrentRoom.Description;
            // Console.WriteLine($"{CurrentRoom.Name}\n");
            // Console.WriteLine(CurrentRoom.Description);

            //Call methods
            description += IterateItems(player);
            description += DisplayExits(CurrentRoom);
            return description;
        }
        else if (playerAction == inv)
        {
            //Call method
            description += ShowInventory(player);
            return description;
        }
        else if (playerAction.Contains(get))
        {
            //Call method
            GetItem(playerAction, player);
            return description;
        }
        else if (playerAction.Contains(use))
        {
            //Call method
            UseItem(playerAction, player);
            return description;
        }
        else if (playerAction == help)
        {
            //Call method
            Help(player);
            return description;
        }
        else
        {
            return "You can't go that way.";
        }
    }

    void Help(Player player)
    {
        //show what kind of commands/actions the user can perform        
        Console.WriteLine("List of possible actions to perform: ");
        Console.WriteLine($"{player.look}, {player.inv}, {player.get}, {player.use}, {player.north}, {player.south}, {player.east}, {player.west} \n");
    }
    static public string DisplayExits(Room CurrentRoom)
    {
        string description = "";

        //check number of exits for CurrentRoom (the room the player is in)       
        string exitsList = "";
        const string exitText = "There is an exit:";
        const string exitTextMany = "There are exits:";
        bool manyExits = true;

        for (int i = 0; i < CurrentRoom.NumberOfExits.Count; i++)
        {
            if (CurrentRoom.NumberOfExits.Count <= 1)
            {
                manyExits = false;
                exitsList = "\n" + CurrentRoom.NumberOfExits[i];
            }
            else
            {
                manyExits = true;
                exitsList += "\n" + CurrentRoom.NumberOfExits[i];
            }
        }
        if (manyExits)
            description = $"{exitTextMany} {exitsList}\n";
        //Console.WriteLine($"{exitTextMany} {exitsList}\n");
        else
            description = $"{exitText} {exitsList}\n";
        //Console.WriteLine($"{exitText} {exitsList}\n");
        return description;

    }//End DisplayExits

    string IterateItems(Player player)
    {
        string description = "";
        const string youSeeText = "You see: ";

        //Check if there are items in the room
        if (player.CurrentRoom.Items.Count > 0)
            description = $"{youSeeText}\n";

        //iterate items in room
        foreach (var item in player.CurrentRoom.Items)
            description += $"{item.Key}\n";
        //Console.WriteLine(item.Key);

        return $"{description}\n";
        //Add some space after item iteration
        //Console.WriteLine();
    }//End of IterateItems

    string ShowInventory(Player player)
    {
        string description = "";
        const string itemsText = "You have: ";
        const string noItemsText = "You have no items";

        //Show player's inventory   
        //Check if player has any items
        if (player.Inventory.Count > 0)
            //Console.WriteLine(ItemsText);
            description = itemsText;
        else
            description = noItemsText;
        //Console.WriteLine(noItemsText);

        for (int i = 0; i < player.Inventory.Capacity; i++)
        {
            //Console.WriteLine($"- {player.Inventory[i]}");
            description = $"- {player.Inventory[i]}";
        }
        return description;
        //Console.WriteLine();
    }//End of ShowInventory

    void UseItem(string playerAction, Player player)
    {
        //Note: this is just a test. Will need to implement (somehow) some sort of Infocom type parser.

        string[] tempItem = playerAction.Split(" ");

        //foreach (var item in player.CurrentRoom.Puzzles)
        for (int j = 0; j < player.CurrentRoom.Puzzles.Count; j++)
        {
            if (player.CurrentRoom.Name.Equals("Bridge"))
            {
                for (int i = 0; i <= player.Inventory.Count; i++)
                {
                    bool hasItem = player.Inventory.Contains(tempItem[1]);
                    KeyValuePair<string, string> item = player.CurrentRoom.Puzzles.ElementAt(j);
                    bool correctRoom = player.CurrentRoom.Name.Equals(item.Value);

                    if (!hasItem)
                    {
                        Console.WriteLine($"You are not in posession of a {tempItem[1]}");
                        return;
                    }
                    // else if (item.Key != tempItem[1] && hasItem)
                    // {
                    //     Console.WriteLine($"That does not seem to work. Yet.");
                    //     return;
                    // }
                    else if (item.Key == tempItem[1] && hasItem && !correctRoom)
                    {
                        Console.WriteLine($"Unable to use {tempItem[1]} here. Perhaps another room?");
                        return;
                    }
                    else if (item.Key == tempItem[1] && hasItem && correctRoom)
                    {
                        Console.WriteLine($"You insert the {item.Key} into the computer slot.");
                        Console.WriteLine("A hologram of a beautiful woman coalesce in front of you. 'Hello, I am SAL. How may I be of service?'");
                        player.CurrentRoom.Description += " There is a hologram of a beautiful woman here.";
                        return;
                    }

                }
            }//End of Bridge "puzzle

            else if (player.CurrentRoom.Name.Equals("Docking Bay"))
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    bool hasItem = player.Inventory.Contains(tempItem[1]);
                    KeyValuePair<string, string> item = player.CurrentRoom.Puzzles.ElementAt(j);
                    bool correctRoom = player.CurrentRoom.Name.Equals(item.Value);

                    if (!hasItem)
                    {
                        Console.WriteLine($"You are not in posession of a {tempItem[1]}");
                        return;
                    }
                    else if (item.Key == tempItem[1] && hasItem && !correctRoom)
                    {
                        Console.WriteLine($"Unable to use {tempItem[1]} here. Perhaps another room?");
                        return;
                    }
                    else if (item.Key == tempItem[1] && hasItem && correctRoom)
                    {
                        Console.WriteLine($"You aim the {item.Key} at the robot and blast it to smithereens!");
                        Console.WriteLine("He looks slightly more depressed than before. ");
                        player.CurrentRoom.Description += " Smoldering ruins of what used to be a slightly depressed robot lies depressingly in front of the shuttle.";
                        return;
                    }
                }
            }//End of Docking Bay "puzzle"

        }//End of Puzzles loop

    }//End method
    void GetItem(string playerAction, Player player)
    {
        bool itemFound = false;
        bool missingItem = false;
        string tempItem = "";

        //Player can pickup any item in CurrentRoom's itemlist

        int itemIndex = 0;

        //Use String.Split method to split verb/items based on blank space (" ")
        string[] arrayWords = playerAction.Split(" ");

        //Split player input based on using blank space(" ") as a marker
        //Note: This means that currently this routine only accepts 1 blank space, i.e. "old newspaper" will fail
        //
        //Check if blank space (" ") can actually be found in the input string
        //or else we would get an ArgumentOutOfRangeException when assigning itemIndex
        // if (playerAction.IndexOf(" ")! > -1)
        //     itemIndex = playerAction.IndexOf(" ");
        // else
        // {
        //     //inform user if input is missing a string (or item in this case). e.g. 'get card'/get keycard
        //     Console.WriteLine("What do you want to get?");
        //     missingItem = true;
        //     break;
        // }

        //remove any whitespace
        tempItem = playerAction.Substring(itemIndex).Trim();

        //Extract the word from index 0 to before the blank space, in this case it's 'get'.
        //We don't currently use it but might later on
        //string get = playerAction.Substring(0, itemIndex);

        // foreach (var item in player.CurrentRoom.Items)
        //     Console.WriteLine(item.Key);

        //if player input word is the same as an item in CurrentRoom
        //we add this item to inventory and remove it from CurrentRoom's itemlist
        string removeRoomItem = "";

        foreach (var item in player.CurrentRoom.Items)
        {
            //Check if blank space (" ") can actually be found in the input string
            //or else we would get an ArgumentOutOfRangeException when assigning itemIndex
            if (playerAction.IndexOf(" ")! > -1)
                itemIndex = playerAction.IndexOf(" ");
            else
            {
                //inform user if input is missing a string (or item in this case). e.g. 'get card'/get keycard
                Console.WriteLine("What do you want to get?");
                missingItem = true;
                break;
            }

            if (arrayWords[1].ToLower() == item.Key)
            {
                removeRoomItem = item.Key;
                player.Inventory.Add(item.Key);

                //Remove item from CurrentRoom's itemlist                
                player.CurrentRoom.Items.Remove(removeRoomItem);

                Console.WriteLine($"You pick up the {removeRoomItem}.");
                itemFound = true;
                break;
            }
        }

        //Inform user if item is not in CurrentRoom's itemlist
        if (!itemFound && missingItem == false)
            Console.WriteLine($"There is no '{arrayWords[1]}' to pick up!\n");
    }//End of GetItem
}//End of class Player