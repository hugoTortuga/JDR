﻿using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core {
	public class GameCore {

		private IMainRepository _Repository;
		private IImageStorage _ImageUploader;

		public GameCore(IMainRepository repo, IImageStorage imageUploader) {
			_Repository = repo;
			_ImageUploader = imageUploader;
		}

        public void AddCharacterToGame(Character character)
        {
			ArgumentNullException.ThrowIfNull(character);
			ArgumentNullException.ThrowIfNullOrEmpty(character.Name);
			_Repository.SaveCharacter(character);
        }

        public IEnumerable<InventoryItem> GetAllItems()
        {
			return _Repository.GetAllItems();
        }

        public IEnumerable<Race> GetAllRaces()
        {
			return new List<Race> 
			{ 
				Race.Human,
				Race.Elve,
				Race.Dwarf,
				Race.HumanElve,
				Race.HumanDwarf,
				Race.DwarfElve
			};
        }

        public IList<Game> GetAvailableGames()
        {
			return _Repository.GetYourGames();
        }

        public List<Scene> GetAvailableScenes()
        {
			return _Repository.GetAllScenes();
        }

        public Game GetLastGame() {
			return _Repository.GetLastGame();
		}

		public async Task<bool> SaveGame(Game game) {
			try {
				await _Repository.SaveGame(game);
				return true;
			}
			catch (Exception) {
				//TODO i dont know how i'll handle exception for now
				return false;
			}

		}

		public async Task<bool> SaveScene(Scene currentScene) {
			try {
				if (currentScene?.Background != null
                    && currentScene.Background?.Content != null
					&& !string.IsNullOrEmpty(currentScene.Background.Name)
					&& !string.IsNullOrEmpty(currentScene.Background.Extension))
					await _ImageUploader.Upload(currentScene.Background.Content, currentScene.Background.Name + currentScene.Background.Extension);
				return true;
			}
			catch (Exception) {
				return false;
			}
		}
	}
}
