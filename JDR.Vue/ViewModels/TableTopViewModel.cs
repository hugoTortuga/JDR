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

		private GameCore _GameCore;
		private IMusicPlayer _MusicPlayer;

        public TableTopViewModel(GameCore gameCore, IMusicPlayer musicPlayer)
        {
			CurrentVolume = 20;
			_MusicPlayer = musicPlayer;
			_GameCore = gameCore;
            Players = new ObservableCollection<Character> {
				CreateAilurus(),
				CreateBengala(),
				CreateBiscuit(),
				CreateLahir()
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
			var winPlayerCreation = new WinCharacterCreation(_GameCore);
			winPlayerCreation.ShowDialog();
			var playerVM = (CharacterCreationViewModel)winPlayerCreation.DataContext;

            if (playerVM.WasValidated && playerVM.Character != null)
                _GameCore.AddCharacterToGame(playerVM.Character);
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

        public void SceneSelected()
		{
			var test = CurrentScene.Background?.Name + " " + CurrentScene.Background?.Extension;
        }

		private string baseCharacterPath = "C:\\Users\\Hugo\\Desktop\\jdr\\ArthosV2\\joueurs\\";

        private Character CreateBengala() {
			return new Character("Bengala",
						Race.Dwarf,
						new Skills {
							Force = 60,
							Agility = 50,
							Intelligence = 20,
							Courage = 60,
							Discretion = 20,
							Persuasion = 40,
							Observation = 40
						}
					) {
						Illustration = new Illustration() {
							Content = File.ReadAllBytes(baseCharacterPath + "bengala.png")
						},
						HP = 14,
						HPMax = 14,
						Mana = 6,
						ManaMax = 6,
						Level = 1,
						Spells = new List<Spell> {
								new Spell {
									Category = MagicCategory.Darkness,
									Level = 1
								},
								new Spell {
									Category = MagicCategory.Heal,
									Level = 1
								}
							},
						Inventory = new Inventory {
							Objects = new List<InventoryItem> {
								new InventoryItem("Masse"),
								new InventoryItem("Bouclier")
							}
						}
			};
		}

		private Character CreateBiscuit() {
			return new Character("Biscuit",
						Race.Human,
						new Skills {
							Agility = 60,
							Force = 50,
							Intelligence = 20,
							Courage = 20,
							Discretion = 60,
							Persuasion = 40,
							Observation = 40
						}
					) {
				Illustration = new Illustration() { 
					Content = File.ReadAllBytes(baseCharacterPath + "biscuit2.png")
				},
				HP = 12,
				HPMax = 12,
				Mana = 8,
				ManaMax = 8,
				Level = 1,
				Spells = new List<Spell> {
								new Spell {
									Category = MagicCategory.Darkness,
									Level = 1
								},
								new Spell {
									Category = MagicCategory.Psy,
									Level = 1
								}
							},
				Inventory = new Inventory {
					Objects = new List<InventoryItem> {
								new InventoryItem("4 dagues")
							}
				}
			};
		}

		private Character CreateLahir() {
			return new Character("Lahir",
						Race.Elve,
						new Skills {
							Agility = 50,
							Force = 20,
							Intelligence = 60,
							Courage = 40,
							Discretion = 50,
							Persuasion = 30,
							Observation = 40
						}
					) {
				Illustration = new Illustration() {
					Content = File.ReadAllBytes(baseCharacterPath + "lahir.png")
				},
				HP = 13,
				HPMax = 13,
				Mana = 7,
				ManaMax = 7,
				Level = 1,
				Spells = new List<Spell> {
								new Spell {
									Category = MagicCategory.Animal,
									Level = 1
								},
								new Spell {
									Category = MagicCategory.Enchantement,
									Level = 1
								}
							},
				Inventory = new Inventory {
					Objects = new List<InventoryItem> {
								new InventoryItem("Arc court"),
								new InventoryItem("Dague")
							}
				}
			};
		}

		private Character CreateAilurus() {
			return new Character("Ailurus",
						Race.DwarfElve,
						new Skills {
							Agility = 50,
							Force = 30,
							Intelligence = 60,
							Courage = 30,
							Discretion = 50,
							Persuasion = 30,
							Observation = 50
						}
					) {
				Illustration = new Illustration() {
					Content = File.ReadAllBytes(baseCharacterPath + "ailurus.png")
				},
				HP = 11,
				HPMax = 11,
				Mana = 9,
				ManaMax = 9,
				Level = 1,
				Spells = new List<Spell> {
								new Spell {
									Category = MagicCategory.Nature,
									Level = 1
								},
								new Spell {
									Category = MagicCategory.Animal,
									Level = 1
								}
							},
				Inventory = new Inventory {
					Objects = new List<InventoryItem> {
								new InventoryItem("Bâton")
							}
				}
			};
		}

        private Character CreateJulio()
        {
            var sdfdf = new FileInfo(baseCharacterPath + "julio.png");
            return new Character("Julio",
						Race.Human,
						new Skills
						{
							Agility = 35,
							Force = 40,
							Intelligence = 50,
							Courage = 40,
							Discretion = 25,
							Persuasion = 50,
							Observation = 20
						}
					)
			{
				
                Illustration = new Illustration
				{
					Content = File.ReadAllBytes(sdfdf.FullName),
					Name = sdfdf.FullName,
                },
                HP = 14,
                HPMax = 14,
                Mana = 10,
                ManaMax = 10,
                Level = 1,
                Spells = new List<Spell> {
                                new Spell {
                                    Category = MagicCategory.Heal,
                                    Level = 1
                                },
                                new Spell {
                                    Category = MagicCategory.Nature,
                                    Level = 1
                                }
                            },
                Inventory = new Inventory
                {
                    Objects = new List<InventoryItem> {
                                new InventoryItem("Epée courte"),
                                new InventoryItem("Bouclier"),
                                new InventoryItem("Arc court (8 flèches)"),
								new InventoryItem("Calice"),
								new InventoryItem("Fiole")
                            }
                }
            };
        }
    }
}
