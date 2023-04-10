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

        public GameCore(IMainRepository repo)
        {
            _Repository = repo;
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
	}
}
