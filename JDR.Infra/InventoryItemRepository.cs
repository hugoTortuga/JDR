using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class InventoryItemRepository : IInventoryItemRepository {

		private readonly AppDbContext _context;

		public InventoryItemRepository(AppDbContext context) {
			_context = context;
		}

		public async Task AddProduct(InventoryItem inventoryObject) {
			try {
				var productEntity = new InventoryItemEntity {
					Name = inventoryObject.Name
				};

				_context.Products.Add(productEntity);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex) {

				throw;
			}

		}

	}
}
