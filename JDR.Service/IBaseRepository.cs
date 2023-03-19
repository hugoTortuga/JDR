using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IBaseRepository {

		public void Save(Object obj);
		public void Delete(Object obj);

	}
}
