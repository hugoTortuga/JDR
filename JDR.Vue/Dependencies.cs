using JDR.Infra;
using JDR.Vue.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using AutoMapper;
using JDR.Service;

namespace JDR.Vue {
	public static class Dependencies {

		public static ServiceProvider ConfigureServices() {


			var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

			var services = new ServiceCollection();

			string connectionString = configuration.GetConnectionString("MySqlConnection");

			// Ajoutez la configuration du DbContext
			services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
			);

			// Enregistrez les services et les dépôts
			services.AddAutoMapper(typeof(AutoMapperProfile));
			services.AddScoped<IDynamicMapperService, DynamicMapperService>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<InventoryService>();


			// Enregistrez les ViewModels
			services.AddTransient<MainViewModel>();

			return services.BuildServiceProvider();
		}

	}
}
