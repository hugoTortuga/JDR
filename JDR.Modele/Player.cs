namespace JDR.Model
{
    public class Player : Entity
    {
        private string? _Name;
        public string? Name { get => _Name; set
            {
                if (value == null || (value != null && value.Length <= 50))
                {
                    _Name = value;
                }
            }
        }

        public string? Description { get; set; }
        public int Level { get; set; }
        public Race Race { get; set; }
        public Skills Skills { get; set; }

        public IList<Spell> Spells { get; set; }

        public Player(string name, Race race, Skills skills)
        {
            Name = name;
            Skills = skills;
            Spells = new List<Spell>();
            Race = race;
        }

        public override string ToString() {
            return Name;
        }
    }
}