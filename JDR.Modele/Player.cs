namespace JDR.Modele
{
    public class Player : Entity
    {
        public int Id { get; }
        public string Name { get; set; }
        public string? Description { get; set; } 

        public Player(string name)
        {
            Name = name;
        }

    }
}