using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Weapon
    {

        public string? Name { get; set; }
        public Speed Vitesse { get; set; }
        public Illustration Illustration { get; set; }

        public Weapon(string name, Speed vitesse) 
        { 
            Name = name;
            Vitesse = vitesse;
        }

    }
}
