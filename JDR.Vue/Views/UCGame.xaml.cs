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
using static System.Net.Mime.MediaTypeNames;

namespace JDR.Vue.Views {
	/// <summary>
	/// Logique d'interaction pour UCGame.xaml
	/// </summary>
	public partial class UCGame : UserControl {

		private bool _IsPlayerSelected;
		private Ellipse _SelectionForm;
		private int _FormSize = 16;
		private double _BackgroundWidth;
		private double _BackgroundHeight;

		public UCGame(MainWindow window) {
			InitializeComponent();
			_BackgroundWidth = BackgroundImageBrush.ImageSource.Width;
			_BackgroundHeight = BackgroundImageBrush.ImageSource.Height;
		}

		private void OpenCharacterSheet(object sender, RoutedEventArgs e) {
			new WinCharacterSheet().ShowDialog();
		}

		private void PlayerClicked(object sender, MouseButtonEventArgs e) {
			Player1.CaptureMouse();
			if (_IsPlayerSelected) {
				_IsPlayerSelected = false;
				if (_SelectionForm != null) {
					GameCanvas.Children.Remove(_SelectionForm);
					_SelectionForm = null;
				}
			}
			else {
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
		}

		private void ChangeMap(object sender, RoutedEventArgs e) {
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == true) {
				var image = new ImageBrush(new BitmapImage(new Uri(dialog.FileName)));
				image.Stretch = Stretch.UniformToFill;
				BackgroundImageBrush = image;
				GameCanvas.Background = BackgroundImageBrush;
			}
		}

		private void ImageMouseMove(object sender, MouseEventArgs e) {
			if (Player1.IsMouseCaptured) 
				MovePlayerToNewPosition(e.GetPosition(Player1.Parent as UIElement));
		}

		private void MovePlayerToNewPosition(Point newPosition) {
			Canvas.SetLeft(Player1, newPosition.X - (Player1.Width / 2));
			Canvas.SetTop(Player1, newPosition.Y - (Player1.Height / 2));
			if (_IsPlayerSelected) {
				Canvas.SetLeft(_SelectionForm, newPosition.X - (Player1.Width / 2) - (_FormSize / 2));
				Canvas.SetTop(_SelectionForm, newPosition.Y - (Player1.Height / 2) - (_FormSize / 2));
			}
		}

		protected override void OnMouseUp(MouseButtonEventArgs e) {
			Player1.ReleaseMouseCapture();
			base.OnMouseUp(e);
		}

		private void ResizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (GameCanvas?.Background == null) return;
			double newWidth = e.NewValue;
			double aspectRatio = _BackgroundHeight / _BackgroundWidth;

			double newHeight = newWidth * aspectRatio;

			BackgroundImageBrush.Viewport = new Rect(0, 0, newWidth, newHeight);
			BackgroundImageBrush.ViewportUnits = BrushMappingMode.Absolute;
			BackgroundImageBrush.Stretch = Stretch.UniformToFill;
		}
	}
}
