using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class BaseRepository : IBaseRepository {

        public BaseRepository()
        {
            
        }

		public void Delete(object obj) {
			throw new NotImplementedException();
		}

		public void Save(object obj) {
			throw new NotImplementedException();
		}
	}
}
