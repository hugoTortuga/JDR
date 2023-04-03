using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class Spell {
		public bool IsPassive { get; set; }
		public int Level { get; set; }
		public int Cost { get; set; }
		public MagicCategory Category { get; set; }
		public string? Description { get; set; }

		public override string ToString() {
			return $"{MagicCategories.ToString(Category)}, niveau {Level}";
		}

	}

	public enum MagicCategory {
		Nature,
		Animal,
		Darkness,
		Brightness,
		Psy,
		Heal,
		Water,
		Fire,
		Air,
		Enchantement
	}

	public class MagicCategories {
		public static string ToString(MagicCategory category) {
			return category switch {
				MagicCategory.Nature => "Nature",
				MagicCategory.Animal => "Animal",
				MagicCategory.Darkness => "Ténèbres",
				MagicCategory.Brightness => "Lumière",
				MagicCategory.Psy => "Psychique",
				MagicCategory.Heal => "Guérison",
				MagicCategory.Water => "Eau",
				MagicCategory.Fire => "Feu",
				MagicCategory.Air => "Air",
				MagicCategory.Enchantement => "Enchantement",
				_ => throw new NotImplementedException(),
			};
		}
	}
}
