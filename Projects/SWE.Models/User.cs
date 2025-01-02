using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public class User
    {
        public User(string userName, string password, int id, int elo, int coins, int packages, Deck stack, Deck playingDeck, int wins, int losses)
        {
            UserName = userName;
            Password = password;
            ID = id;
            Elo = elo;
            Coins = coins;
            Packages = packages;
            Stack = stack;
            PlayingDeck = playingDeck;
            Wins = wins;
            Losses = losses;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
        public int Elo { get; set; }
        public int Coins { get; set; }
        public int Packages { get; set; }
        public Deck Stack { get; set; }
        public Deck PlayingDeck { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        private string Token { get; set; }

        public void ChooseDeck()
        {
            if (Stack?.PlayerStack == null || PlayingDeck?.PlayerStack == null)
            {
                Console.WriteLine("Decks are not initialized properly.");
                return;
            }

            if (Stack.PlayerStack.Count == 0)
            {
                Console.WriteLine("You have no Cards to choose from!");
                return;
            }

            if (Stack.PlayerStack.Count <= 4)
            {
                foreach (var card in Stack.PlayerStack)
                {
                    PlayingDeck.PlayerStack.Add(card);
                }
            }
            else
            {
                Console.WriteLine("Choose your Cards for your Deck!\n");
                DisplayCards(Stack.PlayerStack);

                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine($"Choose your {i} Card: ");
                    if (int.TryParse(Console.ReadLine(), out int cardNumber) && IsValidCardChoice(cardNumber - 1))
                    {
                        var chosenCard = Stack.PlayerStack[cardNumber - 1];
                        PlayingDeck.PlayerStack.Add(chosenCard);
                        chosenCard.IsChosen = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid card.");
                        i--;
                    }
                }
            }
        }

        private void DisplayCards(List<Card> cards)
        {
            int counter = 1;
            foreach (var card in cards)
            {
                if (!card.IsChosen)
                {
                    Console.WriteLine($"{counter}: {card.Name}: {card.Damage}");
                }
                counter++;
            }
        }

        private bool IsValidCardChoice(int cardNumber)
        {
            return cardNumber >= 0 && cardNumber < Stack.PlayerStack.Count && !Stack.PlayerStack[cardNumber].IsChosen;
        }

        public void ShowPlayerStack()
        {
            foreach (var card in Stack.PlayerStack)
            {
                Console.WriteLine($"{card.Name}/{card.Damage}/{card.Description}");
            }
        }

        public void ShowPlayerDeck()
        {
            if (PlayingDeck?.PlayerStack == null)
            {
                Console.WriteLine("Playing Deck is not initialized properly.");
                return;
            }

            if (PlayingDeck.PlayerStack.Count == 0)
            {
                Console.WriteLine("You have no Cards in your Deck!");
                return;
            }

            foreach (var card in PlayingDeck.PlayerStack)
            {
                Console.WriteLine($"{card.Name}/{card.Damage}/{card.Description}");
            }
        }

        public void TradeCard(User userOne, User userTwo, int userCardOne, int userCardTwo)
        {
            var cardFromUserTwo = userTwo.PlayingDeck.PlayerStack[userCardTwo];
            var cardFromUserOne = userOne.Stack.PlayerStack[userCardOne];

            userOne.Stack.PlayerStack.Add(cardFromUserTwo);
            userOne.Stack.PlayerStack.Remove(cardFromUserOne);

            userTwo.Stack.PlayerStack.Add(cardFromUserOne);
            userTwo.Stack.PlayerStack.Remove(cardFromUserTwo);
        }

        public void BuyPackage()
        {
            if (Coins >= 5)
            {
                Coins -= 5;
                Packages++;
            }
        }

        public void OpenPackage()
        {
            if (Packages == 0) return;

            Packages--;

            var rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                var cardType = rnd.Next(0, 2);
                var cardElement = rnd.Next(0, 3);

                if (cardType == 0)
                {
                    Stack.PlayerStack.Add(new MonsterCard("This Card has a lot of normal Damage", "Blue Eyes White Dragon", 100));
                }
                else
                {
                    var cardName = cardElement switch
                    {
                        0 => "Fireball",
                        1 => "Waterball",
                        _ => "Normalball"
                    };
                    var cardDescription = cardElement switch
                    {
                        0 => "This Card has a lot of Fire Damage",
                        1 => "This Card has a lot of Water Damage",
                        _ => "This Card has a lot of Normal Damage"
                    };
                    Stack.PlayerStack.Add(new SpellCard(cardDescription, cardName, 150));
                }
            }
        }

        public int GetCoins() => Coins;
        public int GetPackages() => Packages;
        public int GetElo() => Elo;

        public int CalculateElo() => Wins * 3 - Losses * 5 + 100;
    }
}
