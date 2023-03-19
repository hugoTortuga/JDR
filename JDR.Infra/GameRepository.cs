using JDR.Model;
using JDR.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class GameRepository : IGameRepository {

		private readonly DbContextJDR DBContextJDR;
        public GameRepository(DbContextJDR dbContextJDR)
        {
			DBContextJDR = dbContextJDR;
		}

		public void Delete(Game game) {
			//TODO
		}

		public void Save(Game game) {
			DBContextJDR.GameMap.Add(game);
			DBContextJDR.SaveChanges();
		}

		public IList<Game> GetAll() {
			return DBContextJDR.GameMap.ToList();
		}
	}
}
