using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class BattleLogic
    {
        public BattleLogic(int roundCounter, User player1, User player2)
        {
            RoundCounter = roundCounter;
            Player1 = player1;
            Player2 = player2;
        }

        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public int RoundCounter { get; set; }

        private bool IsPureMonster(int currentPlayer1Card, int currentPlayer2Card)
        {
            return Player1.PlayingDeck.PlayerStack[currentPlayer1Card].GetType() == Player2.PlayingDeck.PlayerStack[currentPlayer2Card].GetType();
        }

        private bool IsEndlessLoop()
        {
            return RoundCounter > 100;
        }
    }
}
