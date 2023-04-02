using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class Inventory {

		public IList<InventoryObject> Objects { get; set; }

        public Inventory()
        {
			Objects = new List<InventoryObject>();
        }

		public override string ToString() {
			string print = string.Empty;
			Objects.ToList().ForEach(x => print += x.ToString() + "\n");
			return print;
		}

	}
}
