using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusAPI.Model
{
    public class BusActions : IBusActions
    {
        protected readonly BusDbContext context;

        public BusActions(BusDbContext context)
        {
            this.context = context;
        }

        public async Task<Bus> AddBusAsync(Bus bus)
        {
            context.Buses.Add(bus);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Exception mess = ex.InnerException;
            }
            return bus;
        }

        public Task<Bus> GetBusAsync(long id)
        {
            return context.Buses.FirstOrDefaultAsync(p => p.Price == id);
            //return context.Buses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Bus>> GetAllBusesAsync()
        {
            return context.Buses.ToListAsync();

        }

        public async Task<List<Bus>> GetAllBusesByCompany(string companyName)
        {
            return await context.Buses.Where(a => a.BusCompany == companyName).ToListAsync();

        }

        public async Task<List<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null)
        {
            if (minPrice != null && maxPrice !=null)
            {

                return await context.Buses.Where(a => a.Price >= minPrice && a.Price <= maxPrice).ToListAsync();

            }
            else if (minPrice != null)
            {
                return await context.Buses.Where(a => a.Price >= maxPrice).ToListAsync();

            }
            else if (maxPrice != null)
            {
                return await context.Buses.Where(a => a.Price <= maxPrice).ToListAsync();
            }
            else
            {
                return await context.Buses.ToListAsync();

            }
        }

        public async Task<List<Bus>> GetAllBusesByRoute(string inCity, string outCity)
        {
            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).ToListAsync();
        }

        public async Task<List<Bus>> GetFastestBuses(string inCity, string outCity, int size = 10)
        {

            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.TravelTime).Take(size).ToListAsync();

        }
        public async Task<List<Bus>> GetCheapestBuses(string inCity, string outCity,int size = 10)
        {
            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.Price).Take(size).ToListAsync();
        }
    }
}
