using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlaneAPI.Model;

namespace PlaneAPI
{
    public class PlaneActions
    {
        protected readonly PlaneDbContext context;

        public PlaneActions(PlaneDbContext context)
        {
            this.context = context;

        }

        public async Task<Plane> AddPlaneAsync(Plane plane)
        {
            context.Planes.Add(plane);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Exception mess = ex.InnerException;
            }
            return plane;
        }

        public Task<Plane> GetPlaneAsync(long id)
        {
            return context.Planes.FirstOrDefaultAsync(p => p.Price == id);
            //return context.Buses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Plane>> GetAllPlanesAsync()
        {
            return context.Planes.ToListAsync();

        }

        public async Task<List<Plane>> GetAllPlanesByCompany(string companyName)
        {
            return await context.Planes.Where(a => a.PlaneCompany == companyName).ToListAsync();

        }

        public async Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null)
        {
            if (minPrice != null && maxPrice != null)
            {

                return await context.Planes.Where(a => a.Price >= minPrice && a.Price <= maxPrice).ToListAsync();

            }
            else if (minPrice != null)
            {
                return await context.Planes.Where(a => a.Price >= maxPrice).ToListAsync();

            }
            else if (maxPrice != null)
            { 
                return await context.Planes.Where(a => a.Price <= maxPrice).ToListAsync();
            }
            else
            {
                return await context.Planes.ToListAsync();

            }
        }

        public async Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity)
        {
            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).ToListAsync();
        }

        public async Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int size = 10)
        {

            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.TravelTime).Take(size).ToListAsync();

        }
        public async Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int size = 10)
        {
            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.Price).Take(size).ToListAsync();
        }

    }
}
