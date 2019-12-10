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
        Task UpdateBusAsync(Bus bus);
        Task<Bus> DeleteBusAsync(long id);
        Task<List<Bus>> GetAllBusesAsync();

        Task<IEnumerable<Bus>> GetAllBusesByCompany(string companyName, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice=null, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Bus>> GetAllBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

        Task<IEnumerable<Bus>> GetFastestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Bus>> GetCheapestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10);


    }
}
