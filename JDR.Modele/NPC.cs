using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class NPC : Entity
    {
        public Guid Id { get; set; }

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

        public int XPLevelWhenKilled { get; set; }

        public NPC(int hostilityLevel) {
            HostilityLevel = hostilityLevel;
        }

    }
}
