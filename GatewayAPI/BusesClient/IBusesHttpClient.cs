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
        Task<Bus> PutAsync(long id, Bus bus);
        Task<Bus> DeleteAsync(long id);

        Task<List<Bus>> GetAllBusesByCompany(string company, int pageNum = 1, int pageSize = 10);
        Task<List<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10);
        Task<List<Bus>> GetAllBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

        Task<List<Bus>> GetFastestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10);
        Task<List<Bus>> GetCheapestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

    }
}
