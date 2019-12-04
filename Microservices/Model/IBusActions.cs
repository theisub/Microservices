using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusAPI.Model
{
    public interface IBusActions
    {
        Task<Bus> AddBusAsync(Bus bus);
        Task<Bus> GetBusAsync(long id);
        Task<List<Bus>> GetAllBusesAsync();

        Task<List<Bus>> GetAllBusesByCompany(string companyName);
        Task<List<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice=null);
        Task<List<Bus>> GetAllBusesByRoute(string inCity, string outCity);

        Task<List<Bus>> GetFastestBuses(string inCity, string outCity,int size = 10);
        Task<List<Bus>> GetCheapestBuses(string inCity, string outCity, int size = 10);


    }
}
