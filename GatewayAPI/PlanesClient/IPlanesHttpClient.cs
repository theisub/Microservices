using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlaneAPI.Model;
namespace GatewayAPI.PlanesClient
{
    public interface IPlanesHttpClient
    {
        Task<string> GetAsync(long id);
        Task<Plane> PostAsync(Plane bus);

        Task<List<Plane>> GetAllPlanesByCompany(string companyName);
        Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null);
        Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity);

        Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int size = 10);
        Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int size = 10);

    }
}
