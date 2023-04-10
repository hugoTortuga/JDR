using JDR.Infra.Entities;
using JDR.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class AppDbContext : DbContext {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<InventoryItemEntity> InventoryItems { get; set; }
		public DbSet<GameEntity> Games { get; set; }
		public DbSet<SceneEntity> Scenes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<SceneEntity>(scene => {
				scene.Property(p => p.Obstacles)
				.HasConversion(
					 v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
					v => JsonConvert.DeserializeObject<List<Obstacle>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
				.HasColumnType("obstacles");
			});
		}

	}
}
