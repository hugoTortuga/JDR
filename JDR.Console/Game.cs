using JDR.Model;

public class Game
{
    public Game()
    {
    }

    public void Start()
    {
        Console.WriteLine("Lançons les dés pour voir si ils ne sont pas rouillés");
        var d6 = new Dice(6);
        var d20 = new Dice(20);
        var d100 = new Dice(100);
        Console.WriteLine("Mon dé 6 fait " + d6.Roll());
        Console.WriteLine("Mon dé 20 fait " + d20.Roll());
        Console.WriteLine("Mon dé 100 fait " + d100.Roll());
    }
}