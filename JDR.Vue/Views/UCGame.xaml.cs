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

namespace JDR.Vue.Views
{
    /// <summary>
    /// Logique d'interaction pour UCGame.xaml
    /// </summary>
    public partial class UCGame : UserControl
    {

        private bool _IsPlayerSelected;
        private Rectangle _SelectionRectangle;
        private Point _MouseOffset;

        public UCGame()
        {
            InitializeComponent();
        }

        private void OpenCharacterSheet(object sender, RoutedEventArgs e)
        {
            new WinCharacterSheet().ShowDialog();
        }

        private void PlayerClicked(object sender, MouseButtonEventArgs e)
        {
            if (_IsPlayerSelected)
            {
                _IsPlayerSelected = false;
                if (_SelectionRectangle != null)
                {
                    GameCanvas.Children.Remove(_SelectionRectangle);
                    _SelectionRectangle = null;
                }
            }
            else
            {
                _IsPlayerSelected = true;
                _SelectionRectangle = new Rectangle
                {
                    Width = Player1.ActualWidth + 4,
                    Height = Player1.ActualHeight + 4,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(_SelectionRectangle, Canvas.GetLeft(Player1) - 2);
                Canvas.SetTop(_SelectionRectangle, Canvas.GetTop(Player1) - 2);

                GameCanvas.Children.Add(_SelectionRectangle);
            }
            Player1.CaptureMouse();
            _MouseOffset = e.GetPosition(Player1);
        }

        private void GameCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_IsPlayerSelected)
            {
                var newMousePosition = e.GetPosition(GameCanvas);

                Canvas.SetLeft(Player1, newMousePosition.X - (Player1.Width / 2));
                Canvas.SetTop(Player1, newMousePosition.Y - (Player1.Height / 2));

                Canvas.SetLeft(_SelectionRectangle, newMousePosition.X - (Player1.Width / 2));
                Canvas.SetTop(_SelectionRectangle, newMousePosition.Y - (Player1.Height / 2));
            }
        }

        private void GameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GameCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsPlayerSelected && GameCanvas.IsMouseCaptured)
            {

            }
        }

        private void Player1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Player1.ReleaseMouseCapture();
            base.OnMouseUp(e);
        }

        private void Player1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Player1.IsMouseCaptured)
            {
                // Get the current mouse position
                Point mousePos = e.GetPosition(Player1.Parent as UIElement);

                Canvas.SetLeft(Player1, mousePos.X - (Player1.Width / 2));
                Canvas.SetTop(Player1, mousePos.Y - (Player1.Height / 2));

                if (_SelectionRectangle != null)
                {
                    Canvas.SetLeft(_SelectionRectangle, mousePos.X - (Player1.Width / 2));
                    Canvas.SetTop(_SelectionRectangle, mousePos.Y - (Player1.Height / 2));
                }
            }
        }
    }
}
