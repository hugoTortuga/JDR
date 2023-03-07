using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class InventoryObject {

		public string Name { get; set; }
		public Illustration Illustration { get; set; }

		public InventoryObject(string name) {
			Name = name;
			Illustration = Illustration.None();
		}

		public InventoryObject(string name, Illustration illustration) {
			Name = name;
			Illustration = illustration;
		}
	}
}
