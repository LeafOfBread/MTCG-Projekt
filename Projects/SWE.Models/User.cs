﻿using System;
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
            else if (this.Stack.PlayerStack.Count <= 4)
            {
                foreach (Card Card in this.Stack.PlayerStack)
                    PlayingDeck.PlayerStack.Add(Card);
            }
            else if (this.Stack.PlayerStack.Count >= 5)
            {
                Console.WriteLine("Choose your Cards for your Deck!\n");
                int counter = 1;
                foreach (Card Card in this.Stack.PlayerStack)
                {
                    if (!Card.IsChosen) Console.WriteLine($"{counter}: {Card.Name}: {Card.Dmg}");
                    counter++;
                }
                for (int i = 1; i < 5; i++)
                {
                    inputAgain:;
                    Console.WriteLine("Choose your " + $"{i}" + " Card: ");
                    int CardNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (CardNumber >= 0 && CardNumber < this.Stack.PlayerStack.Count && !this.Stack.PlayerStack[CardNumber].IsChosen)
                    {
                        PlayingDeck.PlayerStack.Add(this.Stack.PlayerStack[CardNumber]);
                        this.Stack.PlayerStack[CardNumber].IsChosen = true;
                    }
                    else if (CardNumber < 0 || CardNumber >= this.Stack.PlayerStack.Count || this.Stack.PlayerStack[CardNumber].IsChosen)
                    {
                        counter--;
                        Console.WriteLine("Invalid card.");
                        i--;
                    }
                }
            }
        }


        public void showPlayerStack()
        {
            foreach (Card Card in this.Stack.PlayerStack)
            {
                Console.WriteLine($"{Card.Name}/{Card.Dmg}/{Card.Description}");
            }
        }

        public void showPlayerDeck()
        {
            if (this.PlayingDeck?.PlayerStack == null)
            {
                Console.WriteLine("Playing Deck is not initialized properly.");
                return;
            }
            else if (this.PlayingDeck.PlayerStack.Count == 0)
            {
                Console.WriteLine("You have no Cards in your Deck!");
                return;
            }
            else foreach (Card Card in this.PlayingDeck.PlayerStack)
            {
                Console.WriteLine($"{Card.Name}/{Card.Dmg}/{Card.Description}");
            }
        }

            public void TradeCard(User UserOne, User UserTwo, int UserCardOne, int UserCardTwo)
        {
            UserOne.Stack.PlayerStack.Add(UserTwo.PlayingDeck.PlayerStack[UserCardTwo]);
            UserOne.Stack.PlayerStack.Remove(UserOne.Stack.PlayerStack[UserCardOne]);

            UserTwo.Stack.PlayerStack.Add(UserOne.PlayingDeck.PlayerStack[UserCardOne]);
            UserTwo.Stack.PlayerStack.Remove(UserTwo.Stack.PlayerStack[UserCardTwo]);
        }
        public void BuyPackage()
        {
            if (this.Coins >= 5)
            {
                this.Coins = this.Coins - 5;
                this.Packages++;
            }
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

        public int getElo()
        {
            return this.Elo;
        }
        public int EloCalculation()
        {
            return Wins * 3 - Losses * 5 + 100;
        }
    }
}
