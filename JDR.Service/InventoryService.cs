using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public class InventoryService {

		private readonly IInventoryItemRepository _repository;

		public InventoryService(IInventoryItemRepository repository) {
			_repository = repository;
		}

		public async Task AddProduct(InventoryItem inventoryObject) {
			await _repository.AddProduct(inventoryObject);
		}

	}
}
