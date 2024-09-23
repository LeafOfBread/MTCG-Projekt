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
        public User(string UserName, int ID, int Elo, int Coins, int Packages, Deck Deck)
        {
            this.UserName = UserName;
            this.ID = ID;
            this.Elo = Elo;
            this.Coins = Coins;
            this.Packages = Packages;
            this.Deck = Deck;
        }
        public int ID { get; set; }
        public string UserName;
        private string Password;
        private string Token;
        public int Elo;
        private int Coins { get; set; }
        public Deck Deck;
        private int Packages;

        void ChooseDeck()
        {
            if (this.Deck.PlayerDeck.Count == 0)
            {
                return;
            }
            else if (this.Deck.PlayerDeck.Count == 1)
            {
                this.Deck.PlayerDeck[0] = this.Deck.PlayerDeck[0];
            }
            else
            {
              //todo
            }
        }
        void TradeCard()
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
            this.Packages--;
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                int CardType = rnd.Next(0, 2);
                int CardElement = rnd.Next(0, 3);
                if (CardType == 0)
                {
                    this.Deck.PlayerDeck.Add(new MonsterCard("This Card has a lot of normal Damage","Blue Eyes White Dragon", 100));
                }
                else
                {
                    if (CardElement == 0) this.Deck.PlayerDeck.Add(new SpellCard("This Card has a lot of Fire Damage", "Fireball", 150));
                    else if (CardElement == 1) this.Deck.PlayerDeck.Add(new SpellCard("This Card has a lot of Water Damage", "Waterball", 150));
                    else this.Deck.PlayerDeck.Add(new SpellCard("This Card has a lot of Normal Damage", "Normalball", 150));
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
