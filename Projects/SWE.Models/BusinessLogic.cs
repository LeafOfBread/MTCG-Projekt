using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class BusinessLogic
    {
        public BusinessLogic(int RoundCounter, User Player1, User Player2)
        {
            this.RoundCounter = RoundCounter;
            this.Player1 = Player1;
            this.Player2 = Player2;
        }
        public User Player1 { get; set; }
        public User Player2 { get; set; }

        public int RoundCounter { get; set; }

        bool IsPureMonster()
        {
            if (Player1.PlayingDeck.PlayerStack[0].GetType == Player2.PlayingDeck.PlayerStack[0].GetType) return true;
            return false;
        }
        
        void ChooseRandomCard()
        {
            //to be implemented
        }
        bool IsEndlessLoop()
        {
            if (this.RoundCounter > 100) return true;
            else return false;
        }
    }
}
