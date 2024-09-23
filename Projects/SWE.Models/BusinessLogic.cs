using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class BusinessLogic
    {
        public required User Player1 { get; set; }
        public required User Player2 { get; set; }

        public int RoundCounter = 0;

        bool IsPureMonster()
        {
            //to be implemented
            return false;
        }
        
        void ChooseRandomCard()
        {
            //to be implemented
        }
    }
}
