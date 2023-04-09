using JDR.Core;
using JDR.Model;
using JDR.Service;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class GameCreationViewModel : ViewModelBase {


        private GameViewModel _CurrentGame;
        public GameViewModel CurrentGame {
            get {
                return (_CurrentGame);
            }
            set {
                _CurrentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }

        private Scene _CurrentScene;
        public Scene CurrentScene {
            get {
                return (_CurrentScene);
            }
            set {
                _CurrentScene = value;
                OnPropertyChanged(nameof(CurrentScene));
            }
        }

        private GameCore _GameCore;

        public GameCreationViewModel(GameCore gameCore)
        {
			_GameCore = gameCore;
            var currentGame = _GameCore.GetLastGame();
            if (currentGame == null) {
                currentGame = new Game();
            }
			_CurrentGame = new GameViewModel(currentGame);
            CurrentScene = new Scene("Pas de nom");

		}

        public void GoToMenu() {
		}

        public void AddAScene() {
            CurrentScene = new Scene("Scène sans titre");
			CurrentGame.Scenes.Add(CurrentScene);
        }

	}
}
