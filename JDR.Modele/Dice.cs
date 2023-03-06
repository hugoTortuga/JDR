using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Modele
{
    public class Dice
    {

        public int NumberFaces { get; set; }
        public Dice(int numberFaces)
        {
            if (numberFaces <= 0) throw new ArgumentException("The number of faces should be at least one");
            NumberFaces = numberFaces;
        }

        public int Roll()
        {
            return new Random().Next(NumberFaces) + 1;
        }
    }
}
