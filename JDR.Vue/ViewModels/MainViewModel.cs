using JDR.Infra;
using JDR.Model;
using JDR.Service;
using JDR.Vue.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JDR.Vue.ViewModels {
	public class MainViewModel : ViewModelBase {

		private InventoryCore Service;
        public MainViewModel(InventoryCore service)
        {
			Service = service;
		}
		
	}
}
