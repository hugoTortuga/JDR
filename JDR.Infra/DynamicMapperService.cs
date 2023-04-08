using AutoMapper;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class DynamicMapperService : IDynamicMapperService {
		private readonly IMapper _mapper;

		public DynamicMapperService(IMapper mapper) {
			_mapper = mapper;
		}

		public object Map(object source, Type destinationType) {
			return _mapper.Map(source, source.GetType(), destinationType);
		}
	}
}
