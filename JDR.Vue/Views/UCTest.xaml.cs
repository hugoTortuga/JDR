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
	/// Logique d'interaction pour UCTest.xaml
	/// </summary>
	public partial class UCTest : UserControl {

		private bool _IsPlayerSelected;
		private Ellipse _SelectionForm;
		private int _SelectionFormSize = 16;
		private double _BackgroundWidth;
		private double _BackgroundHeight;
		private TranslateTransform _TranslateTransformBackgroundMap;
		private IList<Geometry> _Obstacles;

		public UCTest(MainWindow window) {
			DataContext = new TableTopViewModel();
			InitializeComponent();
			_BackgroundWidth = BackgroundImageBrush.ImageSource.Width;
			_BackgroundHeight = BackgroundImageBrush.ImageSource.Height;
			_TranslateTransformBackgroundMap = new TranslateTransform();
			BackgroundImageBrush.Transform = _TranslateTransformBackgroundMap;
			GameCanvas.Background = BackgroundImageBrush;
			DrawFieldOfVision(new Point(500, 500), 400, new Rect { Height = 20, Location = new Point(300, 300), Width = 40 });
		}

		public void DrawFieldOfVision(Point playerPosition, double radius, Rect rockRect) {
			GameCanvas.Children.Clear();

			// Create the PathGeometry
			var pathGeometry = new PathGeometry();
			var pathFigure = new PathFigure { StartPoint = playerPosition, IsClosed = true, IsFilled = true };
			pathGeometry.Figures.Add(pathFigure);

			// Cast rays in a 360-degree radius around the player
			int numberOfRays = 3600;
			double angleStep = 360.0 / numberOfRays;

			for (int i = 0; i <= numberOfRays; i++) {
				double angle = i * angleStep;
				double x = playerPosition.X + radius * Math.Cos(Math.PI * angle / 180.0);
				double y = playerPosition.Y + radius * Math.Sin(Math.PI * angle / 180.0);

				Point endPoint = new Point(x, y);
				LineSegment ray = new LineSegment(endPoint, true);

				// Check for intersections with the rock obstacle
				var playerToEndpoint = new Line(playerPosition, endPoint);
				Point? nearestIntersection = null;
				double nearestIntersectionDistance = double.MaxValue;

				var intersectionPoints = playerToEndpoint.Intersects(rockRect);
				foreach (var intersectionPoint in intersectionPoints) {
					double distance = (playerPosition - intersectionPoint).Length;
					if (distance < nearestIntersectionDistance) {
						nearestIntersection = intersectionPoint;
						nearestIntersectionDistance = distance;
					}
				}

				if (nearestIntersection.HasValue) {
					ray.Point = nearestIntersection.Value;
				}

				// Add the ray to the path figure
				pathFigure.Segments.Add(ray);
			}

			var canvasRect = new Rect(0, 0, 1800, 1000);
			var canvasRectangleGeometry = new RectangleGeometry(canvasRect);

			// Use XOR to combine the player's field of vision and the canvas rectangle
			var combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, canvasRectangleGeometry, pathGeometry);

			// Create the Path object and set the Fill and Data properties
			var path = new Path {
				Fill = new SolidColorBrush(Colors.Black) { Opacity = 0.7 },
				Data = combinedGeometry
			};

			// Add the Path object to the Canvas
			GameCanvas.Children.Add(path);

			//EllipseGeometry fieldOfVision = new EllipseGeometry(new Point(200,200), 400, 400);
			//GeometryGroup visibleArea = new GeometryGroup();
			//visibleArea.Children.Add(fieldOfVision);
			//Geometry obstacleGeometry = Geometry.Parse("M 100,200 C 100,25 400,350 400,175 H 280");
			//CombinedGeometry subtracted = new CombinedGeometry(GeometryCombineMode.Exclude, visibleArea, obstacleGeometry);
			//visibleArea.Children.Add(subtracted);
			//visibleArea.Children.Add(subtracted);
			//visibleArea = subtracted;

			//ImageBrush mapBrush = new ImageBrush(new BitmapImage(new Uri("/Assets/background.jpeg"))) {
			//	Opacity = new GeometryDrawing(Brushes.Black, null, visibleArea)
			//};

			//Canvas background = new Canvas {
			//	Width = 1000,
			//	Height = 800,
			//	Background = mapBrush
			//};
		}

		private void UserControl_KeyDown(object sender, KeyEventArgs e) {
			_TranslateTransformBackgroundMap.X += 10;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			Focus();
		}
	}
	public class Line {
		public Point Start { get; set; }
		public Point End { get; set; }

		public Line(Point start, Point end) {
			Start = start;
			End = end;
		}

		public IList<Point> Intersects(Rect rect) {
			Line[] rectEdges =
			{
				new Line(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top)),
				new Line(new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom)),
				new Line(new Point(rect.Right, rect.Bottom), new Point(rect.Left, rect.Bottom)),
				new Line(new Point(rect.Left, rect.Bottom), new Point(rect.Left, rect.Top))
			};

			var intersectionPoints = new List<Point>();

			foreach (var edge in rectEdges) {
				var point = TryGetIntersectionWith(edge);
				if (point != null) intersectionPoints.Add(point.Value);
			}

			return intersectionPoints;
		}

		public Point? TryGetIntersectionWith(Line other) {
			var intersection = new Point();

			double a1 = End.Y - Start.Y;
			double b1 = Start.X - End.X;
			double c1 = a1 * Start.X + b1 * Start.Y;

			double a2 = other.End.Y - other.Start.Y;
			double b2 = other.Start.X - other.End.X;
			double c2 = a2 * other.Start.X + b2 * other.Start.Y;

			double delta = a1 * b2 - a2 * b1;
			if (delta == 0) {
				return null;
			}

			intersection.X = Math.Round((b2 * c1 - b1 * c2) / delta, 5, MidpointRounding.ToEven);
			intersection.Y = Math.Round((a1 * c2 - a2 * c1) / delta, 5, MidpointRounding.ToEven);

			if (IsInsideLineBounds(intersection) && other.IsInsideLineBounds(intersection)) {
				return intersection;
			}

			return null;
		}

		private bool IsInsideLineBounds(Point point) {
			double minX = Math.Min(Start.X, End.X);
			double maxX = Math.Max(Start.X, End.X);
			double minY = Math.Min(Start.Y, End.Y);
			double maxY = Math.Max(Start.Y, End.Y);

			return minX <= point.X && point.X <= maxX && minY <= point.Y && point.Y <= maxY;
		}
	}
}
