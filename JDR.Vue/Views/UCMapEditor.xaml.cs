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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JDR.Vue.Views {
	/// <summary>
	/// Logique d'interaction pour UCMapEditor.xaml
	/// </summary>
	public partial class UCMapEditor : UserControl {

		private IList<Polygon> _AllPolygons;
		private IList<Ellipse> _AllEllipses;

		private double _ZoomFactor = 1.0;
		private const double ZoomIncrement = 0.1;
		private Point _previousMousePosition;
		private bool _isDragging;
		private Polygon _selectedPolygon;

		public Scene GetScene() {
			var scene = new Scene("gameCreationHack ça code sale") {
				Obstacles = new List<Obstacle>(_AllPolygons.Select(p => new Obstacle {
					Points = p.Points.Select(e => new System.Drawing.Point((int)e.X, (int)e.Y)).ToList()
				})),
				ZoomValue = _ZoomFactor,
				XMapTranslation = CurrentXTranslation,
				YMapTranslation = CurrentYTranslation
			};
			return scene;
		}

		public UCMapEditor() {
			InitializeComponent();
			_AllPolygons = new List<Polygon>();
			_AllEllipses = new List<Ellipse>();


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

			var pointEllipse = new Ellipse {
				Width = 5,
				Height = 5,
				Fill = Brushes.Black
			};

			Canvas.SetLeft(pointEllipse, clickPosition.X - pointEllipse.Width / 2);
			Canvas.SetTop(pointEllipse, clickPosition.Y - pointEllipse.Height / 2);

			MyCanvas.Children.Add(pointEllipse);
			_AllEllipses.Add(pointEllipse); // Ajoutez cette ligne
			CurrentPolygon.Points.Add(clickPosition);
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete) {
				if (_selectedPolygon != null) {

					int selectedIndex = _AllPolygons.IndexOf(_selectedPolygon);
					if (selectedIndex >= 0 && selectedIndex < _AllPolygons.Count) {
						var obstacles = ((MapEditorViewModel)DataContext).Obstacles;
						obstacles.RemoveAt(selectedIndex);

						if (_AllPolygons.Count > 0) {
							int pointCount = _AllPolygons[selectedIndex].Points.Count - 1;
							RemoveEllipsesAndTextBlocks(selectedIndex * pointCount, pointCount);
						}

						_AllPolygons.RemoveAt(selectedIndex);
					}
					// Supprimez le polygone du canvas
					MyCanvas.Children.Remove(_selectedPolygon);

					// Réinitialisez le polygone sélectionné
					_selectedPolygon = null;
				}
			}
		}

		private void RemoveEllipsesAndTextBlocks(int startIndex, int count) {
			for (int i = startIndex + count - 1; i >= startIndex; i--) {
				// Supprimez les Ellipse du canvas et de la liste _AllEllipses
				MyCanvas.Children.Remove(_AllEllipses[i]);
				_AllEllipses.RemoveAt(i);
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
			foreach (var point in currentPolygon.Points) {
				obstacle.Points.Add(new System.Drawing.Point((int)point.X, (int)point.Y));
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

				var scaleTransform = new ScaleTransform(_ZoomFactor, _ZoomFactor);
				backgroundImage.LayoutTransform = scaleTransform;
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
			if (sender is Polygon polygon && _selectedPolygon != polygon) {
				polygon.Stroke = Brushes.Gray;
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
					polygon.Stroke = Brushes.Gray;
				}

			}
		}
		#endregion

		#region MoveBackground

		private int CurrentXTranslation = 0;
		private int CurrentYTranslation = 0;
		private int taillePas = 10;

		private void MoveImageAndPolygons(int deltaX, int deltaY) {
			CurrentXTranslation += deltaX;
			CurrentYTranslation += deltaY;
			double left = Canvas.GetLeft(backgroundImage);
			if (double.IsNaN(left)) left = 0;
			double top = Canvas.GetTop(backgroundImage);
			if (double.IsNaN(top)) top = 0;

			Canvas.SetLeft(backgroundImage, left + deltaX);
			Canvas.SetTop(backgroundImage, top + deltaY);

			for (int i = 0; i < _AllPolygons.Count; i++) {
				var polygon = _AllPolygons[i];
				for (int j = 0; j < polygon.Points.Count; j++) {
					polygon.Points[j] = new Point(polygon.Points[j].X + deltaX, polygon.Points[j].Y + deltaY);

					if (_AllEllipses.Count > i * polygon.Points.Count + j) 
					{
						// Déplacez les Ellipse
						Ellipse pointEllipse = _AllEllipses[i * polygon.Points.Count + j];
						Canvas.SetLeft(pointEllipse, Canvas.GetLeft(pointEllipse) + deltaX);
						Canvas.SetTop(pointEllipse, Canvas.GetTop(pointEllipse) + deltaY);
					}
				}
			}
		}

		private void MoveImageUp_Click(object sender, RoutedEventArgs e) {
			MoveImageAndPolygons(0, -taillePas);
		}

		private void MoveImageDown_Click(object sender, RoutedEventArgs e) {
			MoveImageAndPolygons(0, taillePas);
		}

		private void MoveImageLeft_Click(object sender, RoutedEventArgs e) {
			MoveImageAndPolygons(-taillePas, 0);
		}

		private void MoveImageRight_Click(object sender, RoutedEventArgs e) {
			MoveImageAndPolygons(taillePas, 0);
		}

		#endregion
	}
}
