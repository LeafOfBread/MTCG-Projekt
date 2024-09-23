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
        public User(string UserName, int ID, int Elo, int Coins, int Packages, Deck Stack, Deck PlayingDeck)
        {
            this.UserName = UserName;
            this.ID = ID;
            this.Elo = Elo;
            this.Coins = Coins;
            this.Packages = Packages;
            this.Stack = Stack;
            this.PlayingDeck = PlayingDeck;
        }
        private int ID { get; set; }
        public string UserName;
        private string Password;
        private string Token;
        public int Elo;
        private int Coins { get; set; }
        public Deck Stack;
        public Deck PlayingDeck;
        private int Packages;

        public void ChooseDeck()
        {
            if (this.Stack?.PlayerStack == null || PlayingDeck?.PlayerStack == null)
            {
                Console.WriteLine("Decks are not initialized properly.");
                return;
            }

            if (this.Stack.PlayerStack.Count == 0)
            {
                Console.WriteLine("You have no Cards to choose from!");
                return;
            }
            else if (this.Stack.PlayerStack.Count <= 3)
            {
                foreach (Card Card in this.Stack.PlayerStack)
                {
                    PlayingDeck.PlayerStack.Add(Card);
                }
            }
            else
            {
                Console.WriteLine("Choose your Cards for your Deck!\n");
                int counter = 1;
                foreach (Card Card in this.Stack.PlayerStack)
                { 
                    Console.WriteLine($"{counter} {Card.Name}: {Card.Dmg}");
                    counter++;
                }
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("Choose your " + $"{i}" + " Card: ");
                    int CardNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (CardNumber >= 0 && CardNumber < this.Stack.PlayerStack.Count)
                    {
                        PlayingDeck.PlayerStack.Add(this.Stack.PlayerStack[CardNumber]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid card number.");
                    }
                }
            }
        }


        public void showPlayerDeck()
        {
            foreach (Card Card in this.Stack.PlayerStack)
            {
                Console.WriteLine($"{Card.Name}/{Card.Dmg}/{Card.Description}");
            }
        }

            public void TradeCard()
        {
            //to be implemented
        }
        public void BuyPackage()
        {
            this.Coins = this.Coins - 5;
            this.Packages++;
        }

        public void OpenPackage()
        {
            if (this.Packages == 0)
            {
                return;
            }
            this.Packages--;

            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                int CardType = rnd.Next(0, 2);
                int CardElement = rnd.Next(0, 3);
                if (CardType == 0)
                {
                    this.Stack.PlayerStack.Add(new MonsterCard("This Card has a lot of normal Damage","Blue Eyes White Dragon", 100));
                }
                else
                {
                    if (CardElement == 0) this.Stack.PlayerStack.Add(new SpellCard("This Card has a lot of Fire Damage", "Fireball", 150));
                    else if (CardElement == 1) this.Stack.PlayerStack.Add(new SpellCard("This Card has a lot of Water Damage", "Waterball", 150));
                    else this.Stack.PlayerStack.Add(new SpellCard("This Card has a lot of Normal Damage", "Normalball", 150));
                }
            }
        }

        void Login()
        {
            //to be implemented
        }
        void Register()
        {
            //to be implemented
        }

        public int getCoins()
        {
            return this.Coins;
        }
        public int getPackages()
        {
            return this.Packages;
        }
    }
}
