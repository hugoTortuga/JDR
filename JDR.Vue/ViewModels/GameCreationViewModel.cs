﻿using JDR.Core;
using JDR.Model;
using JDR.Service;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class GameCreationViewModel : ViewModelBase
    {


        private Game _CurrentGame;
        public Game CurrentGame
        {
            get
            {
                return (_CurrentGame);
            }
            set
            {
                _CurrentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }

        private Scene _CurrentScene;
        public Scene CurrentScene
        {
            get
            {
                return (_CurrentScene);
            }
            set
            {
                _CurrentScene = value;
                OnPropertyChanged(nameof(CurrentScene));
            }
        }

        private ObservableCollection<Scene> _Scenes;
        public ObservableCollection<Scene> Scenes
        {
            get { return _Scenes; }
            set
            {
                _Scenes = value;
                OnPropertyChanged(nameof(Scenes));
            }
        }

        private GameCore _GameCore;

        private MapEditorViewModel _MapEditorViewModel;
        public MapEditorViewModel MapEditorViewModel
        {
            get
            {
                return (_MapEditorViewModel);
            }
            set
            {
                _MapEditorViewModel = value;
                OnPropertyChanged(nameof(MapEditorViewModel));
            }
        }



        public GameCreationViewModel(GameCore gameCore)
        {
            _GameCore = gameCore;
            var currentGame = _GameCore.GetLastGame();
            if (currentGame == null)
            {
                currentGame = new Game();
            }
            _CurrentGame = currentGame;

            Scenes = new ObservableCollection<Scene>(_CurrentGame.Scenes);
            MapEditorViewModel = new MapEditorViewModel();
        }

        public void SaveGame()
        {
            if (CurrentScene != null)
            {
                var fileinfo = MapEditorViewModel?.FileInfoBackground;
                if (fileinfo != null)
                {
                    var illustration = new Illustration
                    {
                        Content = File.ReadAllBytes(fileinfo.FullName),
                        Extension = fileinfo.Extension,
                        Name = fileinfo.Name
                    };
                    CurrentScene.Background = illustration;
                    CurrentScene.Obstacles = MapEditorViewModel?.Obstacles ?? new List<Obstacle>();
                }
            }
            CurrentGame.Scenes = Scenes;
            _GameCore.SaveGame(CurrentGame);

        }

        public void AddAScene()
        {
            if (CurrentScene != null)
            {
                var fileinfo = MapEditorViewModel?.FileInfoBackground;
                if (fileinfo != null)
                {
                    var illustration = new Illustration
                    {
                        Content = File.ReadAllBytes(fileinfo.FullName),
                        Extension = fileinfo.Extension,
                        Name = fileinfo.Name
                    };
                    CurrentScene.Background = illustration;
                    CurrentScene.Obstacles = MapEditorViewModel?.Obstacles ?? new List<Obstacle>();
                }
                SaveCurrentSceneIfNeeded();
            }

            CurrentScene = new Scene("Scène sans titre");
            CurrentGame.Scenes.Add(CurrentScene);
            Scenes.Add(CurrentScene);

        }

        private void SaveCurrentSceneIfNeeded()
        {
            _GameCore.SaveScene(CurrentScene);
        }
    }
}
