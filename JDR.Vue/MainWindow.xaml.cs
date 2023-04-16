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
			CurrentControl.Content = new UCMainMenu(new MainMenuViewModel(), this);
		}

		public void BackToMenu() {
			CurrentControl.Content = new UCMainMenu(new MainMenuViewModel(), this);
		}

		public void OpenGameCreation() {
			CurrentControl.Content = new UCGameCreation(this);
		}

        public void OpenGameCreation(Game existingGame)
        {
            CurrentControl.Content = new UCGameCreation(this, existingGame);
        }

        public void OpenGame(Game selectedGame)
        {
            CurrentControl.Content = new UCGame(this, selectedGame);
        }
    }
}
