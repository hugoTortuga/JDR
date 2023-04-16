using JDR.Core;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class TableTopViewModel : ViewModelBase {


        private ObservableCollection<Player> _Players;
        public ObservableCollection<Player> Players {
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

        public Game CurrentGame { 
			get; 
			set;
		}

        public TableTopViewModel(GameCore gameCore)
        {
            Players = new ObservableCollection<Player> {
				CreateBengala(),
				CreateBiscuit(),
				CreateAilurus()
			};
			Scenes = new ObservableCollection<Scene>(gameCore.GetAvailableScenes());
			CurrentScene = Scenes[0];
        }

		public void SceneSelected()
		{
			var test = CurrentScene.Background?.Name + " " + CurrentScene.Background?.Extension;
        }

        private Player CreateBengala() {
			return new Player("Bengala",
						Race.Dwarf,
						new Skills {
							Agility = 50,
							Force = 60,
							Intelligence = 20,
							Courage = 60,
							Discretion = 20,
							Persuasion = 40,
							Observation = 40
						}
					) {
						//Illustration = new Illustration("joueurs\\bengala.png"),
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

		private Player CreateBiscuit() {
			return new Player("Biscuit",
						Race.Dwarf,
						new Skills {
							Agility = 50,
							Force = 60,
							Intelligence = 20,
							Courage = 20,
							Discretion = 60,
							Persuasion = 40,
							Observation = 40
						}
					) {
				//Illustration = new Illustration("joueurs\\biscuit.png"),
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

		private Player CreateAilurus() {
			return new Player("Ailurus",
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
				//Illustration = new Illustration("joueurs\\ailurus.png"),
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
	}
}
