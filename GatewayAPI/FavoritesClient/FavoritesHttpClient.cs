using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FavoritesAPI.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace GatewayAPI.FavoritesClient
{
    public class FavoritesHttpClient : IFavoritesHttpClient
    {
        protected readonly HttpClient client;
        private readonly string appId = "FavoritesID";
        private readonly string appSecret = "FavoritesSecret";
        private string accessToken = null;
        private bool tokenValid = false;
        private bool appAuthorized = false;

        private string refreshToken = null;

        public FavoritesHttpClient(HttpClient client) : base()
        {
            this.client = client;
        }
        public class TokenInfo
        {
            public string token { get; set; }
            public string refToken { get; set; }
        }
        public class AppInfo
        {
            public string Username { get; set; }
            public string Password { get; set; }

        }

        private async Task<bool> CheckAuth()
        {
            if (!appAuthorized)
            {
                await Auth(new AppInfo { Username = appId, Password = appSecret });
                this.appAuthorized = true;
                this.tokenValid = true;
            }

            if (this.tokenValid == false)
                this.tokenValid = await IsTokenValid(this.accessToken);


            if (this.tokenValid == false)
                await RefreshToken(this.refreshToken);

            return this.tokenValid;

        }
        private async Task<bool> Auth(AppInfo appInfo)
        {
            HttpResponseMessage response;
            TokenInfo tokenInfo;
            string urlAuthApp = $"https://localhost:5051/api/accounts/";
            tokenValid = false;
            var body = JsonConvert.SerializeObject(appInfo);



            using (var request = new HttpRequestMessage(HttpMethod.Post, urlAuthApp)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })

                response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var resultContent = await response.Content.ReadAsStringAsync();
                tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(resultContent);
                this.accessToken = tokenInfo.token;
                this.refreshToken = tokenInfo.refToken;
                this.tokenValid = true;
                this.client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.accessToken);
            }
            return this.tokenValid;



        }

        private async Task<bool> IsTokenValid(string accessToken)
        {
            HttpResponseMessage response;

            string urlAuth = $"https://localhost:5051/api/test";

            tokenValid = false;
            this.client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
            using (var request = new HttpRequestMessage(HttpMethod.Get, urlAuth))
            {
                response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    this.tokenValid = true;
            }
            return this.tokenValid;
        }

        private async Task<bool> RefreshToken(string refreshToken)
        {
            HttpResponseMessage response;
            TokenInfo tokenInfo;
            string urlRefresh = $"https://localhost:5051/api/accounts/{refreshToken}/refresh";
            tokenValid = false;

            using (var request = new HttpRequestMessage(HttpMethod.Post, urlRefresh))
            {
                response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultContent = await response.Content.ReadAsStringAsync();
                    tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(resultContent);
                    this.accessToken = tokenInfo.token;
                    this.refreshToken = tokenInfo.refToken;
                    this.tokenValid = true;
                    this.client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", this.accessToken);
                }


            }
            return this.tokenValid;


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
            await CheckAuth();

            if (this.tokenValid)
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
            throw new Exception("clientApp failed to get");


        }

        public async Task<Favorites> PutAsync(string id, Favorites favorite)
        {
            await CheckAuth();

            if (this.tokenValid)
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
            throw new Exception("clientApp failed to get");



        }

        public async Task<Favorites> DeleteAsync(string id)
        {
            await CheckAuth();

            if (this.tokenValid)
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

            throw new Exception("clientApp failed to get");

        }

    }
}
