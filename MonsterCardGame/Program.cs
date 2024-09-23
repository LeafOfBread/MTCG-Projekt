// See https://aka.ms/new-console-template for more information
using SWE.Models;


string UserInput = "";
Deck deck1 = new Deck("Deck1");
Deck PlayingDeck1 = new Deck("PlayingDeck1");
User newUser = new User("User1", 1, 100, 20, 0, deck1, PlayingDeck1, 10, 2);

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\nWelcome to the MTCG\n\n");


while (UserInput != "E" || UserInput != "e")
{
    Console.Clear();
    if (newUser.getPackages() != 0) Console.WriteLine("You have " + newUser.getPackages() + " packages to open.\n Press (O) to open them");

    else Console.WriteLine("You have no packages to open.");
    if (newUser.getCoins() < 5) Console.WriteLine("You do not have enough coins to buy a package.");

    Console.WriteLine("Current ELO: " + $"{newUser.getElo()}");

    Console.WriteLine("Press (X) to buy some Monster Packages:\nPress (C) to choose your Cards for your Deck.\nPress (E) to exit.\nCoins: " + newUser.getCoins());

    UserInput = Console.ReadLine();

    if (UserInput == "X" || UserInput == "x")
    {
        newUser.BuyPackage();
        foreach (Card Card in newUser.Stack.PlayerStack)
        {
            Console.WriteLine($"{Card.Description}/{Card.Name}/{Card.Dmg}");
        }
    }
    else if (UserInput == "E" || UserInput == "e")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (UserInput == "O" || UserInput == "o")
    {
        newUser.OpenPackage();
        foreach (Card Card in newUser.Stack.PlayerStack)
        {
            Console.WriteLine($"{Card.Description}/{Card.Name}/{Card.Dmg}");
        }
    }
    else if (UserInput == "C" || UserInput == "c")
    {
        newUser.ChooseDeck();
        newUser.showPlayerDeck();
    }
    else
    {
        Console.WriteLine("Invalid Input");
    }
}