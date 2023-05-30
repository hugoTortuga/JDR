using JDR.Model;
using JDR.Vue.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JDR.Vue.Views
{

    public partial class UCGame : UserControl
    {

        private bool _IsPlayerSelected;
        private Ellipse? _SelectionForm;
        private int _SelectionFormSize = 16;
        private double _FOV => SliderFOV.Value;
        private MainWindow _MainWindow;
        private IList<Polygon> _Obstacles;

        public UCGame(MainWindow window, Game selectedGame)
        {
            _MainWindow = window;
            var tableTopViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<TableTopViewModel>();
            tableTopViewModel.CurrentGame = selectedGame;
            DataContext = tableTopViewModel;
            _Obstacles = new List<Polygon>();
            InitializeComponent();
            SliderFOV.Value = 300;
        }

        private void SetObstacles()
        {
            _Obstacles = new List<Polygon>(GetObsctacles().Select(TransformObstacle));
            DrawFieldOfVision();
        }

        private IList<Obstacle> GetObsctacles()
        {
            return ((TableTopViewModel)DataContext).CurrentScene.Obstacles;
        }

        private Polygon TransformObstacle(Obstacle obs)
        {
            return new Polygon
            {
                Points = new PointCollection(
                            obs.Points.Select(l => new Point(l.X, l.Y))
                        ),
                Fill = new SolidColorBrush(Colors.Black)
            };
        }

        private void OpenCharacterSheet(object sender, RoutedEventArgs e)
        {
            var selectedPlayer = (Character)((MenuItem)sender).DataContext;
            if (selectedPlayer != null)
                new WinCharacterSheet(selectedPlayer, ((TableTopViewModel)DataContext).GameCore).Show();
        }

        private void PlayerClicked(object sender, MouseButtonEventArgs e)
        {
            Player1.CaptureMouse();
            if (_IsPlayerSelected)
            {
                _IsPlayerSelected = false;
                if (_SelectionForm != null)
                {
                    GameCanvas.Children.Remove(_SelectionForm);
                    _SelectionForm = null;
                }
            }
            else
            {
                _IsPlayerSelected = true;
                _SelectionForm = new Ellipse
                {
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

        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((UIElement)sender);
            AfficherPingOnMap(position, (Canvas)sender);
        }

        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            if (Player1.IsMouseCaptured)
            {
                DrawFieldOfVision();
                MovePlayerToNewPosition(e.GetPosition(Player1.Parent as UIElement));
            }
        }

        private void MovePlayerToNewPosition(Point newPosition)
        {
            Canvas.SetLeft(Player1, newPosition.X - (Player1.Width / 2));
            Canvas.SetTop(Player1, newPosition.Y - (Player1.Height / 2));
            if (_IsPlayerSelected)
            {
                Canvas.SetLeft(_SelectionForm, newPosition.X - (Player1.Width / 2) - (_SelectionFormSize / 2));
                Canvas.SetTop(_SelectionForm, newPosition.Y - (Player1.Height / 2) - (_SelectionFormSize / 2));
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Player1.ReleaseMouseCapture();
            base.OnMouseUp(e);
        }

        private void ChangeToken(object sender, RoutedEventArgs e)
        {
            try
            {
                Player1.Source = new BitmapImage(new Uri(GetImageURL()));
            }
            catch
            {

            }
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            ((TableTopViewModel)DataContext).StopMusic();
            _MainWindow.GoToMainMenu();
        }

        private string GetImageURL()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            throw new ApplicationException("Aucune image sélectionnée");
        }


        private Path CurrentFOVPath;

        public void DrawFieldOfVision()
        {
            if (((TableTopViewModel)DataContext).CurrentScene == null) return;
            if (!((TableTopViewModel)DataContext).CurrentScene.HasFogOfWarEnable)
            {
                if (GameCanvas.Children.Contains(CurrentFOVPath))
                    GameCanvas.Children.Remove(CurrentFOVPath);
                return;
            }

            var playerPosition = GetPlayerCenterPostition();
            GameCanvas.Children.Remove(CurrentFOVPath);
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = playerPosition, IsClosed = true, IsFilled = true };
            pathGeometry.Figures.Add(pathFigure);

            int numberOfRays = 720;
            double angleStep = 360.0 / numberOfRays;

            for (int i = 0; i <= numberOfRays; i++)
            {
                var ray = ComputeRay(playerPosition, angleStep, _FOV, i);
                pathFigure.Segments.Add(ray);
            }

            var canvasRect = new Rect(0, 0, 2000, 1400);
            var canvasRectangleGeometry = new RectangleGeometry(canvasRect);

            var combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, canvasRectangleGeometry, pathGeometry);

            CurrentFOVPath = new Path
            {
                Fill = new SolidColorBrush(Colors.Black) { Opacity = 1 },
                Data = combinedGeometry
            };

            GameCanvas.Children.Add(CurrentFOVPath);
        }

        private LineSegment ComputeRay(Point playerPosition, double angleStep, double radius, int i)
        {
            double angle = i * angleStep;
            double x = playerPosition.X + radius * Math.Cos(Math.PI * angle / 180.0);
            double y = playerPosition.Y + radius * Math.Sin(Math.PI * angle / 180.0);

            var endPoint = new Point(x, y);
            var ray = new LineSegment(endPoint, true);

            var playerToEndpoint = new Utils.Line(playerPosition, endPoint);
            Point? nearestIntersection = null;
            double nearestIntersectionDistance = double.MaxValue;

            foreach (var obstacle in _Obstacles)
            {
                var intersectionPoints = playerToEndpoint.Intersects(obstacle);

                foreach (var intersectionPoint in intersectionPoints)
                {
                    double distance = (playerPosition - intersectionPoint).Length;
                    if (distance < nearestIntersectionDistance)
                    {
                        nearestIntersection = intersectionPoint;
                        nearestIntersectionDistance = distance;
                    }
                }
            }

            if (nearestIntersection.HasValue)
            {
                ray.Point = nearestIntersection.Value;
            }
            return ray;
        }

        private Point GetPlayerCenterPostition()
        {
            return new Point(Canvas.GetLeft(Player1) + Player1.Width / 2, Canvas.GetTop(Player1) + Player1.Height / 2);
        }

        private void SceneChanged(object sender, SelectionChangedEventArgs e)
        {
            SetObstacles();
            ResizeMap();

            var playerSpawnPoint = ((TableTopViewModel)DataContext).CurrentScene.PlayerSpawnPoint;
            MovePlayerToNewPosition(new Point(playerSpawnPoint.X, playerSpawnPoint.Y));
        }

        private void ResizeMap()
        {
            //var imageBackground = GameCanvas.Background;
            //var scene = ((TableTopViewModel)DataContext).CurrentScene;

            //GameCanvas.Background = imageBackground;

            //Canvas.SetLeft(GameCanvas, scene.XMapTranslation);
            //Canvas.SetTop(GameCanvas, scene.YMapTranslation);

            //GameCanvas.Width = scene.Width;
            //GameCanvas.Height = scene.Height;

        }

        private void AfficherPingOnMap(Point position, Canvas canvas)
        {
            Ellipse pingCircle = new Ellipse
            {
                Width = 70,
                Height = 70,
                Stroke = Brushes.Red,
                StrokeThickness = 3,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            Canvas.SetLeft(pingCircle, position.X - pingCircle.Width / 2);
            Canvas.SetTop(pingCircle, position.Y - pingCircle.Height / 2);

            pingCircle.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(0, 0),
                    new TranslateTransform()
                }
            };

            canvas.Children.Add(pingCircle);

            Storyboard storyboard = (Storyboard)FindResource("PingAnimation");
            storyboard.Begin(pingCircle);

            storyboard.Completed += (s, _) =>
            {
                canvas.Children.Remove(pingCircle);
            };
        }

        private void SliderFOV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DrawFieldOfVision();
        }
    }
}
