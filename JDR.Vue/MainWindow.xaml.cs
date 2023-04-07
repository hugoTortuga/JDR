using JDR.Infra;
using JDR.Service;
using JDR.Vue.ViewModels;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JDR.Vue {

	public partial class MainWindow : Window {
		public MainWindow() {
			DataContext = new MainViewModel();
			InitializeComponent();
			CurrentControl.Content = new UCMainMenu(new MainMenuViewModel(), this);
		}

		public void OpenGame() {
			CurrentControl.Content = new UCGame(this);
		}

		internal void BackToMenu() {
			CurrentControl.Content = new UCMainMenu(new MainMenuViewModel(), this);
		}

		internal void OpenMapEditor() {
			CurrentControl.Content = new UCMapEditor(this);
		}
	}
}
