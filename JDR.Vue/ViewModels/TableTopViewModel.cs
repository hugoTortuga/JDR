using JDR.Core;
using JDR.Infra;
using JDR.Model;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class TableTopViewModel : ViewModelBase {


        private ObservableCollection<Character> _Players;
        public ObservableCollection<Character> Players {
            get {
                return (_Players);
            }
            set {
                _Players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

		private ObservableCollection<Scene> _Scenes;
		public ObservableCollection<Scene> Scenes
		{
			get { 
				return _Scenes;
			}
			set { 
				_Scenes = value; 
				OnPropertyChanged(nameof(Scenes));
			}
		}

		private Scene _CurrentScene;
		public Scene CurrentScene
		{
			get { return _CurrentScene; }
			set
			{
				_CurrentScene = value;
				OnPropertyChanged(nameof(CurrentScene));
			}
		}

		private Game _CurrentGame;
        public Game CurrentGame { 
			get { return  (_CurrentGame); }
			set
			{
				_CurrentGame = value;
				if (value != null)
				{
					Scenes = new ObservableCollection<Scene>(_CurrentGame.Scenes);
				}
				OnPropertyChanged(nameof(CurrentGame));
			}
		}

		private Music _SelectedMusic;
		public Music SelectedMusic
		{
			get
			{
				return (_SelectedMusic);
			}
			set
			{
				_SelectedMusic = value;
				OnPropertyChanged(nameof(SelectedMusic));
			}
		}

		private bool _IsMusicPlaying;
		public bool IsMusicPlaying
		{
			get
			{
				return (_IsMusicPlaying);
			}
			set
			{
				_IsMusicPlaying = value;
				OnPropertyChanged(nameof(IsMusicPlaying));
			}
		}


		private int _CurrentVolume;
		public int CurrentVolume
		{
			get
			{
				return (_CurrentVolume);
			}
			set
			{
				_CurrentVolume = value;
				OnPropertyChanged(nameof(CurrentVolume));
				_MusicPlayer?.SetVolume((float)CurrentVolume / 100);

            }
		}

		private int _NumberOfDice;
		public int NumberOfDice
		{
			get
			{
				return (_NumberOfDice);
			}
			set
			{
				_NumberOfDice = value;
				OnPropertyChanged(nameof(NumberOfDice));
			}
		}

		private ObservableCollection<Dice> _Dices;
		public ObservableCollection<Dice> Dices
		{
			get
			{
				return (_Dices);
			}
			set
			{
				_Dices = value;
				OnPropertyChanged(nameof(Dice));
			}
		}

		private Dice _SelectedDice;
		public Dice SelectedDice
		{
			get
			{
				return (_SelectedDice);
			}
			set
			{
				_SelectedDice = value;
				OnPropertyChanged(nameof(SelectedDice));
			}
		}

		private string _ResultDice;
		public string ResultDice
		{
			get
			{
				return (_ResultDice);
			}
			set
			{
				_ResultDice = value;
				OnPropertyChanged(nameof(ResultDice));
			}
		}

		public GameCore GameCore { get; set; }
		private IMusicPlayer _MusicPlayer;

        public TableTopViewModel(GameCore gameCore, IMusicPlayer musicPlayer)
        {
			NumberOfDice = 1;
			Dices = new ObservableCollection<Dice>
			{
				new Dice(100),
				new Dice(20),
                new Dice(10),
                new Dice(8),
                new Dice(6),
                new Dice(4),
                new Dice(3),
                new Dice(2)
            };
			SelectedDice = Dices[0];
            CurrentVolume = 20;
			_MusicPlayer = musicPlayer;
			GameCore = gameCore;
            var characters = gameCore.GetCharacters();
			var characterTest = gameCore.GetCharacterTest();
            Players = new ObservableCollection<Character> {
                characterTest
            };
        }

		public void MusicSelection()
		{
            if (SelectedMusic == null) return;
            _MusicPlayer.Stop();
			IsMusicPlaying = false;
            _MusicPlayer.SetMusic(SelectedMusic);
            _MusicPlayer.SetVolume((float)CurrentVolume / 100);
        }

		public void OpenPlayerCreation()
		{
			var winPlayerCreation = new WinCharacterCreation(GameCore);
			winPlayerCreation.ShowDialog();
			var playerVM = (CharacterCreationViewModel)winPlayerCreation.DataContext;

            if (playerVM.WasValidated && playerVM.Character != null)
                GameCore.AddCharacterToGame(playerVM.Character);
        }

		public void RollTheDice()
		{
			if (SelectedDice == null) return;
			ResultDice = "Résultat : " + new Dices($"{NumberOfDice}d{SelectedDice.NumberFaces}").RollTheDice();
		}

        public void PlayOrPauseMusic()
		{
			if (SelectedMusic == null) return;

            if (_IsMusicPlaying)
			{
                _MusicPlayer.Pause();
				IsMusicPlaying = false;
            }
			else
			{
                _MusicPlayer.Play();
                IsMusicPlaying = true;
            }
		}

		public void StopMusic()
		{
            if (SelectedMusic == null) return;
			_MusicPlayer.Stop();
        }

        public void SceneSelected()
		{
			var test = CurrentScene.Background?.Name + " " + CurrentScene.Background?.Extension;
        }

    }
}
