using AutoMapper;
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

namespace JDR.Infra
{
    public class MainRepository : IMainRepository
    {

        private readonly AppDbContext _DbContext;
        private readonly IImageStorage _ImageManager;
        private readonly IMusicStorage _MusicManager;
        private readonly IMapper _Mapper;

        public MainRepository(AppDbContext dbContext, IImageStorage imageUploader, IMusicStorage musicStorage, IMapper mapper)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _ImageManager = imageUploader;
            _MusicManager = musicStorage;
            _Mapper = mapper;
        }

        public IEnumerable<Character> GetAllCharacters()
        {
            return _DbContext.Characters.Select(c => c.ToCharacter(_Mapper)).ToList();
        }

        public IEnumerable<InventoryItem> GetAllItems()
        {
            return _DbContext.InventoryItems.Select(i => i.ToInventoryItem()).ToList();
        }

        public List<Scene> GetAllScenes()
        {
            return _DbContext.Scenes.Select(s => new Scene(s.Name)
            {
                Obstacles = s.Obstacles,
                Background = _ImageManager.Get(s.BackgroundImage)
            }).ToList();
        }

        public Game GetLastGame()
        {
            var lastGame = _DbContext.Games.OrderBy(g => g.Name).Last();
            if (lastGame == null) return new Game();
            return lastGame.ToGame(_ImageManager, _MusicManager);
        }

        public IList<Game> GetYourGames()
        {
            var gameEntities = _DbContext.Games
                .Include(g => g.Scenes)
                .ThenInclude(s => s.Musics)
                .ToList();
            if (gameEntities == null) return new List<Game>();

            return gameEntities.Select(g => g.ToGame(_ImageManager, _MusicManager)).ToList();
        }

        public async void SaveCharacter(Character character)
        {
            if (character.Id == Guid.Empty || character.Id == null || !_DbContext.Characters.Any(c => c.Id == character.Id))
                await _DbContext.Characters.AddAsync(CharacterEntity.ToCharacterEntity(character));
            else
            {
                var characterEntity = _DbContext.Characters.FirstOrDefault(c => c.Id == character.Id);
                characterEntity.UpdateAttributesFrom(character);
            }
            await _DbContext.SaveChangesAsync();
        }

        public async Task SaveGame(Game game)
        {
            await _DbContext.Games.AddAsync(new GameEntity
            {
                Name = game.Name,
                MaxPlayer = game.MaxPlayer,
                Scenes = game.Scenes.Select(s => SceneEntity.ToSceneEntity(s)).ToList()
            });
            await _DbContext.SaveChangesAsync();
        }

        public async Task SaveItem(InventoryItem item)
        {
            await _DbContext.InventoryItems.AddAsync(new InventoryItemEntity { Name = item.Name });
            await _DbContext.SaveChangesAsync();
        }

    }
}
