using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IDynamicMapperService {
		object Map(object source, Type destinationType);
	}
}
