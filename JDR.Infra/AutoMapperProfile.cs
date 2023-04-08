using AutoMapper;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class AutoMapperProfile : Profile {
		public AutoMapperProfile() {
			CreateMap<InventoryItem, InventoryItemEntity>().ReverseMap();
		}
	}
}
