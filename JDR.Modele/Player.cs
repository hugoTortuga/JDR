namespace JDR.Model
{
    public class Player : Entity
    {
        public int Id { get; }

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

        public Skills Skills { get; set; }

        public Player(string name, Skills skills)
        {
            Name = name;
            Skills = skills;
        }

    }
}