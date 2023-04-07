using JDR.Vue.ViewModels;
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

		private readonly ServiceProvider _serviceProvider;

		public App() {
			_serviceProvider = Dependencies.ConfigureServices();
		}

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			// Instanciez MainViewModel à l'aide de l'injection de dépendances
			var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

			// Instanciez et affichez la fenêtre principale
			var mainWindow = new MainWindow { DataContext = mainViewModel };
			mainWindow.Show();
		}

	}
}
