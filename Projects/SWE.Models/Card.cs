using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public abstract class Card
    {
        protected Card(string description, string name, int damage)
        {
            Description = description;
            Name = name;
            Damage = damage;
        }

        public enum Element
        {
            Fire,
            Water,
            Normal
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public bool IsChosen { get; set; }

        public abstract void Attack(Card opponentCard);
    }

    public class MonsterCard : Card
    {
        public MonsterCard(string description, string name, int damage)
            : base(description, name, damage) { }

        public int HP { get; set; }

        public enum MonsterType
        {
            Goblin,
            Dragon,
            Wizard,
            Ork,
            Knight,
            Kraken,
            FireElve
        }

        public override void Attack(Card opponentCard)
        {
            // Implementation to be added
        }
    }

    public class SpellCard : Card
    {
        public SpellCard(string description, string name, int damage)
            : base(description, name, damage) { }

        public enum Effect
        {
            // Effects to be implemented
        }

        public override void Attack(Card opponentCard)
        {
            // Implementation to be added
        }
    }
}
