namespace JDR.Modele
{
    public class Player : Entity
    {
        public int Id { get; }
        public string Name { get; set; }
        public string? Description { get; set; } 

        public Skills Skills { get; set; }

        public Player(string name, Skills skills)
        {
            Name = name;
            Skills = skills;
        }

    }
}