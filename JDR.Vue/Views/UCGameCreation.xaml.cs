using JDR.Model;
using JDR.Vue.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace JDR.Vue.Views
{

    public partial class UCGameCreation : UserControl
    {

        private IList<Polygon> _AllPolygons;
        private IList<Ellipse> _AllEllipses;

        private double ActualImageWidth;
        private double ActualImageHeight;

        private double _ZoomFactor = 1.0;
        private const double ZoomIncrement = 0.1;
        private Point _previousMousePosition;
        private bool _IsDragging;
        private Polygon _selectedPolygon;

        private MainWindow _MainWindow;
        
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            _MainWindow.GoToMainMenu();
        }
        public UCGameCreation(MainWindow mainWindow, Game? existingGame = null)
        {
            _MainWindow = mainWindow;
            DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<GameCreationViewModel>();
            if (existingGame != null)
                ((GameCreationViewModel)DataContext).CurrentGame = existingGame;
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

            SelectionMode = SelectionMode.None;
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectionMode == SelectionMode.Obstacle)
                HandleClickObstacle(e);
            else if (SelectionMode == SelectionMode.Links)
            {

            }
            else if (SelectionMode == SelectionMode.Player)
            {
                HandleClickPlayer(e);
            }

        }

        private Ellipse ZoneApparitionJoueurs;


        public void SetScene()
        {
            var scene = new Scene
            {
                Obstacles = new List<Obstacle>(_AllPolygons.Select(p => new Obstacle
                {
                    Points = p.Points.Select(e => new System.Drawing.Point((int)e.X, (int)e.Y)).ToList()
                })),
                ZoomValue = _ZoomFactor,
                XMapTranslation = CurrentXTranslation,
                YMapTranslation = CurrentYTranslation,
                Height = (int)(ActualImageHeight * _ZoomFactor),
                Width = (int)(ActualImageWidth * _ZoomFactor)
            };
            ((GameCreationViewModel)DataContext).CurrentScene = scene;
        }

        private void HandleClickPlayer(MouseButtonEventArgs e)
        {
            if (MyCanvas.Children.Contains(ZoneApparitionJoueurs))
                MyCanvas.Children.Remove(ZoneApparitionJoueurs);

            int zoneApparitionJoueurSize = 100;
            var clickPosition = e.GetPosition(MyCanvas);
            ZoneApparitionJoueurs = new Ellipse
            {
                Width = zoneApparitionJoueurSize,
                Height = zoneApparitionJoueurSize,
                StrokeThickness = 2,
                Stroke = System.Windows.Media.Brushes.Black
            };
            Canvas.SetLeft(ZoneApparitionJoueurs, clickPosition.X - ZoneApparitionJoueurs.Width / 2);
            Canvas.SetTop(ZoneApparitionJoueurs, clickPosition.Y - ZoneApparitionJoueurs.Height / 2);
            MyCanvas.Children.Add(ZoneApparitionJoueurs);
            ((GameCreationViewModel)DataContext).SetApparitionJoueurs(new System.Drawing.Point((int)clickPosition.X, (int)clickPosition.Y));
        }

        private void HandleClickObstacle(MouseButtonEventArgs e)
        {

            if (CurrentPolygon == null) CurrentPolygon = new Polygon();
            if (e.ClickCount == 2)
            {
                DoubleClick();
                return;
            }

            var clickPosition = e.GetPosition(MyCanvas);

            var pointEllipse = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Black
            };

            Canvas.SetLeft(pointEllipse, clickPosition.X - pointEllipse.Width / 2);
            Canvas.SetTop(pointEllipse, clickPosition.Y - pointEllipse.Height / 2);

            MyCanvas.Children.Add(pointEllipse);
            _AllEllipses.Add(pointEllipse);
            CurrentPolygon.Points.Add(clickPosition);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (_selectedPolygon != null)
                {

                    int selectedIndex = _AllPolygons.IndexOf(_selectedPolygon);
                    if (selectedIndex >= 0 && selectedIndex < _AllPolygons.Count)
                    {
                        var obstacles = ((GameCreationViewModel)DataContext).Obstacles;
                        obstacles.RemoveAt(selectedIndex);

                        if (_AllPolygons.Count > 0)
                        {
                            int pointCount = _AllPolygons[selectedIndex].Points.Count - 1;
                            RemoveEllipsesAndTextBlocks(selectedIndex * pointCount, pointCount);
                        }

                        _AllPolygons.RemoveAt(selectedIndex);
                        SetScene();

                    }
                    // Supprimez le polygone du canvas
                    MyCanvas.Children.Remove(_selectedPolygon);

                    // Réinitialisez le polygone sélectionné
                    _selectedPolygon = null;
                }
            }
        }

        private void RemoveEllipsesAndTextBlocks(int startIndex, int count)
        {
            for (int i = startIndex + count - 1; i >= startIndex; i--)
            {
                // Supprimez les Ellipse du canvas et de la liste _AllEllipses
                MyCanvas.Children.Remove(_AllEllipses[i]);
                _AllEllipses.RemoveAt(i);
            }
        }

        private void DoubleClick()
        {
            if (CurrentPolygon.Points.Count > 2)
            {

                CurrentPolygon.Fill = new SolidColorBrush(new Color
                {
                    R = 0,
                    G = 0,
                    B = 0,
                    A = 125
                });
                CurrentPolygon.Points.Add(CurrentPolygon.Points[0]);
                _AllPolygons.Add(CurrentPolygon);
                UpdateObstacles(CurrentPolygon);

                SetScene();

                CurrentPolygon = new Polygon
                {
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

        private void UpdateObstacles(Polygon currentPolygon)
        {
            var obstacles = ((GameCreationViewModel)DataContext).Obstacles;
            var obstacle = new Obstacle();
            foreach (var point in currentPolygon.Points)
            {
                obstacle.Points.Add(new System.Drawing.Point((int)point.X, (int)point.Y));
            }
            obstacles.Add(obstacle);
        }

        private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {

                if (e.Delta > 0)
                    _ZoomFactor += ZoomIncrement;
                else
                    _ZoomFactor -= ZoomIncrement;

                _ZoomFactor = Math.Max(_ZoomFactor, 0.1); // Limite le dézoomage

                SetScene();


                var scaleTransform = new ScaleTransform(_ZoomFactor, _ZoomFactor);
                backgroundImage.LayoutTransform = scaleTransform;
                ActualImageWidth = backgroundImage.ActualWidth;
                ActualImageHeight = backgroundImage.ActualHeight;
            }
        }

        private void BackgroundImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                _IsDragging = true;
                _previousMousePosition = e.GetPosition(MyCanvas);
                backgroundImage.CaptureMouse();
            }
        }

        private void BackgroundImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _IsDragging = false;
            backgroundImage.ReleaseMouseCapture();
        }

        private void BackgroundImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_IsDragging) return;
            Point currentPosition = e.GetPosition(MyCanvas);
            double deltaX = currentPosition.X - _previousMousePosition.X;
            double deltaY = currentPosition.Y - _previousMousePosition.Y;

            double left = Canvas.GetLeft(backgroundImage) + deltaX;
            double top = Canvas.GetTop(backgroundImage) + deltaY;

            Canvas.SetLeft(backgroundImage, left);
            Canvas.SetTop(backgroundImage, top);

            _previousMousePosition = currentPosition;
        }

        #region SelectionMode

        private SelectionMode SelectionMode;

        public void PlaceObstacle(object sender, RoutedEventArgs e)
        {
            SelectionMode = SelectionMode.Obstacle;
            ButtonPlaceLinks.IsChecked = false;
            ButtonPlacePlayer.IsChecked = false;
        }

        public void PlacePlayer(object sender, RoutedEventArgs e)
        {
            SelectionMode = SelectionMode.Player;
            ButtonPlaceLinks.IsChecked = false;
            ButtonPlaceObstacle.IsChecked = false;
        }

        public void PlaceLinks(object sender, RoutedEventArgs e)
        {
            SelectionMode = SelectionMode.Links;
            ButtonPlaceObstacle.IsChecked = false;
            ButtonPlacePlayer.IsChecked = false;
        }

        #endregion

        #region CurrentPolygonSelectionSuppression
        private void CurrentPolygon_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Polygon polygon && _selectedPolygon != polygon)
            {
                polygon.Stroke = Brushes.Gray;
            }
        }

        private void CurrentPolygon_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Polygon polygon && polygon != _selectedPolygon)
            {
                polygon.Stroke = Brushes.Black;
            }
        }

        private void CurrentPolygon_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_selectedPolygon != null)
            {
                _selectedPolygon.Stroke = Brushes.Black;
            }

            if (sender is Polygon polygon)
            {
                if (_selectedPolygon != polygon)
                {
                    _selectedPolygon = polygon;
                    _selectedPolygon.Stroke = Brushes.Red;
                }
                else
                {
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

        private void MoveImageAndPolygons(int deltaX, int deltaY)
        {
            CurrentXTranslation += deltaX;
            CurrentYTranslation += deltaY;
            SetScene();

            double left = Canvas.GetLeft(backgroundImage);
            if (double.IsNaN(left)) left = 0;
            double top = Canvas.GetTop(backgroundImage);
            if (double.IsNaN(top)) top = 0;

            Canvas.SetLeft(backgroundImage, left + deltaX);
            Canvas.SetTop(backgroundImage, top + deltaY);

            for (int i = 0; i < _AllPolygons.Count; i++)
            {
                var polygon = _AllPolygons[i];
                for (int j = 0; j < polygon.Points.Count; j++)
                {
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

        private void MoveImageUp_Click(object sender, RoutedEventArgs e)
        {
            MoveImageAndPolygons(0, -taillePas);
        }

        private void MoveImageDown_Click(object sender, RoutedEventArgs e)
        {
            MoveImageAndPolygons(0, taillePas);
        }

        private void MoveImageLeft_Click(object sender, RoutedEventArgs e)
        {
            MoveImageAndPolygons(-taillePas, 0);
        }

        private void MoveImageRight_Click(object sender, RoutedEventArgs e)
        {
            MoveImageAndPolygons(taillePas, 0);
        }

        #endregion
    }

    public enum SelectionMode
    {
        Obstacle,
        Player,
        Links,
        None
    }
}
