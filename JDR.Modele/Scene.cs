using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Scene
    {
        public string Name { get; set; }
        public Music? Music { get; set; }
        public IList<Illustration> Illustrations { get; set; }

        public Scene(string name) {
            Name = name;
            Illustrations = new List<Illustration>();
        }

		public override string ToString() {
            return Name;
		}

	}
}
