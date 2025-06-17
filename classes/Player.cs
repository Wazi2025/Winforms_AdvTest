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
    readonly string exit = "exit";

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
            description = $"You move {playerAction} and enter the {CurrentRoom.Name}.\n{CurrentRoom.Description}\n";

            //Call methods
            description += IterateItems(player);
            description += DisplayExits(CurrentRoom);
            return description;
        }
        else if (playerAction == look)
        {
            description = $"{CurrentRoom.Name}\n{CurrentRoom.Description}\n";

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
            description = GetItem(playerAction, player);
            return description;
        }
        else if (playerAction.Contains(use))
        {
            //Call method
            description = UseItem(playerAction, player);
            return description;
        }
        else if (playerAction == help)
        {
            //Call method
            description = Help(player);
            return description;
        }
        else if (playerAction == exit)
        {
            Application.Exit();
            return "";
        }
        else
        {
            return "You can't go that way.";
        }
    }

    string Help(Player player)
    {
        string description = "";

        //show what kind of commands/actions the user can perform        
        description = $"List of possible actions to perform: \n";
        description += $"{player.look}, {player.inv}, {player.get}, {player.use}, {player.north}, {player.south}, {player.east}, {player.west} \n";
        return description;
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
        else
            description = $"{exitText} {exitsList}\n";

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

        return $"{description}\n";
    }//End of IterateItems

    string ShowInventory(Player player)
    {
        string description = "";
        const string itemsText = "You have: ";
        const string noItemsText = "You have no items";

        //Check if player has any items
        if (player.Inventory.Count > 0)
            description = $"{itemsText}\n";
        else
            description = $"{noItemsText}\n";

        //Show player's inventory 
        for (int i = 0; i < player.Inventory.Count; i++)
            description += $"- {player.Inventory[i]}\n";

        return description;
    }//End of ShowInventory

    string UseItem(string playerAction, Player player)
    {
        string description = "";
        //Note: this is just a test. Will need to implement (somehow) some sort of Infocom type parser.

        string[] tempItem = playerAction.Split(" ");

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
                        description = $"You are not in posession of a {tempItem[1]}";
                        return description;
                    }
                    // else if (item.Key != tempItem[1] && hasItem)
                    // {
                    //     Console.WriteLine($"That does not seem to work. Yet.");
                    //     return;
                    // }
                    else if (item.Key == tempItem[1] && hasItem && !correctRoom)
                    {
                        description = $"Unable to use {tempItem[1]} here. Perhaps another room?";
                        return description;
                    }
                    else if (item.Key == tempItem[1] && hasItem && correctRoom)
                    {
                        description = $"You insert the {item.Key} into the computer slot.\n";
                        description += "A hologram of a beautiful woman coalesce in front of you. 'Hello, I am SAL. How may I be of service?'\n";
                        player.CurrentRoom.Description += " There is a hologram of a beautiful woman here.";
                        return description;
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
                        description = $"You are not in posession of a {tempItem[1]}";
                        return description;
                    }
                    else if (item.Key == tempItem[1] && hasItem && !correctRoom)
                    {
                        description = $"Unable to use {tempItem[1]} here. Perhaps another room?";
                        return description;
                    }
                    else if (item.Key == tempItem[1] && hasItem && correctRoom)
                    {
                        description = $"You aim the {item.Key} at the robot and blast it to smithereens!";
                        description += "He looks slightly more depressed than before. ";
                        player.CurrentRoom.Description += " Smoldering ruins of what used to be a slightly depressed robot lies depressingly in front of the shuttle.";
                        return description;
                    }
                }
            }//End of Docking Bay "puzzle"
        }//End of Puzzles loop
        return description;

    }//End UseItem method
    string GetItem(string playerAction, Player player)
    {
        string description = "";
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
                description = "What do you want to get?";
                missingItem = true;
                return description;
                //break;
            }

            if (arrayWords[1].ToLower() == item.Key)
            {
                removeRoomItem = item.Key;
                player.Inventory.Add(item.Key);

                //Remove item from CurrentRoom's itemlist                
                player.CurrentRoom.Items.Remove(removeRoomItem);

                description = $"You pick up the {removeRoomItem}.";
                itemFound = true;
                return description;
                //break;
            }
        }

        //Inform user if item is not in CurrentRoom's itemlist
        if (!itemFound && missingItem == false)
            description = $"There is no '{arrayWords[1]}' to pick up!\n";

        return description;
    }//End of GetItem
}//End of class Player