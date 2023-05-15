using JDR.Model;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class MusicSelectionViewModel : ViewModelBase
    {

        private ObservableCollection<Music> _Musics;
        public ObservableCollection<Music> Musics
        {
            get
            {
                return (_Musics);
            }
            set
            {
                _Musics = value;
                OnPropertyChanged(nameof(Musics));
            }
        }

        public string MusicNumbersLabel => Musics.Count + " musiques";

        private string? _MusicName;
        public string? MusicName
        {
            get
            {
                return (_MusicName);
            }
            set
            {
                _MusicName = value;
                OnPropertyChanged(nameof(MusicName));
            }
        }


        private string? _SelectedFileLabel;
        public string? SelectedFileLabel
        {
            get
            {
                return (_SelectedFileLabel);
            }
            set
            {
                _SelectedFileLabel = value;
                OnPropertyChanged(nameof(SelectedFileLabel));
            }
        }

        private Action ClosingAction;
        public bool WasValidated = false;

        public MusicSelectionViewModel(IList<Music> musics, Action action)
        {
            ClosingAction = action;
            if (musics != null)
                _Musics = new ObservableCollection<Music>(musics);
            else 
                _Musics = new ObservableCollection<Music>();
        }

        public void DeleteMusic(object parameter)
        {
            var music = parameter as Music;
            if (music != null && Musics != null && Musics.Contains(music))
                Musics.Remove(music);
        }

        public void DeleteMusic()
        {
            
        }

        public void DeleteMusic(object parameter, object sender)
        {
        }

        public void ChooseMusic()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3|WAV files (*.wav)|*.wav|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                MusicName = Path.GetFileNameWithoutExtension(dialog.FileName);
                SelectedFileLabel = dialog.FileName;
            }
        }

        private int GetMusicLength(string fileName)
        {
            using var audioFile = new AudioFileReader(fileName);
            return (int)audioFile.TotalTime.TotalSeconds;
        }

        public void AddMusic()
        {
            if (string.IsNullOrEmpty(SelectedFileLabel)) return;
            var music = new Music
            {
                Name = Path.GetFileNameWithoutExtension(MusicName),
                DurationInSecond = GetMusicLength(SelectedFileLabel),
                Content = File.ReadAllBytes(SelectedFileLabel)
            };
            Musics.Add(music);
            SelectedFileLabel = null;
            MusicName = null;
            OnPropertyChanged(MusicNumbersLabel);
        }

        public void ValidMusicsAndClose()
        {
            WasValidated = true;
            ClosingAction.Invoke();
        }

    }
}
