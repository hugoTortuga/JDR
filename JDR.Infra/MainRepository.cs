using AutoMapper;
using JDR.Core;
using JDR.Core.Exceptions;
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
            return _DbContext.Characters.ToList();
        }

        public IEnumerable<InventoryItem> GetAllItems()
        {
            return _DbContext.InventoryItems.ToList();
        }

        public IEnumerable<Race> GetAllRaces()
        {
            return _DbContext.Races.ToList();
        }

        public List<Scene> GetAllScenes()
        {
            return _DbContext.Scenes.Select(s => new Scene(s.Name)
            {
                Obstacles = s.Obstacles,
                Background = _ImageManager.Get(s.BackgroundImage),
                PlayerSpawnPoint = s.PlayerSpawnPoint
            }).ToList();
        }

        public Character GetCharacterById(Guid id)
        {
            return _DbContext.Characters.FirstOrDefault(c => c.Id == id) ?? throw new DataNotFoundException("Character not found");
        }

        public Game GetLastGame()
        {
            return _DbContext.Games.OrderBy(g => g.Name).Last();
        }

        public IList<Game> GetYourGames()
        {
            var games = _DbContext.Games
                .Include(g => g.Scenes)
                .ThenInclude(s => s.Musics)
                .ToList();
            return games;
        }

        public async void SaveCharacter(Character character)
        {
            if (character.Id == Guid.Empty || character.Id == null || !_DbContext.Characters.Any(c => c.Id == character.Id))
                await _DbContext.Characters.AddAsync(character);
            await _DbContext.SaveChangesAsync();
        }

        public async Task SaveGame(Game game)
        {
            await _DbContext.Games.AddAsync(game);
            await _DbContext.SaveChangesAsync();
        }

        public async Task SaveItem(InventoryItem item)
        {
            await _DbContext.InventoryItems.AddAsync(item);
            await _DbContext.SaveChangesAsync();
        }

    }
}
