using System;
using System.Collections.Generic;
using System.Linq;
using FavoritesAPI.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FavoritesAPI.Services
{
    public class FavoritesActions : IFavoritesActions
    {
        private readonly IMongoCollection<Favorites> favorites;

        public FavoritesActions(IFavoritesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            favorites = database.GetCollection<Favorites>(settings.FavoritesCollectionName);
        }

        public async Task<List<Favorites>> Get() =>
            await favorites.Find(favorites => true).ToListAsync();

        public async Task<Favorites> Get(string id) =>
            await favorites.Find<Favorites>(favorite => favorite.Id == id).FirstOrDefaultAsync();

        public async Task<Favorites> Create(Favorites favorite)
        {
            await favorites.InsertOneAsync(favorite);
            return favorite;
        }

        public async void Update(string id, Favorites favoriteIn) =>
            await favorites.ReplaceOneAsync(favorite => favorite.Id == id, favoriteIn);
    

        public async void Remove(Favorites favoriteIn) =>
            await favorites.DeleteOneAsync(favorite => favorite.Id == favoriteIn.Id);

        public async void Remove(string id) =>
            await favorites.DeleteOneAsync(favorite => favorite.Id == id);
    }
}

