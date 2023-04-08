using AutoMapper;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class MapperService : IMapperService {
		private readonly IMapper _mapper;

		public MapperService(IMapper mapper) {
			_mapper = mapper;
		}

		public TDestination Map<TSource, TDestination>(TSource source) {
			return _mapper.Map<TSource, TDestination>(source);
		}
	}
}
