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
			var mainViewModel = App.Current.MainWindow.DataContext as MainViewModel;
			if (mainViewModel != null) mainViewModel.MoveToGameCreation();
			
		}

	}
}
