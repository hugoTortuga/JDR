using System.ComponentModel.DataAnnotations.Schema;

namespace JDR.Model
{
    public class Character : Entity
    {
        public Guid Id { get; set; }

        private string? _Name;
        public string? Name { get => _Name; set
            {
                if (value == null || (value != null && value.Length <= 50))
                {
                    _Name = value;
                }
            }
        }

        public Illustration? Illustration { get; set; }
        public Illustration? Token { get; set; }
        public string? Description { get; set; }
        public int Level { get; set; }

        public Race Race { get; set; }
        public Skills Skills { get; set; }
        public Inventory Inventory { get; set; }
        public IList<Spell> Spells { get; set; }
        public int CurrentXP { get; set; }


        public Character()
        {
            Inventory = new Inventory();
            Spells = new List<Spell>();
            Skills = new Skills();
        }

        public Character(Guid id) : this()
        {
            Id = id;
        }

        public Character(string name, Race race, Skills skills)
        {
            Name = name;
            Skills = skills; 
            Inventory = new Inventory();
            Spells = new List<Spell>();
            Race = race;
        }

        public override string ToString() {
            return $"{Name}, {Race?.Name}";
        }
    }
}