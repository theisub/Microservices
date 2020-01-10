using System;
using System.Net.Http;
using PlaneAPI.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.PlanesClient
{
    public class PlanesHttpClient : IPlanesHttpClient
    {
        private readonly HttpClient client;
        private readonly string appId = "PlanesID";
        private readonly string appSecret = "PlanesSecret";
        private string accessToken = null;
        private bool tokenValid = false;
        private bool appAuthorized = false;

        private  string refreshToken = null;
        public PlanesHttpClient(HttpClient client) : base()
        {
            this.client = client;
            //client.DefaultRequestHeaders.Add("appId", appId);
            //client.DefaultRequestHeaders.Add("appSecret", appSecret);


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
        public async Task<List<Plane>> GetAsync()
        {
            HttpResponseMessage response;
            string url = $"/api/planes/";

            await Auth(new AppInfo { Username = appId, Password = appSecret });
            await IsTokenValid(this.accessToken);

            await RefreshToken(this.refreshToken);

            if (this.tokenValid)
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    response = await client.SendAsync(request);

                List<Plane> result;

                var resultContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
                }
                else
                {
                    throw new Exception("GetAllPlanes failed to get");
                }
                return result;
            }

            throw new Exception("GetAllPlanes failed to get");
        }
        public async Task<Plane> GetIdAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/{id}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            Plane result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Plane>(resultContent);
            }
            else
            {
                throw new Exception("GetPlanesById failed to get");
            }


            return result;
        }

        public async Task<List<Plane>> GetAllPlanesByCompany(string companyName, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;

            string url = $"/api/planes/companies/{companyName}?&pageNum={pageNum}&pageSize={pageSize}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllPlanesByCompany failed to get");
            }

            return result;
        }

        public async Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/pricerange?minPrice={minPrice}&maxPrice={maxPrice}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllPlanesByCompany failed to get");
            }

            return result;

        }


        public async Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/routes?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllPlanesByRoute failed to get");
            }

            return result;


        }

        public async Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/fastestRoute?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetFastestPlanes failed to get");
            }

            return result;



        }

        public async Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/cheapestRoute?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                try
                {
                    response = await client.SendAsync(request);
                    List<Plane> result;

                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {

                        return null;
                    }
                    else
                    {
                        throw new Exception("GetCheapestPlanes failed to get");
                    }

                    return result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("GetCheapestPlanes failed to get");


                };
                


            }   
        }





        public async Task<Plane> PostAsync(Plane plane)
        {

            HttpResponseMessage response;
            string url = $"/api/planes/";
            var body = JsonConvert.SerializeObject(plane);

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                })
                    response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception("plane failed to be posted");
            }

            var resultContent = await response.Content.ReadAsStringAsync();
            Plane result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Plane>(resultContent);
            }
            else
            {
                throw new Exception("plane failed to be posted");
            }

            return result;
        }

        public async Task<Plane> PutAsync(long id, Plane plane)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/{id}";
            var body = JsonConvert.SerializeObject(plane);
            using (var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await client.SendAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Plane result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Plane>(resultContent);
            }
            else
            {
                throw new Exception("Plane failed to be put");
            }

            return result;


        }

        public async Task<Plane> DeleteAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            
            response = await client.SendAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Plane result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Plane>(resultContent);
            }
            else
            {
                throw new Exception("Plane failed to be deleted");
            }

            return result;


        }

    }
}
