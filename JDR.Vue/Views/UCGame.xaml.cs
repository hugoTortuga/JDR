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

		private bool _isPlayerSelected;
		private Rectangle _selectionRectangle;

		public UCGame(MainWindow window) {
			InitializeComponent();
		}

		private void OpenCharacterSheet(object sender, RoutedEventArgs e) {
			new WinCharacterSheet().ShowDialog();
		}

		private void PlayerClicked(object sender, EventArgs e) {
			if (_isPlayerSelected) {
				_isPlayerSelected = false;
				if (_selectionRectangle != null) {
					GameCanvas.Children.Remove(_selectionRectangle);
					_selectionRectangle = null;
				}
			}
			else {
				// Sélectionner le joueur et dessiner le carré de sélection
				_isPlayerSelected = true;
				_selectionRectangle = new Rectangle {
					Width = Player1.ActualWidth + 4,
					Height = Player1.ActualHeight + 4,
					Stroke = Brushes.Red,
					StrokeThickness = 2
				};

				Canvas.SetLeft(_selectionRectangle, Canvas.GetLeft(Player1) - 2);
				Canvas.SetTop(_selectionRectangle, Canvas.GetTop(Player1) - 2);

				GameCanvas.Children.Add(_selectionRectangle);
			}
		}

		private void GameCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
			if (_isPlayerSelected) {
				var newMousePosition = e.GetPosition(GameCanvas);

				Canvas.SetLeft(Player1, newMousePosition.X - (Player1.Width / 2));
				Canvas.SetTop(Player1, newMousePosition.Y - (Player1.Height / 2));

				Canvas.SetLeft(_selectionRectangle, newMousePosition.X - (Player1.Width / 2));
				Canvas.SetTop(_selectionRectangle, newMousePosition.Y - (Player1.Height / 2));
			}
		}

		private void GameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			
		}

		private void GameCanvas_MouseMove(object sender, MouseEventArgs e) {
			if (_isPlayerSelected && GameCanvas.IsMouseCaptured) {
				
			}
		}
	}
}
