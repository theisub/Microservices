using System;
using PlaneAPI.Model;
using FavoritesAPI.Model;
using GatewayAPI.PlanesClient;
using GatewayAPI.FavoritesClient;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace GatewayAPI.Controllers
{
    class FavoritesAndPlane 
    {
        public Favorites favorites { get; }
        public Plane plane { get; }

        public FavoritesAndPlane(Favorites favorites, Plane plane)
        {
            this.plane = plane;
            this.favorites = favorites;
        }
    }

    public class QueueManager
    {
        private ConcurrentQueue<FavoritesAndPlane> favoritesQueue = new ConcurrentQueue<FavoritesAndPlane>();
        private ConcurrentQueue<Plane> planeQueue = new ConcurrentQueue<Plane>();

        private const int pendDelayMs = 2000;

        private IFavoritesHttpClient favoritesActions;
        private IPlanesHttpClient planeService;

        public QueueManager(IFavoritesHttpClient res, IPlanesHttpClient plane)
        {
            favoritesActions = res;
            planeService = plane;
        }

        public void AddFavoriteRequest(Favorites favorites, Plane plane)
        {
            favoritesQueue.Enqueue(new FavoritesAndPlane(favorites, plane));
            if (favoritesQueue.Count == 1)
            {
                Task.Run(startFavoritesAdd);
            }
        }

        public void AddPlaneRequest(Plane data)
        {
            planeQueue.Enqueue(data);
            if (planeQueue.Count == 1)
            {
                Task.Run(startPlaneAdd);
            }
        }

        private async Task startFavoritesAdd()
        {
            FavoritesAndPlane data;
            while (favoritesQueue.TryPeek(out data))
            {
                try
                {
                    await favoritesActions.PostAsync(data.favorites);
                    favoritesQueue.TryDequeue(out data);
                }               
                catch (Exception e)
                {
                    Thread.Sleep(pendDelayMs);
                }
            }
        }

        private async Task startPlaneAdd()
        {
            Plane data;
            while (planeQueue.TryPeek(out data))
            {
                try
                {
                    await planeService.PostAsync(data);
                    planeQueue.TryDequeue(out data);
                }
                catch
                {
                    Thread.Sleep(pendDelayMs);
                }
            }
        }
    }
}
