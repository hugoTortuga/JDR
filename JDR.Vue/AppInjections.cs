using JDR.Infra;
using JDR.Vue.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using JDR.Service;
using JDR.Core;
using System;
using AutoMapper;
using System.Windows.Forms;

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
            string basePathMusicDB = configuration.GetSection("Paths")["MusicDBPath"] ?? throw new ApplicationException("Aucune base de données de musiques accessible");

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
			);

            services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped<IMainRepository, MainRepository>();
			services.AddScoped<IImageStorage, ImageStorage>(basePathParameter => {
				return new ImageStorage(basePathImageDB);
			});
            services.AddScoped<IMusicStorage, MusicStorage>(basePathParameter => {
                return new MusicStorage(basePathMusicDB);
            });

            services.AddScoped<IMusicPlayer, MusicPlayer>();
            services.AddScoped<InventoryCore>();
			services.AddScoped<GameCore>();

			services.AddTransient<MainViewModel>();
            services.AddTransient<TableTopViewModel>();
            services.AddTransient<GameCreationViewModel>();

			ServiceProvider = services.BuildServiceProvider();
		}

	}
}
