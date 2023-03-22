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

		private const double MoveSpeed = 3; // Adjust as needed
		private readonly DispatcherTimer _movementTimer;
		private Point _previousPlayerPosition;
		private int fieldOfVisionDistance = 100;

		public UCGame() {
			InitializeComponent();
			DrawFieldOfVision();
			_movementTimer = new DispatcherTimer {
				Interval = TimeSpan.FromMilliseconds(1000 / 144), // 144 FPS
			};
			_movementTimer.Tick += MovementTimer_Tick;
			_movementTimer.Start();
		}

		private void MovementTimer_Tick(object sender, EventArgs e) {
			MovePlayerBasedOnPressedKeys();
		}

		private void MovePlayer(double deltaX, double deltaY) {
			double newX = Canvas.GetLeft(Player1) + deltaX;
			double newY = Canvas.GetTop(Player1) + deltaY;

			// Update the player token's position
			Canvas.SetLeft(Player1, newX);
			Canvas.SetTop(Player1, newY);
		}

		private Path fieldOfVision;

		private void DrawFieldOfVision() {
			Point playerPosition = new Point(Canvas.GetLeft(Player1) + Player1.ActualWidth / 2, Canvas.GetTop(Player1) + Player1.ActualHeight / 2);

			// Create a geometry representing the entire canvas
			RectangleGeometry canvasGeometry = new RectangleGeometry(new Rect(0, 0, GameCanvas.Width, GameCanvas.Height));

			// Create a geometry representing the circular field of vision
			EllipseGeometry visionGeometry = new EllipseGeometry(playerPosition, fieldOfVisionDistance, fieldOfVisionDistance);

			PathGeometry fogOfWarGeometry = new PathGeometry();

			// Add the main figure (the entire canvas)
			fogOfWarGeometry.Figures.Add(new PathFigure {
				StartPoint = new Point(0, 0),
				IsClosed = true,
				Segments =
				{
					new LineSegment(new Point(GameCanvas.Width, 0), true),
					new LineSegment(new Point(GameCanvas.Width, GameCanvas.Height), true),
					new LineSegment(new Point(0, GameCanvas.Height), true)
				}
			});

			// Add the field of vision figure (cut-out circle)
			fogOfWarGeometry.Figures.Add(new PathFigure {
				StartPoint = new Point(playerPosition.X + fieldOfVisionDistance, playerPosition.Y),
				IsClosed = true,
				IsFilled = true,
				Segments =
				{
					new ArcSegment(new Point(playerPosition.X - fieldOfVisionDistance, playerPosition.Y), new Size(fieldOfVisionDistance, fieldOfVisionDistance), 0, false, SweepDirection.Clockwise, true),
					new ArcSegment(new Point(playerPosition.X + fieldOfVisionDistance, playerPosition.Y), new Size(fieldOfVisionDistance, fieldOfVisionDistance), 0, false, SweepDirection.Clockwise, true)
				}
			});

			// Create a path for the fog of war geometry and set its properties
			fieldOfVision = new Path {
				Data = fogOfWarGeometry,
				Fill = new SolidColorBrush(Color.FromArgb(153, 0, 0, 0)),
				IsHitTestVisible = false
			};

			// Add the fog of war to the canvas
			GameCanvas.Children.Add(fieldOfVision);
		}


		private readonly HashSet<Key> _pressedKeys = new HashSet<Key>();

		private void UserControl_KeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Z || e.Key == Key.Q || e.Key == Key.S || e.Key == Key.D) {
				_pressedKeys.Add(e.Key);
			}
		}
		private void MovePlayerBasedOnPressedKeys() {
			double deltaX = 0;
			double deltaY = 0;

			if (_pressedKeys.Contains(Key.Z)) {
				deltaY -= MoveSpeed;
			}

			if (_pressedKeys.Contains(Key.S)) {
				deltaY += MoveSpeed;
			}

			if (_pressedKeys.Contains(Key.Q)) {
				deltaX -= MoveSpeed;
			}

			if (_pressedKeys.Contains(Key.D)) {
				deltaX += MoveSpeed;
			}

			if (deltaX != 0 || deltaY != 0) {
				MovePlayer(deltaX, deltaY);

				Point currentPlayerPosition = new Point(Canvas.GetLeft(Player1) + Player1.ActualWidth / 2, Canvas.GetTop(Player1) + Player1.ActualHeight / 2);
				if (_previousPlayerPosition != currentPlayerPosition) {
					_previousPlayerPosition = currentPlayerPosition;

					// Redraw the field of vision after moving the player
					GameCanvas.Children.Remove(fieldOfVision);
					DrawFieldOfVision();
				}
			}
		}


		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			Focus();
		}

		private void UserControl_KeyUp(object sender, KeyEventArgs e) {
			_pressedKeys.Remove(e.Key);
		}
	}
}
