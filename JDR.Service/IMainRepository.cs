using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IMainRepository {
		Game GetLastGame();
		Task SaveItem(InventoryItem item);

	}
}
