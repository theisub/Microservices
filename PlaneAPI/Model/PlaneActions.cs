using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlaneAPI.Model;
using X.PagedList;

namespace PlaneAPI
{
    public class PlaneActions : IPlaneActions
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

        public async Task UpdatePlaneAsync(Plane plane)
        {

            context.Entry(await context.Planes.FirstOrDefaultAsync(x => x.Id == plane.Id)).CurrentValues.SetValues(plane);
            await context.SaveChangesAsync();
        }

        public async Task<Plane> DeletePlaneAsync(long id)
        {
            var plane = await context.Planes.FirstOrDefaultAsync(plane => plane.Id == id);
            if (plane == null)
            {
                return plane;
            }

            try
            {
                context.Planes.Remove(plane);
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
            //return context.Planes.FirstOrDefaultAsync(p => p.Price == id);
            return context.Planes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Plane>> GetAllPlanesAsync()
        {
            return context.Planes.ToListAsync();

        }

        public async Task<IEnumerable<Plane>> GetAllPlanesByCompany(string companyName, int pageNum = 1, int pageSize = 10)
        {
            return await context.Planes.Where(a => a.PlaneCompany == companyName).ToPagedListAsync(pageNum,pageSize);

        }

        public async Task<IEnumerable<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10)
        {
            if (minPrice != null && maxPrice != null)
            {

                return await context.Planes.Where(a => a.Price >= minPrice && a.Price <= maxPrice).ToPagedListAsync(pageNum, pageSize);

            }
            else if (minPrice != null)
            {
                return await context.Planes.Where(a => a.Price >= maxPrice).ToPagedListAsync(pageNum, pageSize);

            }
            else if (maxPrice != null)
            { 
                return await context.Planes.Where(a => a.Price <= maxPrice).ToPagedListAsync(pageNum, pageSize);
            }
            else
            {
                return await context.Planes.ToPagedListAsync(pageNum, pageSize);

            }
        }

        public async Task<IEnumerable<Plane>> GetAllPlanesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).ToPagedListAsync(pageNum, pageSize);
        }

        public async Task<IEnumerable<Plane>> GetFastestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {

            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.TravelTime).ToPagedListAsync(pageNum, pageSize);

        }
        public async Task<IEnumerable<Plane>> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            return await context.Planes.Where(a => a.InCity == inCity && a.OutCity == outCity).OrderBy(a => a.Price).ToPagedListAsync(pageNum, pageSize);
        }

    }
}
