using JDR.Core;
using JDR.Infra;
using JDR.Model;
using JDR.Service;
using JDR.Vue.Views;
using Microsoft.Win32;
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
                ContentImage = _CurrentScene?.Background?.Content ?? new byte[] { };
                Obstacles = _CurrentScene?.Obstacles ?? new ObservableCollection<Obstacle>();
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


        private IMusicStorage _MusicStorage;


        public GameCreationViewModel(GameCore gameCore, IMusicStorage musicStorage)
        {
            _MusicStorage = musicStorage;
            _GameCore = gameCore;
            _CurrentGame = new Game();
            _Scenes = new ObservableCollection<Scene>(_CurrentGame.Scenes);
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

            var imageFileInfo = FileInfoBackground;
            if (imageFileInfo != null)
            {
                var illustration = new Illustration
                (
                    imageFileInfo.Extension,
                    imageFileInfo.Name[..^imageFileInfo.Extension.Length]
                );
                CurrentScene.Background = illustration;
                CurrentScene.Obstacles = Obstacles ?? new List<Obstacle>();
            }
            SaveCurrentSceneIfNeeded();
        }

        public void AddAScene()
        {
            _Obstacles = new List<Obstacle>();
            _ContentImage = new byte[0];
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
            if (CurrentScene == null) return;
            _GameCore.SaveScene(CurrentScene);
        }



        private IList<Obstacle> _Obstacles;
        public IList<Obstacle> Obstacles
        {
            get
            {
                return (_Obstacles);
            }
            set
            {
                _Obstacles = value;
                OnPropertyChanged(nameof(Obstacles));
            }
        }

        private byte[] _ContentImage;
        public byte[] ContentImage
        {
            get { return _ContentImage; }
            set
            {
                _ContentImage = value;
                OnPropertyChanged(nameof(ContentImage));
            }
        }

        public FileInfo? FileInfoBackground { get; set; }


        public void ChangeBackground()
        {
            SetImageProperties();
        }

        private void SetImageProperties()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                FileInfoBackground = new FileInfo(dialog.FileName);
                ContentImage = File.ReadAllBytes(dialog.FileName);
            }
        }

        public void SetApparitionJoueurs(System.Drawing.Point point)
        {
            CurrentScene.PlayerSpawnPoint = point;
        }
    }
}
