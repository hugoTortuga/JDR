using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model {
	public class Game {

        public int MaxPlayer { get; set; }
        public IList<Scene> Scenes { get; set; }

        public Game(IList<Scene> scenes)
        {
            Scenes = scenes;
        }

    }
}
