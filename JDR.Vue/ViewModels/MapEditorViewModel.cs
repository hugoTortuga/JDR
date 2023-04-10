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

namespace JDR.Vue.ViewModels {
	public class MapEditorViewModel : ViewModelBase {

		private string? _BackgroundPath;
		public string? BackgroundPath {
			get {
				return (_BackgroundPath);
			}
			set {
				_BackgroundPath = value;
				OnPropertyChanged(nameof(BackgroundPath));
			}
		}

		private IList<Obstacle> _Obstacles;
		public IList<Obstacle> Obstacles {
			get {
				return (_Obstacles);
			}
			set {
				_Obstacles = value;
				OnPropertyChanged(nameof(Obstacles));
			}
		}

		public MapEditorViewModel()
        {
			_Obstacles = new List<Obstacle>();
		}

		public void ChangeBackground() {
			BackgroundPath = GetImageURL();
		}

		private string GetImageURL() {
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == true) {
				return dialog.FileName;
			}
			throw new ApplicationException("Aucune image sélectionnée");
		}

	}
}
