using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core {
	public class GameCore {

        private IMainRepository _Repository;

        public GameCore(IMainRepository repo)
        {
            _Repository = repo;
		}

        public Game GetLastGame() {
            return _Repository.GetLastGame();
        }

    }
}
