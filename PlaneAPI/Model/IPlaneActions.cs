using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaneAPI.Model
{
    public interface IPlaneActions
    {
        Task<Plane> AddPlaneAsync(Plane plane);
        Task<Plane> GetPlaneAsync(long id);
        Task<List<Plane>> GetAllPlanesAsync();

        Task<List<Plane>> GetAllPlanesByCompany(string companyName);
        Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null);
        Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity);

        Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int size = 10);
        Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int size = 10);
    }
}
