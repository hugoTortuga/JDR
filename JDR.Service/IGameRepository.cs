using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IGameRepository {

		public void Save(Game obj);
		public void Delete(Game obj);
		public IList<Game> GetAll();

	}
}
