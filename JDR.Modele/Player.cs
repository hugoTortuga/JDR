﻿namespace JDR.Model
{
    public class Player : Entity
    {
        private string _Name;
        public string Name { get => _Name; set
            {
                if (value == null || (value != null && value.Length <= 50))
                {
                    _Name = value;
                }
            }
        }

        public Illustration Illustration { get; set; }
        public string? Description { get; set; }
        public int Level { get; set; }
        public Race Race { get; set; }
        public Skills Skills { get; set; }
        public Inventory Inventory { get; set; }
        public IList<Spell> Spells { get; set; }

        public Player(string name, Race race, Skills skills)
        {
            Name = name;
            Skills = skills;
            Race = race;
            Spells = new List<Spell>();
            Inventory = new Inventory();
        }

        public override string ToString() {
            return $"{Name}, {Races.ToString(Race)}";
        }
    }
}