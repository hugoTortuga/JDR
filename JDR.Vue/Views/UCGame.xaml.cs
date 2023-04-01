using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JDR.Vue.Views {
	/// <summary>
	/// Logique d'interaction pour UCGame.xaml
	/// </summary>
	public partial class UCGame : UserControl {

		private bool _IsPlayerSelected;
		private Ellipse _SelectionForm;
		private int _FormSize = 16;

		public UCGame(MainWindow window) {
			InitializeComponent();
		}

		private void OpenCharacterSheet(object sender, RoutedEventArgs e) {
			new WinCharacterSheet().ShowDialog();
		}

		private void PlayerClicked(object sender, EventArgs e) {
			if (_IsPlayerSelected) {
				_IsPlayerSelected = false;
				if (_SelectionForm != null) {
					GameCanvas.Children.Remove(_SelectionForm);
					_SelectionForm = null;
				}
			}
			else {
				// Sélectionner le joueur et dessiner le carré de sélection
				
				_IsPlayerSelected = true;
				_SelectionForm = new Ellipse {
					Width = Player1.ActualWidth + _FormSize,
					Height = Player1.ActualHeight + _FormSize,
					Stroke = Brushes.Black,
					StrokeThickness = 2,
					Opacity = 0.8
				};

				Canvas.SetLeft(_SelectionForm, Canvas.GetLeft(Player1) - _FormSize / 2);
				Canvas.SetTop(_SelectionForm, Canvas.GetTop(Player1) - _FormSize / 2);

				GameCanvas.Children.Add(_SelectionForm);
			}
		}

		private void GameCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
			if (_IsPlayerSelected) {
				var newMousePosition = e.GetPosition(GameCanvas);

				Canvas.SetLeft(Player1, newMousePosition.X - (Player1.Width / 2));
				Canvas.SetTop(Player1, newMousePosition.Y - (Player1.Height / 2));

				Canvas.SetLeft(_SelectionForm, newMousePosition.X - (Player1.Width / 2) - (_FormSize / 2));
				Canvas.SetTop(_SelectionForm, newMousePosition.Y - (Player1.Height / 2) - (_FormSize / 2));
			}
		}


		private void ChangeMap(object sender, RoutedEventArgs e) {
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == true) {
				var image = new ImageBrush(new BitmapImage(new Uri(dialog.FileName)));
				image.Stretch = Stretch.UniformToFill;
				GameCanvas.Background = image;


			}

		}
	}
}
