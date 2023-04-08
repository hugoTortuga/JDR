using JDR.Model;

namespace JDR.Service {
	public interface IInventoryService {
		Task<InventoryItem> AddInventoryItemAsync(InventoryItem inventoryItem);
		Task DeleteInventoryItemAsync(int id);
		Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync();
		Task<InventoryItem> GetInventoryItemByIdAsync(int id);
		Task UpdateInventoryItemAsync(InventoryItem inventoryItem);
	}
}