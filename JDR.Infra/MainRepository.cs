using AutoMapper;
using AutoMapper.QueryableExtensions;
using JDR.Core;
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
		private readonly IImageUploader _ImageManager;
		public MainRepository(AppDbContext dbContext, IImageUploader imageUploader) {
			_DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_ImageManager = imageUploader;

        }

        public List<Scene> GetAllScenes()
        {
			return _DbContext.Scenes.Select(s => new Scene(s.Name)
			{
				Obstacles = s.Obstacles,
				Background = _ImageManager.Get(s.BackgroundImage)
			}).ToList();
        }

        public Game GetLastGame() {
			var lastGame = _DbContext.Games.OrderBy(g => g.Name).Last();
			if (lastGame == null) return new Game();
			return lastGame.ToGame(_ImageManager);
		}

        public IList<Game> GetYourGames()
        {
			var gameEntities = _DbContext.Games
				.Include(g => g.Scenes)
				.ToList();
			if (gameEntities == null) return new List<Game>();

			return gameEntities.Select(g => g.ToGame(_ImageManager)).ToList();
        }

        public async Task SaveGame(Game game) {
			await _DbContext.Games.AddAsync(new GameEntity {
				Name = game.Name,
				MaxPlayer = game.MaxPlayer,
				Scenes = game.Scenes.Select(s => SceneEntity.ToSceneEntity(s)).ToList()
			});
			await _DbContext.SaveChangesAsync();
		}

		public async Task SaveItem(InventoryItem item) {
			await _DbContext.InventoryItems.AddAsync(new InventoryItemEntity { Name = item.Name });
			await _DbContext.SaveChangesAsync();
		}

	}
}
