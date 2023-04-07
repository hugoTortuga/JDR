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
		private MainWindow _MainWindow;
        public UCMapEditor(MainWindow window) {
			_MainWindow = window;
			InitializeComponent();
		}

		private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			Point clickPosition = e.GetPosition(MyCanvas);

			var pointEllipse = new Ellipse {
				Width = 5,
				Height = 5,
				Fill = Brushes.Black
			};

			// Position the ellipse at the clicked point
			Canvas.SetLeft(pointEllipse, clickPosition.X - pointEllipse.Width / 2);
			Canvas.SetTop(pointEllipse, clickPosition.Y - pointEllipse.Height / 2);

			// Add the point to the canvas and the polygon
			MyCanvas.Children.Add(pointEllipse);
			MyPolygon.Points.Add(clickPosition);
		}

		private void BackToMenu(object sender, RoutedEventArgs e) {
			_MainWindow.BackToMenu();
		}
	}
}
