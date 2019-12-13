using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlaneAPI.Model;
namespace GatewayAPI.PlanesClient
{
    public interface IPlanesHttpClient
    {
        Task<List<Plane>> GetAsync();
        Task<Plane> GetIdAsync(long id);
        Task<Plane> PostAsync(Plane plane);
        Task<Plane> PutAsync(long id,Plane plane);
        Task<Plane> DeleteAsync(long id);

        Task<List<Plane>> GetAllPlanesByCompany(string companyName, int pageNum = 1, int pageSize = 10);
        Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10);
        Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

        Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10);
        Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10);

    }
}
