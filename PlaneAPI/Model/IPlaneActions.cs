using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaneAPI.Model
{
    public interface IPlaneActions
    {
        Task<Plane> AddPlaneAsync(Plane plane);
        Task<Plane> GetPlaneAsync(long id);
        Task UpdatePlaneAsync(Plane plane);
        Task<Plane> DeletePlaneAsync(long id);

        Task<List<Plane>> GetAllPlanesAsync();

        Task<IEnumerable<Plane>> GetAllPlanesByCompany(string companyName, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Plane>> GetAllPlanesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

        Task<IEnumerable<Plane>> GetFastestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10);
        Task<IEnumerable<Plane>> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10);
    }
}
