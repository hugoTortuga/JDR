using AutoMapper;
using AutoMapper.QueryableExtensions;
using JDR.Infra.Entities;
using JDR.Model;
using JDR.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class MainRepository : IMainRepository {

		private readonly AppDbContext _dbContext;
		public MainRepository(AppDbContext dbContext) {
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task SaveItem(InventoryItem item) {
			await _dbContext.InventoryItems.AddAsync(new InventoryItemEntity { Name = item.Name});
			await _dbContext.SaveChangesAsync();
		}

	}
}
