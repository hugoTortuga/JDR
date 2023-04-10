using JDR.Infra;
using JDR.Vue.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using AutoMapper;
using JDR.Service;
using JDR.Core;
using System;

namespace JDR.Vue {
	public class AppInjections {

		public ServiceProvider ServiceProvider { get; }

		public AppInjections() {

			var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

			string connectionString = configuration.GetConnectionString("MySqlConnection") ?? throw new ApplicationException("Aucune connexion string");
			string basePathImageDB = configuration.GetSection("Paths")["ImageDBPath"] ?? throw new ApplicationException("Aucune base de données d'images accessible");

			var services = new ServiceCollection();

			services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
			);

			services.AddScoped<IMainRepository, MainRepository>();
			services.AddScoped<IImageUploader, ImageUploader>(basePathParameter => {
				return new ImageUploader(basePathImageDB);
			});
			services.AddScoped<InventoryCore>();
			services.AddScoped<GameCore>();

			services.AddTransient<MainViewModel>();
			services.AddTransient<GameCreationViewModel>();

			ServiceProvider = services.BuildServiceProvider();
		}

	}
}
