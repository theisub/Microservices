using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusAPI.Model;

namespace GatewayAPI.BusesClient
{
    public interface IBusesHttpClient
    {
        Task<string> GetAsync(long id);
        Task<Bus> PostAsync(Bus bus);

        Task<List<Bus>> GetAllBusesByCompany(string company);
        Task<List<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null);
        Task<List<Bus>> GetAllBusesByRoute(string inCity, string outCity);

        Task<List<Bus>> GetFastestBuses(string inCity, string outCity, int size = 10);
        Task<List<Bus>> GetCheapestBuses(string inCity, string outCity, int size = 10);

    }
}
