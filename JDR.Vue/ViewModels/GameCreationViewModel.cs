using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class GameCreationViewModel {

		public void GoToMenu() {
			var mainViewModel = App.Current.MainWindow.DataContext as MainViewModel;
			if (mainViewModel != null) {
				mainViewModel.CurrentControl = new UCMainMenu();
			}
		}

	}
}
