using JDR.Core;
using JDR.Infra;
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

        private IMusicStorage _MusicStorage;


        public GameCreationViewModel(GameCore gameCore, IMusicStorage musicStorage)
        {
            _MusicStorage = musicStorage;
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

            var imageFileInfo = MapEditorViewModel?.FileInfoBackground;
            if (imageFileInfo != null)
            {
                var illustration = new Illustration
                {
                    Content = File.ReadAllBytes(imageFileInfo.FullName),
                    Extension = imageFileInfo.Extension,
                    Name = imageFileInfo.Name.Substring(0, imageFileInfo.Name.Length - imageFileInfo.Extension.Length)
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

        public void OpenMusicSelection()
        {
            if (CurrentScene == null) return;

            var windowSelectionMusic = new WinMusicSelection(_MusicStorage, CurrentScene.Musics);
            windowSelectionMusic.ShowDialog();
            var musicsVM = (MusicSelectionViewModel)windowSelectionMusic.DataContext;

            if (musicsVM != null && musicsVM.WasValidated && musicsVM.Musics != null && musicsVM.Musics.Count > 0)
            {
                var validMusics = musicsVM.Musics.Where(m => m.Content != null && m.Path != null).ToList();
                CurrentScene.Musics = validMusics;
                Task.WaitAll(validMusics.Select(m => _MusicStorage.Upload(m.Content, m.Path)).ToArray());
            }
        }

        private void SaveCurrentSceneIfNeeded()
        {
            if (_MapEditorViewModel.CurrentScene == null) return;
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
