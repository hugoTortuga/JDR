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
		private IList<Polygon> _AllPolygons;
		public UCMapEditor() {
			InitializeComponent();
			_AllPolygons = new List<Polygon>();
		}

		private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
		{
			if (CurrentPolygon == null) CurrentPolygon = new Polygon();
			if (e.ClickCount == 2)
			{
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
			CurrentPolygon.Points.Add(clickPosition);
		}

		private void DoubleClick() {
			if (CurrentPolygon.Points.Count > 2) {

				CurrentPolygon.Points.Add(CurrentPolygon.Points[0]);
				_AllPolygons.Add(CurrentPolygon);
				UpdateObstacles(CurrentPolygon);

				CurrentPolygon = new Polygon {
					Stroke = Brushes.Black,
					StrokeThickness = 1,
					Fill = Brushes.Transparent
				};

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
	}
}
