using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IGenericRepository<TPersistenceEntity> where TPersistenceEntity : class {
		Task<TPersistenceEntity> GetByIdAsync(int id);
		IList<TPersistenceEntity> GetAll();
		Task<TPersistenceEntity> AddAsync(TPersistenceEntity entity);
		Task UpdateAsync(TPersistenceEntity entity);
		Task DeleteAsync(int id);
	}
}
