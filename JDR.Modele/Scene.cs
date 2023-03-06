using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Modele
{
    public class Scene
    {

        public Music? Music { get; set; }
        public IList<Illustration> Illustrations { get; set; }

        public Scene() {
            Illustrations = new List<Illustration>();
        }

    }
}
