using JDR.Core;
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


        private Character _Player;
        public Character Player {
            get {
                return (_Player);
            }
            set {
                _Player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        private GameCore _GameCore;

        public CharacterSheetViewModel(Character player, GameCore gameCore)
        {
            Player = player;
            _GameCore = gameCore;
        }

        public void Save()
        {
            _GameCore.SaveCharacter(Player);
        }

    }
}
