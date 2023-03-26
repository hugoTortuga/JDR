﻿using JDR.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class GameViewModel : ViewModelBase {

        public Game Game { get; set; }
        private readonly ObservableCollection<Scene> _scenes;

		public string Name {
			get {
				return (Game.Name);
			}
			set {
				Game.Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public GameViewModel(Game game)
        {
            Game = game;
			Name = game.Name;
			_scenes = new ObservableCollection<Scene>(game.Scenes);
		}

		public ObservableCollection<Scene> Scenes {
			get { return _scenes; }
			set {
				if (Game.Scenes != value) {
					Game.Scenes = value;
					OnPropertyChanged(nameof(Scenes));
				}
			}
		}


	}
}