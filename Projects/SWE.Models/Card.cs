using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public abstract class Card
    {
        public Card(string Description, string Name, int Dmg)
        {
            this.Description = Description;
            this.Name = Name;
            this.Dmg = Dmg;
        }

        public enum Element
        {
            Fire = 0,
            Water = 1,
            Normal = 2
        }

        public string Description;
        public string Name;
        public int Dmg;
        public Type CardType;
        public bool IsChosen;

        public virtual void Attack(Card OpponentCard)
        { 
            //todo
        }
    }

    public class MonsterCard : Card 
    { 
    
        public MonsterCard(string Description, string Name, int Dmg)
            : base(Description, Name, Dmg) { }

        public int HP;
        public enum MonsterType{
            Goblin = 0,
            Dragon = 1,
            Wizard = 2,
            Ork = 3,
            Knight = 4,
            Kraken = 5,
            FireElve = 6
        }

        public override void Attack(Card OpponentCard)
        {
            //to be implemented
        }
    }

    public class SpellCard : Card
    {
        public SpellCard(string Description, string Name, int Dmg)
            : base(Description, Name, Dmg) { }
        public enum Effect
        {
            //effects to be implemented
        }

        public override void Attack(Card OpponentCard)
        {
            //to be implemented
        }
    }
}
