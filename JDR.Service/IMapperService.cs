using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Service {
	public interface IMapperService {
		TDestination Map<TSource, TDestination>(TSource source);
	}
}
