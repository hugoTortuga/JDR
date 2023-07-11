using JDR.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class AppDbContext : DbContext {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<InventoryItem> InventoryItems { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Scene> Scenes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Scene>(scene => {
				scene.Property(p => p.Obstacles)
				.HasConversion(
					 v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
					v => JsonConvert.DeserializeObject<List<Obstacle>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
				.HasColumnType("obstacles");
                scene.Property(p => p.PlayerSpawnPoint)
                .HasConversion(
                     v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Point>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnType("playerSpawnPoint");
            });

            modelBuilder.Entity<Character>(charac => {
                charac.ToTable("character");
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

            modelBuilder.Entity<Character>()
            .HasOne(i => i.Illustration)
            .WithMany()
            .HasForeignKey("idIllustrationCharacter");
            modelBuilder.Entity<Character>()
            .HasOne(i => i.Token)
            .WithMany()
            .HasForeignKey("idIllustrationToken");
            modelBuilder.Entity<Character>()
            .HasOne(i => i.Race)
            .WithMany()
            .HasForeignKey("idRace");

            modelBuilder.Entity<Race>().ToTable("race");

            modelBuilder.Entity<Music>().ToTable("music");
            modelBuilder.Entity<Music>().Ignore(m => m.Content);

            modelBuilder.Entity<Scene>().ToTable("scene");
            modelBuilder.Entity<Scene>()
			.HasMany(s => s.Musics)
			.WithOne()
			.HasForeignKey("idScene");
            modelBuilder.Entity<Scene>()
			.Property<Guid>("idGame");
            modelBuilder.Entity<Scene>().Ignore(b => b.Background);

            modelBuilder.Entity<InventoryItem>().ToTable("inventory_item");
            modelBuilder.Entity<InventoryItem>()
            .HasOne(i => i.Illustration)
            .WithMany()
            .HasForeignKey("idIllustration");

            modelBuilder.Entity<Game>().ToTable("game");
            modelBuilder.Entity<Game>()
			.HasMany(g => g.Scenes)
			.WithOne()
			.HasForeignKey("idGame")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
		}

	}
}
