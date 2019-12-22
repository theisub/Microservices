using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FavoritesAPI.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.FavoritesClient
{
    public class FavoritesHttpClient : IFavoritesHttpClient
    {
        protected readonly HttpClient client;

        public FavoritesHttpClient(HttpClient client) : base()
        {
            this.client = client;
        }



        public async Task<List<Favorites>> GetAllAsync()
        {
            HttpResponseMessage response;
            string url = $"/api/favorites/";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);


            List<Favorites> result;

            var resultBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Favorites>>(resultBody);
            }
            else
            {
                throw new Exception("GetAllFavorites failed to get");
            }


            return result;
        }

        public async Task<Favorites> GetAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/favorites/{id}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            Favorites result;

            var resultBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Favorites>(resultBody);
            }
            else
            {
                throw new Exception("GetFavoritesById failed to get");
            }



            return result;


        }
        public async Task<Favorites> PostAsync(Favorites favorite)
        {

            HttpResponseMessage response;
            string url = $"/api/favorites/";
            var body = JsonConvert.SerializeObject(favorite);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await client.SendAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Favorites result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Favorites>(resultContent);
            }
            else
            {
                result = JsonConvert.DeserializeObject<Favorites>(resultContent);
                throw new Exception("Bus failed to be posted");
            }

            return result;
        }

        public async Task<Favorites> PutAsync(string id, Favorites favorite)
        {
            HttpResponseMessage response;
            string url = $"/api/favorites/{id}";
            var body = JsonConvert.SerializeObject(favorite);
            using (var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await client.SendAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Favorites result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Favorites>(resultContent);
            }
            else
            {
                throw new Exception("Favorite failed to be put");
            }

            return result;


        }

        public async Task<Favorites> DeleteAsync(string id)
        {
            HttpResponseMessage response;
            string url = $"/api/favorites/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            response = await client.SendAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Favorites result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Favorites>(resultContent);
            }
            else
            {
                throw new Exception("Favorite failed to be deleted");
            }

            return result;


        }

    }
}
