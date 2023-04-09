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

namespace JDR.Vue.Views {

	public partial class UCGameCreation : UserControl {

		private MainWindow _MainWindow;
		public UCGameCreation(MainWindow mainWindow) {
			_MainWindow = mainWindow;
			DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<GameCreationViewModel>();
			InitializeComponent();
		}

		private void BackToMenu(object sender, RoutedEventArgs e) {
			_MainWindow.BackToMenu();
		}
	}
}
