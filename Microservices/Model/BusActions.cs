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

        public async Task<Bus> AddBusAsync(Bus entity)
        {
            context.Buses.Add(entity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Exception mess = ex.InnerException;
            }
            return entity;
        }

        public Task<Bus> GetBusAsync(int id)
        {
            return context.Buses.FirstOrDefaultAsync(p => p.Price == id);
            //return context.Buses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Bus>> GetAllBusesAsync()
        {

            return context.Buses.ToListAsync();

        }
    }
}
