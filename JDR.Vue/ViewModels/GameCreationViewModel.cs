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

        private readonly ServiceBase _Service;

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

        public GameCreationViewModel(ServiceBase service)
        {
            _CurrentGame = new GameViewModel(new Game(new ObservableCollection<Scene>()));
            _Service = service;
        }

        public void GoToMenu() {
		}

        public void AddAScene() {
            CurrentScene = new Scene("Scène sans titre");
			CurrentGame.Scenes.Add(CurrentScene);
        }

	}
}
