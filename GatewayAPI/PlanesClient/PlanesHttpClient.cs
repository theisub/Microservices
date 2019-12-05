using System;
using System.Net.Http;
using PlaneAPI.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.PlanesClient
{
    public class PlanesHttpClient : CustomHttpClientBase, IPlanesHttpClient
    {
        public PlanesHttpClient(HttpClient client) : base(client)
        {


        }
        public async Task<string> GetAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

            var resultBody = await response.Content.ReadAsStringAsync();


            return resultBody;
        }


        public async Task<List<Plane>> GetAllPlanesByCompany(string companyName)
        {
            HttpResponseMessage response;

            string url = $"/api/planes/companies/{companyName}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

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

        public async Task<List<Plane>> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/pricerange?minPrice={minPrice}&maxPrice={maxPrice}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

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


        public async Task<List<Plane>> GetAllPlanesByRoute(string inCity, string outCity)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/routes/{inCity}&{outCity}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

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

        public async Task<List<Plane>> GetFastestPlanes(string inCity, string outCity, int size = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/fastestRoute?inCity={inCity}&outCity={outCity}&size={size}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

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

        public async Task<List<Plane>> GetCheapestPlanes(string inCity, string outCity, int size = 10)
        {
            HttpResponseMessage response;
            string url = $"/api/planes/cheapestRoute?inCity={inCity}&outCity={outCity}&size={size}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

            List<Plane> result;

            var resultContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Plane>>(resultContent);
            }
            else
            {
                throw new Exception("GetCheapestBuses failed to get");
            }

            return result;
        }





        public async Task<Plane> PostAsync(Plane dto)
        {

            HttpResponseMessage response;
            string url = $"/api/planes/";
            var body = JsonConvert.SerializeObject(dto);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await SendRequestAsync(request);

            var resultContent = await response.Content.ReadAsStringAsync();
            Plane result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Plane>(resultContent);
            }
            else
            {
                throw new Exception("Bus failed to be posted");
            }

            return result;
        }
    }
}
