using JDR.Model;
using JDR.Vue.ViewModels;
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
		private int _SelectionFormSize = 16;
		private int _FOV = 400;
		private double _BackgroundWidth;
		private double _BackgroundHeight;
		private TranslateTransform _TranslateTransformBackgroundMap;
		private IList<Geometry> _Obstacles;

		public UCGame(MainWindow window) {
			DataContext = new TableTopViewModel();

			_Obstacles = new List<Geometry> { 
				new RectangleGeometry(new Rect {
					Width = 60,
					Height = 60,
					Location = new Point(120, 250)
				})
			};
			InitializeComponent();
			SetMapProperties();
			DrawFieldOfVision();
		}

		private void SetMapProperties() {
			_BackgroundWidth = BackgroundImageBrush.ImageSource.Width;
			_BackgroundHeight = BackgroundImageBrush.ImageSource.Height;
			_TranslateTransformBackgroundMap = new TranslateTransform();
			BackgroundImageBrush.Transform = _TranslateTransformBackgroundMap;
			GameCanvas.Background = BackgroundImageBrush;
		}

		private void OpenCharacterSheet(object sender, RoutedEventArgs e) {
			var menuItem = sender as MenuItem;
			var selectedPlayer = menuItem.DataContext as Player;
			if (selectedPlayer != null)
				new WinCharacterSheet(selectedPlayer).Show();
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
					Width = Player1.ActualWidth + _SelectionFormSize,
					Height = Player1.ActualHeight + _SelectionFormSize,
					Stroke = Brushes.Black,
					StrokeThickness = 2,
					Opacity = 0.8
				};

				Canvas.SetLeft(_SelectionForm, Canvas.GetLeft(Player1) - _SelectionFormSize / 2);
				Canvas.SetTop(_SelectionForm, Canvas.GetTop(Player1) - _SelectionFormSize / 2);

				GameCanvas.Children.Add(_SelectionForm);
			}
		}

		private void GameCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
		}

		private void ChangeMap(object sender, RoutedEventArgs e) {
			try {
				var imagePath = GetImageURL();
				var image = new ImageBrush(new BitmapImage(new Uri(imagePath)));
				image.Stretch = Stretch.UniformToFill;
				BackgroundImageBrush = image;
				SetMapProperties();
			}
			catch {

			}
		}

		private void ImageMouseMove(object sender, MouseEventArgs e) {
			if (Player1.IsMouseCaptured) {
				DrawFieldOfVision();
				MovePlayerToNewPosition(e.GetPosition(Player1.Parent as UIElement));
			}
		}

		private void MovePlayerToNewPosition(Point newPosition) {
			Canvas.SetLeft(Player1, newPosition.X - (Player1.Width / 2));
			Canvas.SetTop(Player1, newPosition.Y - (Player1.Height / 2));
			if (_IsPlayerSelected) {
				Canvas.SetLeft(_SelectionForm, newPosition.X - (Player1.Width / 2) - (_SelectionFormSize / 2));
				Canvas.SetTop(_SelectionForm, newPosition.Y - (Player1.Height / 2) - (_SelectionFormSize / 2));
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

		private void UpButton_Click(object sender, RoutedEventArgs e) {
			_TranslateTransformBackgroundMap.Y -= 10;
		}

		private void DownButton_Click(object sender, RoutedEventArgs e) {
			_TranslateTransformBackgroundMap.Y += 10;
		}

		private void LeftButton_Click(object sender, RoutedEventArgs e) {
			_TranslateTransformBackgroundMap.X -= 10;
		}

		private void RightButton_Click(object sender, RoutedEventArgs e) {
			_TranslateTransformBackgroundMap.X += 10;
		}

		private void ChangeToken(object sender, RoutedEventArgs e) {
			try {
				Player1.Source = new BitmapImage(new Uri(GetImageURL()));
			}
			catch {

			}
		}

		private string GetImageURL() {
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == true) {
				return dialog.FileName;
			}
			throw new ApplicationException("Aucune image sélectionnée");
		}


		private Path CurrentFOVPath;

		public void DrawFieldOfVision() {
			var playerPosition = GetPlayerCenterPostition();
			GameCanvas.Children.Remove(CurrentFOVPath);
			var pathGeometry = new PathGeometry();
			var pathFigure = new PathFigure { StartPoint = playerPosition, IsClosed = true, IsFilled = true };
			pathGeometry.Figures.Add(pathFigure);

			int numberOfRays = 720;
			double angleStep = 360.0 / numberOfRays;

			for (int i = 0; i <= numberOfRays; i++) {
				var ray = ComputeRay(playerPosition, angleStep, _FOV, i);
				pathFigure.Segments.Add(ray);
			}

			var canvasRect = new Rect(0, 0, 2000, 1400);
			var canvasRectangleGeometry = new RectangleGeometry(canvasRect);

			var combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, canvasRectangleGeometry, pathGeometry);

			CurrentFOVPath = new Path {
				Fill = new SolidColorBrush(Colors.Black) { Opacity = 1 },
				Data = combinedGeometry
			};

			GameCanvas.Children.Add(CurrentFOVPath);
		}

		private LineSegment ComputeRay(Point playerPosition, double angleStep, double radius, int i) {
			double angle = i * angleStep;
			double x = playerPosition.X + radius * Math.Cos(Math.PI * angle / 180.0);
			double y = playerPosition.Y + radius * Math.Sin(Math.PI * angle / 180.0);

			var endPoint = new Point(x, y);
			var ray = new LineSegment(endPoint, true);

			var playerToEndpoint = new Line(playerPosition, endPoint);
			Point? nearestIntersection = null;
			double nearestIntersectionDistance = double.MaxValue;

			foreach (var obstacle in _Obstacles) {
				var intersectionPoints = playerToEndpoint.Intersects(obstacle);

				foreach (var intersectionPoint in intersectionPoints) {
					double distance = (playerPosition - intersectionPoint).Length;
					if (distance < nearestIntersectionDistance) {
						nearestIntersection = intersectionPoint;
						nearestIntersectionDistance = distance;
					}
				}
			}

			if (nearestIntersection.HasValue) {
				ray.Point = nearestIntersection.Value;
			}
			return ray;
		}

		private Point GetPlayerCenterPostition() {
			return new Point(Canvas.GetLeft(Player1) + Player1.Width / 2, Canvas.GetTop(Player1) + Player1.Height / 2);
		}

	}
}
