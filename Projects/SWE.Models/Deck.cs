using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public class Deck
    {
        public Deck(string Name) {
            this.Name = Name;
            this.PlayerStack = new List<Card>();
        }
        public string Name;
        
        public List<Card> PlayerStack { get; set; }
    }
}
