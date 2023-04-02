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

        public TableTopViewModel()
        {
            Players = new ObservableCollection<Player> { new Player("Bengala", 
                new Skills { 
                    Agility = 50,
                    Force = 60,
                    Intelligence = 20,
                    Courage = 60,
                    Discretion = 15,
                    Persuasion = 40,
                    Observation = 40
                }
            )};

		}

	}
}
