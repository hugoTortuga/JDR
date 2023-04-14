using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IMainRepository {
        List<Scene> GetAllScenes();
        Game GetLastGame();
        IList<Game> GetYourGames();
        Task SaveGame(Game game);
		Task SaveItem(InventoryItem item);

	}
}
