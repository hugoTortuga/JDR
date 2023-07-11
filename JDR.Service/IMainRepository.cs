using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IMainRepository {
        IEnumerable<InventoryItem> GetAllItems();
        IEnumerable<Character> GetAllCharacters();
        List<Scene> GetAllScenes();
        Game GetLastGame();
        IList<Game> GetYourGames();
        void SaveCharacter(Character character);
        Task SaveGame(Game game);
		Task SaveItem(InventoryItem item);
        IEnumerable<Race> GetAllRaces();
    }
}
