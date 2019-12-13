using System;
using System.Net.Http;
using PlaneAPI.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.PlanesClient
{
    public class PlanesHttpClient : IPlanesHttpClient
    {
        private readonly HttpClient client;
        public PlanesHttpClient(HttpClient client) : base()
        {
            this.client = client;

        }
        public async Task<List<Plane>> GetAsync()
        {
            HttpResponseMessage response;
            string url = $"/api/planes/";
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
                response = await client.SendAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetCheapestPlanes failed to get");
            }

            return result;
        }





        public async Task<Plane> PostAsync(Plane plane)
        {

            HttpResponseMessage response;
            string url = $"/api/planes/";
            var body = JsonConvert.SerializeObject(plane);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url)
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
