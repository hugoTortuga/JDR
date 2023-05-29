using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using JDR.Model;
using System.IO;

namespace JDR.Vue.ViewModels
{
    public class MapEditorViewModel : ViewModelBase
    {

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
        public Scene CurrentScene { get; set; }

        public MapEditorViewModel()
        {
            _Obstacles = new List<Obstacle>();
            _ContentImage = new byte[0];
        }

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
