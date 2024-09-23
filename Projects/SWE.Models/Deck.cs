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
            this.PlayerDeck = new List<Card>();
        }
        public string Name;
        
        public List<Card> PlayerDeck { get; set; }
    }
}
