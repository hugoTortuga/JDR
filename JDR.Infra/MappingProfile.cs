using AutoMapper;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Character, Character>();
            CreateMap<Character, Character>();
        }
    }
}
