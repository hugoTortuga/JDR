using JDR.Infra;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels {
	public class MainMenuViewModel : ViewModelBase {

        public MainMenuViewModel()
        {
            
        }

        public void OpenGameCreation() {
            ((MainViewModel)App.Current.MainWindow.DataContext).MoveToGameCreation();
		}

		public void OpenGame() {
			((MainViewModel)App.Current.MainWindow.DataContext).MoveToGame();
		}

	}
}
