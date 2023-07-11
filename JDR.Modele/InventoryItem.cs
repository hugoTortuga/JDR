using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class InventoryItem 
	{
        public Guid Id { get; set; }
        public string? Name { get; set; }
		public string? Description { get; set; }
		public Illustration? Illustration { get; set; }

		public InventoryItem() 
		{ 
		}
		public InventoryItem(string name) {
			Name = name;
			Illustration = Illustration.None();
		}

		public InventoryItem(string name, Illustration illustration) {
			Name = name;
			Illustration = illustration;
		}

		public override string ToString() {
			return Name;
		}
	}
}
