using JDR.Model;
using JDR.Vue.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace JDR.Vue.Views {
	/// <summary>
	/// Logique d'interaction pour UCMapEditor.xaml
	/// </summary>
	public partial class UCMapEditor : UserControl {

		private IList<Polygon> _AllPolygons;

		private double _ZoomFactor = 1.0;
		private const double ZoomIncrement = 0.1;
		private Point _previousMousePosition;
		private bool _isDragging;
		private Polygon _selectedPolygon;

		public UCMapEditor() {
			InitializeComponent();
			_AllPolygons = new List<Polygon>();
			MouseWheel += MainWindow_MouseWheel;
			KeyDown += MainWindow_KeyDown;

			backgroundImage.MouseLeftButtonDown += BackgroundImage_MouseLeftButtonDown;
			backgroundImage.MouseLeftButtonUp += BackgroundImage_MouseLeftButtonUp;
			backgroundImage.MouseMove += BackgroundImage_MouseMove;

			CurrentPolygon.MouseEnter += CurrentPolygon_MouseEnter;
			CurrentPolygon.MouseLeave += CurrentPolygon_MouseLeave;
			CurrentPolygon.MouseRightButtonDown += CurrentPolygon_MouseRightButtonDown;
		}

		private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			if (CurrentPolygon == null) CurrentPolygon = new Polygon();
			if (e.ClickCount == 2) {
				DoubleClick();
				return;
			}

			Point clickPosition = e.GetPosition(MyCanvas);

			//var pointEllipse = new Ellipse {
			//	Width = 5,
			//	Height = 5,
			//	Fill = Brushes.Black
			//};

			//Canvas.SetLeft(pointEllipse, clickPosition.X - pointEllipse.Width / 2);
			//Canvas.SetTop(pointEllipse, clickPosition.Y - pointEllipse.Height / 2);

			//MyCanvas.Children.Add(pointEllipse);
			CurrentPolygon.Points.Add(clickPosition);
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete) {
				if (_selectedPolygon != null) {
					// Trouvez l'index du polygone sélectionné dans _AllPolygons
					int selectedIndex = _AllPolygons.IndexOf(_selectedPolygon);

					// Supprimez le polygone de la liste des obstacles et de _AllPolygons
					if (selectedIndex >= 0 && selectedIndex < _AllPolygons.Count) {
						var obstacles = ((MapEditorViewModel)DataContext).Obstacles;
						obstacles.RemoveAt(selectedIndex);
						_AllPolygons.RemoveAt(selectedIndex);
					}

					// Supprimez le polygone du canvas
					MyCanvas.Children.Remove(_selectedPolygon);

					// Réinitialisez le polygone sélectionné
					_selectedPolygon = null;
				}
			}
		}

		private void DoubleClick() {
			if (CurrentPolygon.Points.Count > 2) {

				CurrentPolygon.Fill = new SolidColorBrush(new Color {
					R = 0,
					G = 0,
					B = 0,
					A = 125
				});
				CurrentPolygon.Points.Add(CurrentPolygon.Points[0]);
				_AllPolygons.Add(CurrentPolygon);
				UpdateObstacles(CurrentPolygon);

				CurrentPolygon = new Polygon {
					Stroke = Brushes.Black,
					StrokeThickness = 2,
					Fill = Brushes.Transparent,
				};

				CurrentPolygon.MouseEnter += CurrentPolygon_MouseEnter;
				CurrentPolygon.MouseLeave += CurrentPolygon_MouseLeave;
				CurrentPolygon.MouseRightButtonDown += CurrentPolygon_MouseRightButtonDown;

				MyCanvas.Children.Add(CurrentPolygon);
			}
		}

		private void UpdateObstacles(Polygon currentPolygon) {
			var obstacles = ((MapEditorViewModel)DataContext).Obstacles;
			var obstacle = new Obstacle();
			for (int i = 0; i < currentPolygon.Points.Count - 1; i++) {
				Point startPoint = currentPolygon.Points[i];
				Point endPoint = currentPolygon.Points[(i + 1) % currentPolygon.Points.Count];

				var li = new Model.Line(
					new System.Drawing.Point((int)startPoint.X, (int)startPoint.Y),
					new System.Drawing.Point((int)endPoint.X, (int)endPoint.Y)
				);
				obstacle.Lines.Add(li);
			}
			obstacles.Add(obstacle);
		}

		private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e) {
			if (Keyboard.Modifiers == ModifierKeys.Control) {
				if (e.Delta > 0)
					_ZoomFactor += ZoomIncrement;
				else
					_ZoomFactor -= ZoomIncrement;

				_ZoomFactor = Math.Max(_ZoomFactor, 0.1); // Limite le dézoomage
				backgroundImage.LayoutTransform = new ScaleTransform(_ZoomFactor, _ZoomFactor);
			}
		}

		private void BackgroundImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			if (Keyboard.Modifiers == ModifierKeys.Control) {
				_isDragging = true;
				_previousMousePosition = e.GetPosition(MyCanvas);
				backgroundImage.CaptureMouse();
			}
		}

		private void BackgroundImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			_isDragging = false;
			backgroundImage.ReleaseMouseCapture();
		}

		private void BackgroundImage_MouseMove(object sender, MouseEventArgs e) {
			if (_isDragging) {
				Point currentPosition = e.GetPosition(MyCanvas);
				double deltaX = currentPosition.X - _previousMousePosition.X;
				double deltaY = currentPosition.Y - _previousMousePosition.Y;

				double left = Canvas.GetLeft(backgroundImage) + deltaX;
				double top = Canvas.GetTop(backgroundImage) + deltaY;

				Canvas.SetLeft(backgroundImage, left);
				Canvas.SetTop(backgroundImage, top);

				_previousMousePosition = currentPosition;
			}
		}

		#region CurrentPolygonSelectionSuppression
		private void CurrentPolygon_MouseEnter(object sender, MouseEventArgs e) {
			if (sender is Polygon polygon) {
				polygon.Stroke = Brushes.Red;
			}
		}

		private void CurrentPolygon_MouseLeave(object sender, MouseEventArgs e) {
			if (sender is Polygon polygon && polygon != _selectedPolygon) {
				polygon.Stroke = Brushes.Black;
			}
		}

		private void CurrentPolygon_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
			if (_selectedPolygon != null) {
				_selectedPolygon.Stroke = Brushes.Black;
			}

			if (sender is Polygon polygon) {
				if (_selectedPolygon != polygon) {
					_selectedPolygon = polygon;
					_selectedPolygon.Stroke = Brushes.Red;
				}
				else {
					_selectedPolygon = null;
				}

			}
		}
		#endregion

	}
}
