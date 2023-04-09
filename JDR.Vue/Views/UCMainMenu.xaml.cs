using JDR.Vue.ViewModels;
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

namespace JDR.Vue {

	public partial class UCMainMenu : UserControl {

		private MainWindow _mainWindow;
		public UCMainMenu(MainMenuViewModel mainMenuViewModel, MainWindow window) {
			_mainWindow = window;
			DataContext = mainMenuViewModel;
			InitializeComponent();
		}

		private void OpenGame(object sender, RoutedEventArgs e) {
			_mainWindow.OpenGame();
		}

		private void OpenGameCreation(object sender, RoutedEventArgs e) {
			_mainWindow.OpenGameCreation();
		}
    }
}
