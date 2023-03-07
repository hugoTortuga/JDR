using JDR.Model;

public class Game {
	public Game() {
	}

	public void Start() {
		TestAStartingWeapon();
		TestTheDice();
	}

	public void TestTheDice() {
		Console.WriteLine("Lançons les dés pour voir si ils ne sont pas rouillés");
		Console.WriteLine("Mon dé 100 fait " + new Dice(100).Roll());
	}

	public void TestAStartingWeapon() {
		var startingWeapon = GetStartingWeapon()[0];
		Console.WriteLine(startingWeapon.Name + " a fait " + startingWeapon.DicesDamage.RollTheDice() + " dégât(s)");
	}

	private IList<Weapon> GetStartingWeapon() {
		return new List<Weapon> {
			new Weapon("Arc long", Speed.Slow, "2d8")
		};
	}
}