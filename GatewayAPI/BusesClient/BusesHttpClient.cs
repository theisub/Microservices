using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using BusAPI.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace GatewayAPI.BusesClient
{
    public class BusesHttpClient :  IBusesHttpClient
    {
        protected readonly HttpClient client;
        private readonly string appId = "BusesID";
        private readonly string appSecret = "BusesSecret";
        private string accessToken = null;
        private bool tokenValid = false;
        private bool appAuthorized = false;

        private string refreshToken = null;

        public BusesHttpClient(HttpClient client) : base()
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
        public async Task<List<Bus>> GetAsync()
        {
            HttpResponseMessage response;
            string url = $"/api/buses/";


            await Auth(new AppInfo { Username = appId, Password = appSecret });
            await IsTokenValid(this.accessToken);

            await RefreshToken(this.refreshToken);

            if (this.tokenValid)
            {

                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    response = await client.SendAsync(request);

                List<Bus> result;

                var resultContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
                }
                else
                {
                    throw new Exception("GetAllBuses failed to get");
                }
                return result;

            }
            throw new Exception("GetAllBuses failed to get");

        }

        public async Task<Bus> GetIdAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/{id}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            Bus result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Bus>(resultContent);
            }
            else
            {
                throw new Exception("GetAllBusesByCompany failed to get");
            }


            return result;
        }


        public async Task<List<Bus>> GetAllBusesByCompany(string companyName, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;

            string url = $"/api/buses/companies/{companyName}?&pageNum={pageNum}&pageSize={pageSize}";
            
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Bus> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllBusesByCompany failed to get");
            }

            return result;
        }

        public async Task<List<Bus>> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/pricerange?minPrice={minPrice}&maxPrice={maxPrice}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Bus> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllBusesByCompany failed to get");
            }

            return result;

        }


        public async Task<List<Bus>> GetAllBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/routes?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Bus> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
            }
            else
            {
                throw new Exception("GetAllBusesByRoute failed to get");
            }

            return result;


        }

        public async Task<List<Bus>> GetFastestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/fastestRoute?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await client.SendAsync(request);

            List<Bus> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
            }
            else
            {
                throw new Exception("GetFastestBuses failed to get");
            }

            return result;



        }

        public async Task<List<Bus>> GetCheapestBuses(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/cheapestRoute?inCity={inCity}&outCity={outCity}&pageNum={pageNum}&pageSize={pageSize}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                try
                {
                    response = await client.SendAsync(request);
                    List<Bus> result;

                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<List<Bus>>(resultContent);
                    }
                    else
                    {
                        throw new Exception("GetCheapestBuses failed to get");
                    }

                    return result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("GetCheapestBuses failed to get");

                }

            }
            throw new Exception("clientApp token not valid");


        }





        public async Task<Bus> PostAsync(Bus dto)
        {
            await CheckAuth();
            if (this.tokenValid)
            {
                HttpResponseMessage response;
                string url = $"/api/buses/";
                var body = JsonConvert.SerializeObject(dto);
                using (var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                })
                    response = await client.SendAsync(request);

                var resultContent = await response.Content.ReadAsStringAsync();
                Bus result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Bus>(resultContent);
                }
                else
                {
                    throw new Exception("Bus failed to be posted");
                }

                return result;
            }
            throw new Exception("clientApp token not valid");

        }

        public async Task<Bus> PutAsync(long id, Bus bus)
        {
            await CheckAuth();
            if (this.tokenValid)
            {
                HttpResponseMessage response;
                string url = $"/api/buses/{id}";
                var body = JsonConvert.SerializeObject(bus);
                using (var request = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                })
                    response = await client.SendAsync(request);

                var resultContent = await response.Content.ReadAsStringAsync();
                Bus result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Bus>(resultContent);
                }
                else
                {
                    throw new Exception("Plane failed to be put");
                }

                return result;
            }
            throw new Exception("clientApp token not valid");



        }

        public async Task<Bus> DeleteAsync(long id)
        {
            await CheckAuth();
            if (this.tokenValid)
            {
                HttpResponseMessage response;
                string url = $"/api/buses/{id}";

                var request = new HttpRequestMessage(HttpMethod.Delete, url);

                response = await client.SendAsync(request);

                var resultContent = await response.Content.ReadAsStringAsync();
                Bus result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Bus>(resultContent);
                }
                else
                {
                    throw new Exception("Bus failed to be deleted");
                }

                return result;
            }
            throw new Exception("clientApp token not valid");



        }



    }
}
