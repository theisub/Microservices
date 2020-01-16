using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoritesAPI.Model;

namespace GatewayAPI.FavoritesClient
{
    public interface IFavoritesHttpClient
    {
        Task<List<Favorites>> GetAllAsync();
        Task<Favorites> GetAsync(long id);
        Task<Favorites> PostAsync(Favorites favorite);
        Task<Favorites> PutAsync(string id, Favorites favorite);
        Task<Favorites> DeleteAsync(string id);


    }
}
