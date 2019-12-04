using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusAPI.Model
{
    public class BusDtoMappingProfile : Profile
    {
        public BusDtoMappingProfile()
        {
            CreateMap<Bus, BusDto>();
            CreateMap<BusDto, Bus>();

      
        }
    }
}
