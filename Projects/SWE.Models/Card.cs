using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public class Card
    {
        public Card(string Description, string Name)
        {
            this.Description = Description;
            this.Name = Name;
        }
        public enum Type
        {
            MonsterCard,
            SpellCard
        }
        public enum Element
        {
            Fire,
            Water,
            Normal
        }
        public string Description;
        public string Name;

        void Attack()
        {
            //todo
        }
    }

    public class MonsterCard : Card { 
    
        public MonsterCard(string Description, string Name)
            : base(Description, Name) { }

        public int HP;
        public int Dmg;
        public enum MonsterType{
            Goblin,
            Dragon,
            Wizard,
            Ork,
            Knight,
            Kraken,
            FireElve
        }
    }

    public class Spellcard : Card
    {
        public int Dmg;
        public enum Effect
        {
            //effects to be implemented
        }
    }
}
