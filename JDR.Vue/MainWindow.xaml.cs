using JDR.Infra;
using JDR.Service;
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

	public partial class MainWindow : Window {
		public MainWindow() {
			var service = GetService();
			DataContext = new MainViewModel();
			InitializeComponent();
		}

		private ServiceBase GetService() {
			return new ServiceBase(new BaseRepository());
		}
	}
}
