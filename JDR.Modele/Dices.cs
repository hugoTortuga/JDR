using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JDR.Model {
	public partial class Dices {

		public IList<Dice> PlayDices { get; set; }
		public Dices(string numberOfDicesAndFaces) {

			var match = CheckMatchRegex(numberOfDicesAndFaces);

			var numberOfDice = int.Parse(match.Groups[1].Value);
			var numberOfFace = int.Parse(match.Groups[2].Value);

			SetDices(numberOfDice, numberOfFace);
		}

		private Match CheckMatchRegex(string numberOfDicesAndFaces) {
			ArgumentException.ThrowIfNullOrEmpty(numberOfDicesAndFaces);
			var match = StringDiceControl().Match(numberOfDicesAndFaces);
			if (!match.Success) throw new ArgumentException("The dice string parameter is invalid");
			return match;
		}

		public Dices(IList<Dice> dices) {
			PlayDices = dices;
		}

		private void SetDices(int numberDice, int numberFace) {
			PlayDices = new List<Dice>();
			for (int i = 0; i < numberDice; i++) {
				PlayDices.Add(new Dice(numberFace));
			}
		}

		public int RollTheDice() {
			return PlayDices.Select(d => d.Roll()).Sum();
		}

		[GeneratedRegex("(\\d+)?d(\\d+)")]
		private static partial Regex StringDiceControl();
	}
}
