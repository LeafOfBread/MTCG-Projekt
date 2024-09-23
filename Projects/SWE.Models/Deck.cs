using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class Deck
    {
        public Deck(string Name) {
            this.Name = Name;
        }
        public string Name;
        
        public Vector<Card> PlayerDeck { get; set; }
    }
}
