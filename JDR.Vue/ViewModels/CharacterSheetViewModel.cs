using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class CharacterSheetViewModel : ViewModelBase
    {


        private Player _Player;
        public Player Player {
            get {
                return (_Player);
            }
            set {
                _Player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public CharacterSheetViewModel(Player player)
        {
            Player = player;
        }

    }
}
