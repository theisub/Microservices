using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneAPI.Model
{
    public class PlaneDtoMappingProfile : Profile
    {
        public PlaneDtoMappingProfile()
        {
            CreateMap<Plane, PlaneDto>();
            CreateMap<PlaneDto, Plane>();

      
        }
    }
}
