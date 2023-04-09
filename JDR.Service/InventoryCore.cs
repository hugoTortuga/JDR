using JDR.Model;

namespace JDR.Service {
	public class InventoryCore {

        private IMainRepository _Repository;

        public InventoryCore(IMainRepository repository)
        {
			_Repository = repository;
   		}

        public async Task AddItem(InventoryItem item) {
            await _Repository.SaveItem(item);
        }

    }
}