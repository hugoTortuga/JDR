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

    }

	public enum MagicCategory {
		Nature,
		Animal,
		Darkness,
		Brightness,
		Psy,
		Heal
	}
}
