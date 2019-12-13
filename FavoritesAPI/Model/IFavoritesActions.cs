using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesAPI.Model
{
    public interface IFavoritesActions
    {
        Task<List<Favorites>> Get();
        Task<Favorites> Get(string id);
        Task<Favorites> Create(Favorites favorite);
        void Update(string id, Favorites favoriteIn);
        void Remove(Favorites favoriteIn);
        void Remove(string id);



    }
}
