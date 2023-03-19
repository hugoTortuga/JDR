using JDR.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class DbContextJDR : DbContext {

		public DbSet<Game> GameMap { get; set; }
		public DbSet<Scene> SceneMap { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseMySql("server=localhost;database=myDatabase;user=myUser;password=myPassword",
				new MySqlServerVersion(new Version(8, 0, 31)));
		}

	}
}
