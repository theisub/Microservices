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

    }
}
