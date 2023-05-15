using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Music
    {

        public int DurationInSecond;
        public byte[]? Content { get; set; }
        public string? Name { get; set; }

        public Music()
        {
        }

        public override string ToString()
        {
            var name = Name ?? "Pas de musique";
            var duration = DurationInSecond == 0 ? "0min" : (DurationInSecond / 60 + "min" + DurationInSecond % 60);
            return $"{name} - {duration}";
        }

    }
}
