using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class User
    {
        public User(string UserName)
        {
            this.UserName = UserName;
        }
        public int ID { get; set; }
        public string UserName;
        public int Elo;
        private int Coins;
        public required Deck Deck;
        private int Packages;

        void ChooseCard()
        {
            //to be implemented
        }

    }
}
