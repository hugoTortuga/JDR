using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public enum Race {
		Human,
		Dwarf,
		Elve,
		HumanDwarf,
		HumanElve,
		DwarfElve
	}

	public static class Races {
		public static string ToString(Race race) {
			switch (race) {
				case Race.Human:
					return "Humain";
				case Race.Dwarf:
					return "Nain";
				case Race.Elve:
					return "Elfe";
				case Race.HumanDwarf:
					return "Demi-Nain";
				case Race.HumanElve:
					return "Demi-Elfe";
				case Race.DwarfElve:
					return "Nain-Elfe";
				default:
					throw new NotImplementedException();
			}
		}
	}
}
