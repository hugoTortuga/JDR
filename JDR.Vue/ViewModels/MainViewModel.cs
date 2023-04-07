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
		private InventoryService Service;
        public MainViewModel(InventoryService service)
        {
			Service = service;
		}

		public async void Test() {
			var shield = new InventoryItem("Bouclier 4");
			await Service.AddProduct(shield);
		}

		
	}
}
