using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core {
	public class GameCore {

		private IMainRepository _Repository;
		private IImageUploader _ImageUploader;

		public GameCore(IMainRepository repo, IImageUploader imageUploader) {
			_Repository = repo;
			_ImageUploader = imageUploader;
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
				if (currentScene.Background?.Uri != null)
					await _ImageUploader.Upload(currentScene.Background.Uri.LocalPath);
				return true;
			}
			catch (Exception) {
				return false;
			}
		}
	}
}
