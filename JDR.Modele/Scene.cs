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
        public IList<Music> Musics { get; set; }
        public Illustration? Background { get; set; }
        public IList<NPC> Characters { get; set; }

        public IList<Obstacle> Obstacles { get; set; }

        public Scene(string name) {
            Name = name;
			Musics = new List<Music>();
			Characters = new List<NPC>();
            Obstacles = new List<Obstacle>();
		}

		public override string ToString() {
            return Name;
		}

	}
}
