using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Modele
{
    public class NPC : Entity
    {
        public int Id { get; }

        private int _HostilityLevel;
        public int HostilityLevel
        {
            get => _HostilityLevel;
            set
            {
                if (value > -11 || value < 11)
                {
                    _HostilityLevel = value;
                }
            }
        }

        public NPC(int hostilityLevel) {
            HostilityLevel = hostilityLevel;
        }

    }
}
