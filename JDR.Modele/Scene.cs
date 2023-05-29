using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDR.Model
{
    public class Scene
    {
        public string? Name { get; set; }
        public IList<Music> Musics { get; set; }
        public Illustration? Background { get; set; }
        public IList<NPC> Characters { get; set; }
        public IList<Obstacle> Obstacles { get; set; }
        public bool HasFogOfWarEnable { get; set; }
        public double ZoomValue { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public int XMapTranslation { get; set; }
		public int YMapTranslation { get; set; }
        public Point PlayerSpawnPoint { get; set; }

        public Scene()
        {
            Musics = new List<Music>();
            Characters = new List<NPC>();
            Obstacles = new List<Obstacle>();
        }
        public Scene(string name) {
            Name = name;
			Musics = new List<Music>();
			Characters = new List<NPC>();
            Obstacles = new List<Obstacle>();
		}

		public override string ToString() {
            return Name ?? "Sans titre";
		}

	}
}
