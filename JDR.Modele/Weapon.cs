using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Weapon : InventoryItem
    {
        public int NumberOfHand { get; set; }
        public Speed Speed { get; set; }

        public int BaseDamage { get; set; }

        public Dices DicesDamage { get; set; }

        public Weapon(string name, Speed speed, string valueDice) : base (name)
        { 
            Name = name;
            Speed = speed;
            DicesDamage = new Dices(valueDice);
        }

    }
}
