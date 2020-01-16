using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusAPI.Model;
using FavoritesAPI.Model;
using PlaneAPI.Model;

namespace GatewayAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<List<Bus>, Favorites>()
                .ForMember(favorite => favorite.BusesRoute, buses => buses.MapFrom(arg => arg));
            CreateMap<List<Plane>, Favorites>()
                .ForMember(favorite => favorite.PlanesRoute, planes => planes.MapFrom(arg => arg));

            CreateMap<Route, Favorites>();

            CreateMap<Bus, IEnumerable<Route>>();
            //CreateMap<IEnumerable<Bus>, IEnumerable<Route>>();
            CreateMap<Bus, Route>()
                .ForMember(route => route.CompanyName, bus => bus.MapFrom(arg => arg.BusCompany))
                .ForMember(route => route.travelTime, bus => bus.MapFrom(arg => arg.TravelTime));

            CreateMap<Plane, Route>()
               .ForMember(route => route.CompanyName, plane => plane.MapFrom(arg => arg.PlaneCompany))
               .ForMember(route => route.travelTime, plane => plane.MapFrom(arg => arg.TravelTime));

            //CreateMap<Bus, IEnumerable<Route>();
        }
    }
}
