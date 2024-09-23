// See https://aka.ms/new-console-template for more information
using SWE.Models;

/*Deck deck1 = new("Deck1");

User user1 = new("User1", 1, 1000, 100, 10, deck1);
user1.OpenPackage();

foreach (Card Card in user1.Deck.PlayerDeck)
{
    Console.WriteLine($"{Card.Description}/{Card.Name}/{Card.Dmg}");
    //Console.WriteLine(Card.Name);
    //Console.WriteLine(Card.Dmg);
}*/

string UserInput = "v";
Deck deck1 = new Deck("Deck1");
User newUser = new User("User1", 1, 100, 20, 0, deck1);

Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("\nWelcome to the MTCG\n\n");


while (UserInput != "E")
{
    if (newUser.getPackages() != 0)
    {
        Console.WriteLine("You have " + newUser.getPackages() + " packages to open.\n Press (O) to open them");
    }
    else
    {
        Console.WriteLine("You have no packages to open.");
    }
    Console.WriteLine("Press (X) to buy some Monster Packages:\nPress (E) to exit.\nCoins: " + newUser.getCoins());

    UserInput = Console.ReadLine();

    if (UserInput == "X" || UserInput == "x")
    {
        newUser.BuyPackage();
        foreach (Card Card in newUser.Deck.PlayerDeck)
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
        foreach (Card Card in newUser.Deck.PlayerDeck)
        {
            Console.WriteLine($"{Card.Description}/{Card.Name}/{Card.Dmg}");
        }
    }
    else
    {
        Console.WriteLine("Invalid Input");
    }
}