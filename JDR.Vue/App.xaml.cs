using JDR.Infra;
using JDR.Vue.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JDR.Vue {

	public partial class App : Application {

		public ServiceProvider ServiceProvider { get; }

		public App() {
			var appInjections = new AppInjections();
			ServiceProvider = appInjections.ServiceProvider;
		}

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
			var mainWindow = new MainWindow { DataContext = mainViewModel };
			mainWindow.Show();
		}

	}
}
