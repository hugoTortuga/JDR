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
        public DbSet<CharacterEntity> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<SceneEntity>(scene => {
				scene.Property(p => p.Obstacles)
				.HasConversion(
					 v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
					v => JsonConvert.DeserializeObject<List<Obstacle>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
				.HasColumnType("obstacles");
			});

            modelBuilder.Entity<CharacterEntity>(charac => {
                charac.Property(c => c.Skills)
                .HasConversion(
                     v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Skills>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnType("skills");
                charac.Property(c => c.Inventory)
                .HasConversion(
                     v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Inventory>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnType("inventory");
                charac.Property(c => c.Spells)
                .HasConversion(
                     v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<IList<Spell>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnType("spells");
            });
            modelBuilder.Entity<CharacterEntity>()
            .HasOne(i => i.IllustrationTokenEntity)
            .WithMany()
            .HasForeignKey("idIllustrationToken");
            modelBuilder.Entity<CharacterEntity>()
            .HasOne(i => i.IllustationEntity)
            .WithMany()
            .HasForeignKey("idIllustrationCharacter");

            modelBuilder.Entity<SceneEntity>()
			.HasMany(s => s.Musics)
			.WithOne()
			.HasForeignKey("idScene");

            modelBuilder.Entity<SceneEntity>()
			.Property<Guid>("idGame");

			modelBuilder.Entity<InventoryItemEntity>()
            .HasOne(i => i.IllustrationEntity)
            .WithMany()
            .HasForeignKey("idIllustration");

            modelBuilder.Entity<GameEntity>()
			.HasMany(g => g.Scenes)
			.WithOne()
			.HasForeignKey("idGame")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
		}

	}
}
