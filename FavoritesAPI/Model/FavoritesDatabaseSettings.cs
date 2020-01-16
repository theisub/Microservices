using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesAPI.Model
{
    public class FavoritesDatabaseSettings : IFavoritesDatabaseSettings
    {
        public string FavoritesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IFavoritesDatabaseSettings
    {
        string FavoritesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }

}
