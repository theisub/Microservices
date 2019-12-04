using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusAPI.Model
{
    public interface IBusActions
    {
        Task<Bus> AddBusAsync(Bus entity);
        Task<Bus> GetBusAsync(int id);
        Task<List<Bus>> GetAllBusesAsync();
    }
}
