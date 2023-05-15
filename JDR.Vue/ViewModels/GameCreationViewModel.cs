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
                if (value != null)
                {
                    Scenes = new ObservableCollection<Scene>(_CurrentGame.Scenes);
                    if (Scenes.Count > 0)
                        CurrentScene = Scenes[0];
                }
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
                _MapEditorViewModel.ContentImage = _CurrentScene?.Background?.Content ?? new byte[] { };
                _MapEditorViewModel.Obstacles = _CurrentScene?.Obstacles ?? new ObservableCollection<Obstacle>();
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
            _CurrentGame = new Game();
            _Scenes = new ObservableCollection<Scene>(_CurrentGame.Scenes);
            _MapEditorViewModel = new MapEditorViewModel();
        }

        public void SaveGame()
        {
            AddCurrentSceneToGameScenes();
            CurrentGame.Scenes = Scenes;
            _GameCore.SaveGame(CurrentGame);
        }

        private void AddCurrentSceneToGameScenes()
        {
            if (CurrentScene == null) return;

            var fileinfo = MapEditorViewModel?.FileInfoBackground;
            if (fileinfo != null)
            {
                var illustration = new Illustration
                {
                    Content = File.ReadAllBytes(fileinfo.FullName),
                    Extension = fileinfo.Extension,
                    Name = fileinfo.Name.Substring(0, fileinfo.Name.Length - fileinfo.Extension.Length)                
                };
                CurrentScene.Background = illustration;
                CurrentScene.Obstacles = MapEditorViewModel?.Obstacles ?? new List<Obstacle>();
            }
            SaveCurrentSceneIfNeeded();
        }

        public void AddAScene()
        {
            AddCurrentSceneToGameScenes();
            CurrentScene = new Scene("Scène sans titre");
            CurrentGame.Scenes.Add(CurrentScene);
            Scenes.Add(CurrentScene);
        }

        private void SaveCurrentSceneIfNeeded()
        {
            CurrentScene.Obstacles = _MapEditorViewModel.CurrentScene.Obstacles;
            CurrentScene.ZoomValue = _MapEditorViewModel.CurrentScene.ZoomValue;
            CurrentScene.XMapTranslation = _MapEditorViewModel.CurrentScene.XMapTranslation;
            CurrentScene.YMapTranslation = _MapEditorViewModel.CurrentScene.YMapTranslation;
            CurrentScene.Width = _MapEditorViewModel.CurrentScene.Width;
            CurrentScene.Height = _MapEditorViewModel.CurrentScene.Height;
            _GameCore.SaveScene(CurrentScene);
        }
    }
}
