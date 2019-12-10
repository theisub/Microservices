using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task UpdateBusAsync(Bus bus)
        {

            context.Entry(await context.Buses.FirstOrDefaultAsync(x => x.Id == bus.Id)).CurrentValues.SetValues(bus);
            await context.SaveChangesAsync();
        }

        public async Task<Bus> DeleteBusAsync(long id)
        {
            var plane = await context.Buses.FirstOrDefaultAsync(plane => plane.Id == id);
            if (plane == null)
            {
                return plane;
            }

            try
            {
                context.Buses.Remove(plane);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Exception mess = ex.InnerException;

            }
            return plane;
        }

        public Task<Bus> GetBusAsync(long id)
        {
            //return context.Buses.FirstOrDefaultAsync(p => p.Price == id);
            return context.Buses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Bus>> GetAllBusesAsync()
        {
            return context.Buses.ToListAsync();

        }

        public async Task<IEnumerable<Bus>> GetAllBusesByCompany(string companyName, int pageNum = 1, int pageSize = 10)
        {
            return await context.Buses.Where(a => a.BusCompany == companyName).ToPagedListAsync(pageNum, pageSize);

        }

        public async Task<IEnumerable<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10)
        {
            if (minPrice != null && maxPrice !=null)
            {

                return await context.Buses.Where(a => a.Price >= minPrice && a.Price <= maxPrice).ToPagedListAsync(pageNum, pageSize);

            }
            else if (minPrice != null)
            {
                return await context.Buses.Where(a => a.Price >= maxPrice).ToPagedListAsync(pageNum, pageSize);

            }
            else if (maxPrice != null)
            {
                return await context.Buses.Where(a => a.Price <= maxPrice).ToPagedListAsync(pageNum, pageSize);
            }
            else
            {
                return await context.Buses.ToPagedListAsync(pageNum, pageSize);

            }
        }

        public async Task<IEnumerable<Bus>> GetAllBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).ToPagedListAsync(pageNum, pageSize);
        }

        public async Task<IEnumerable<Bus>> GetFastestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {

            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).ToPagedListAsync(pageNum, pageSize);

        }
        public async Task<IEnumerable<Bus>> GetCheapestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            return await context.Buses.Where(a => a.InCity == inCity && a.OutCity == outCity).ToPagedListAsync(pageNum, pageSize);
        }
    }
}
