using JDR.Vue.ViewModels;
using JDR.Vue.Views;
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

		private MainWindow _MainWindow;
		public UCMainMenu(MainMenuViewModel mainMenuViewModel, MainWindow window) {
			_MainWindow = window;
            DataContext = mainMenuViewModel;
			InitializeComponent();
		}

		private void OpenGame(object sender, RoutedEventArgs e) {
			_MainWindow.OpenGame();
		}

		private void OpenGameCreation(object sender, RoutedEventArgs e) {
			_MainWindow.OpenGameCreation();
		}

		private void EditGame(object sender, RoutedEventArgs e)
		{
			var window = new WinGameSelection(_MainWindow);
			window.ShowDialog();
		}
    }
}
