using JDR.Infra;
using JDR.Vue.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using AutoMapper;
using JDR.Service;
using JDR.Core;

namespace JDR.Vue {
	public class AppInjections {

		public ServiceProvider ServiceProvider { get; }

		public AppInjections() {

			var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

			var services = new ServiceCollection();

			string connectionString = configuration.GetConnectionString("MySqlConnection");

			services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
			);

			services.AddScoped<IMainRepository, MainRepository>();
			services.AddScoped<InventoryCore>();
			services.AddScoped<GameCore>();

			services.AddTransient<MainViewModel>();
			services.AddTransient<GameCreationViewModel>();

			ServiceProvider = services.BuildServiceProvider();
		}

	}
}
