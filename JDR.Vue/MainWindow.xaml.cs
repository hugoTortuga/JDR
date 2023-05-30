using JDR.Infra;
using JDR.Model;
using JDR.Service;
using JDR.Vue.ViewModels;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace JDR.Vue {

	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
            GoToConnection();
		}

		public void GoToMainMenu() {
            CurrentControl.Content = new UCMainMenu(new MainMenuViewModel(), this);
		}

		public void OpenGameCreation() {
            CurrentControl.Width = ActualWidth;
            CurrentControl.Height = ActualHeight;
            CurrentControl.Content = new UCGameCreation(this);
		}

        public void OpenGameCreation(Game existingGame)
        {
            CurrentControl.Width = ActualWidth;
            CurrentControl.Height = ActualHeight;
            CurrentControl.Content = new UCGameCreation(this, existingGame);
        }

        public void OpenGame(Game selectedGame)
        {
            CurrentControl.Width = ActualWidth;
            CurrentControl.Height = ActualHeight;
            CurrentControl.Content = new UCGame(this, selectedGame);
        }

        public void OpenGameSelection(IList<Game> availableGames)
        {
            CurrentControl.Width = 400;
            CurrentControl.Height = 600;
            CurrentControl.Content = new UCGameSelection(this, availableGames);
        }

        public void GoToSubscribtion()
        {
            CurrentControl.Content = new UCSubscribtion(this);
        }

        public void GoToConnection()
        {
            CurrentControl.Content = new UCConnection(this);
        }
    }
}
