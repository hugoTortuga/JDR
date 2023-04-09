using JDR.Infra.Entities;
using Microsoft.EntityFrameworkCore;
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

	}
}
