using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class InventoryService : IInventoryService {

		private readonly IDynamicMapperService _dynamicMapperService;
		private readonly IGenericRepository<InventoryItemEntity> _inventoryItemRepository;

		public InventoryService(IDynamicMapperService dynamicMapperService, IGenericRepository<InventoryItemEntity> inventoryItemRepository) {
			_dynamicMapperService = dynamicMapperService;
			_inventoryItemRepository = inventoryItemRepository;
		}

		public async Task<InventoryItem> GetInventoryItemByIdAsync(int id) {
			var inventoryItemEntity = await _inventoryItemRepository.GetByIdAsync(id);
			return (InventoryItem)_dynamicMapperService.Map(inventoryItemEntity, typeof(InventoryItem));
		}

		public IEnumerable<InventoryItem> GetAllInventoryItemsAsync() {
			var inventoryItemEntities = _inventoryItemRepository.GetAll();
			return ((IEnumerable<InventoryItemEntity>)inventoryItemEntities).Select(x => _dynamicMapperService.Map(x, typeof(InventoryItem))).Cast<InventoryItem>().ToList();
		}

		public async Task<InventoryItem> AddInventoryItemAsync(InventoryItem inventoryItem) {
			var inventoryItemEntity = _dynamicMapperService.Map(inventoryItem, typeof(InventoryItemEntity));
			var addedInventoryItemEntity = await _inventoryItemRepository.AddAsync((InventoryItemEntity)inventoryItemEntity);
			return (InventoryItem)_dynamicMapperService.Map(addedInventoryItemEntity, typeof(InventoryItem));
		}

		public async Task UpdateInventoryItemAsync(InventoryItem inventoryItem) {
			var inventoryItemEntity = _dynamicMapperService.Map(inventoryItem, typeof(InventoryItemEntity));
			await _inventoryItemRepository.UpdateAsync((InventoryItemEntity)inventoryItemEntity);
		}

		public async Task DeleteInventoryItemAsync(int id) {
			await _inventoryItemRepository.DeleteAsync(id);
		}

		Task<IEnumerable<InventoryItem>> IInventoryService.GetAllInventoryItemsAsync() {
			throw new NotImplementedException();
		}
	}
}
