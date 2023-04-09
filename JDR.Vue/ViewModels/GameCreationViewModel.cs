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


		private Game _CurrentGame;
		public Game CurrentGame {
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

		private MapEditorViewModel _MapEditorViewModel;
		public MapEditorViewModel MapEditorViewModel {
			get {
				return (_MapEditorViewModel);
			}
			set {
				_MapEditorViewModel = value;
				OnPropertyChanged(nameof(MapEditorViewModel));
			}
		}



		public GameCreationViewModel(GameCore gameCore) {
			_GameCore = gameCore;
			var currentGame = _GameCore.GetLastGame();
			if (currentGame == null) {
				currentGame = new Game();
			}
			_CurrentGame = currentGame;
			if (_CurrentGame.Scenes.Count == 0) {
				CurrentScene = new Scene("Pas de nom");
				_CurrentGame.Scenes.Add(CurrentScene);
			}

			else
				CurrentScene = _CurrentGame.Scenes[0];

			MapEditorViewModel = new MapEditorViewModel();
		}

		public void SaveGameSettings() {
			var game = CurrentGame;
			var currentScenePath = MapEditorViewModel.BackgroundPath;
			game.Scenes[0].Background = new Illustration(currentScenePath);
			game.Scenes[0].Obstacles = MapEditorViewModel.Obstacles;

			_GameCore.SaveGame(game).GetAwaiter().GetResult();
		}

		public void AddAScene() {
			CurrentScene = new Scene("Scène sans titre");
			CurrentGame.Scenes.Add(CurrentScene);
		}

	}
}
