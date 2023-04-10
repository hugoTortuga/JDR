using AutoMapper;
using AutoMapper.QueryableExtensions;
using JDR.Infra.Entities;
using JDR.Model;
using JDR.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class MainRepository : IMainRepository {

		private readonly AppDbContext _DbContext;
		public MainRepository(AppDbContext dbContext) {
			_DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public Game GetLastGame() {
			//TODO not done yet
			return new Game();
		}

		public async Task SaveGame(Game game) {
			await _DbContext.Games.AddAsync(new GameEntity {
				Name = game.Name,
				MaxPlayer = game.MaxPlayer,
				Scenes = game.Scenes.Select(s => new SceneEntity {
					BackgroundImage = s.Background?.Name + s.Background?.Extension,
					Name = s.Name,
					Obstacles = s.Obstacles
				}).ToList()
			});
			await _DbContext.SaveChangesAsync();
		}

		public async Task SaveItem(InventoryItem item) {
			await _DbContext.InventoryItems.AddAsync(new InventoryItemEntity { Name = item.Name });
			await _DbContext.SaveChangesAsync();
		}

	}
}
