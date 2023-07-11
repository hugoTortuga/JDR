using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class Inventory {

        public Guid Id { get; set; }
        public IList<InventoryItem> Objects { get; set; }

        public Inventory()
        {
			Objects = new List<InventoryItem>();
        }

		public override string ToString() {
			string print = string.Empty;
			Objects.ToList().ForEach(x => print += x.ToString() + "\n");
			return print;
		}

	}
}
