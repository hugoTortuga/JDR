using JDR.Model;
using JDR.Vue.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
		private Polygon _CurrentPolygon;
		private IList<Polygon> _AllPolygons;
		private string _ImagePath;
		public UCMapEditor() {
			InitializeComponent();
			_CurrentPolygon = MyPolygon;
			_AllPolygons = new List<Polygon>();
		}

		private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

			if (e.ClickCount == 2)
			{
				DoubleClick();
				return;
			}

			Point clickPosition = e.GetPosition(MyCanvas);

			// Create an ellipse to represent the point
			Ellipse pointEllipse = new Ellipse {
				Width = 5,
				Height = 5,
				Fill = Brushes.Black
			};

			// Position the ellipse at the clicked point
			Canvas.SetLeft(pointEllipse, clickPosition.X - pointEllipse.Width / 2);
			Canvas.SetTop(pointEllipse, clickPosition.Y - pointEllipse.Height / 2);

			// Add the point to the canvas and the polygon
			MyCanvas.Children.Add(pointEllipse);
			_CurrentPolygon.Points.Add(clickPosition);
		}

		private void DoubleClick() {
			if (_CurrentPolygon.Points.Count > 2) {

				_CurrentPolygon.Points.Add(_CurrentPolygon.Points[0]);
				_AllPolygons.Add(_CurrentPolygon);
				UpdateObstacles(_CurrentPolygon);

				_CurrentPolygon = new Polygon {
					Stroke = Brushes.Black,
					StrokeThickness = 1,
					Fill = Brushes.Transparent
				};

				MyCanvas.Children.Add(_CurrentPolygon);
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
	}
}
