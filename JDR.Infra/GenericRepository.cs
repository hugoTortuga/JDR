using AutoMapper;
using AutoMapper.QueryableExtensions;
using JDR.Model;
using JDR.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class GenericRepository<TPersistenceEntity> : IGenericRepository<TPersistenceEntity>
	where TPersistenceEntity : class {
		private readonly DbContext _dbContext;
		private readonly DbSet<TPersistenceEntity> _dbSet;

		public GenericRepository(AppDbContext dbContext) {
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_dbSet = dbContext.Set<TPersistenceEntity>();
		}

		public async Task<TPersistenceEntity> GetByIdAsync(int id) {
			return await _dbSet.FindAsync(id);
		}

		public IList<TPersistenceEntity> GetAll() {
			return _dbSet.ToList();
		}

		public async Task<TPersistenceEntity> AddAsync(TPersistenceEntity entity) {
			await _dbSet.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task UpdateAsync(TPersistenceEntity entity) {
			_dbSet.Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id) {
			var entity = await _dbSet.FindAsync(id);
			if (entity == null) return;

			_dbSet.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}
