﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using BusAPI.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.BusesClient
{
    public class BusesHttpClient : CustomHttpClientBase, IBusesHttpClient
    {
        public BusesHttpClient(HttpClient client) : base(client)
        {
            

        }
        public async Task<string> GetAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

            var resultBody = await response.Content.ReadAsStringAsync();


            return resultBody;
        }


        public async Task<List<Bus>> GetAllBusesByCompany(string companyName, int pageNum = 1, int pageSize = 10)
        {
            HttpResponseMessage response;

            string url = $"/api/buses/companies/{companyName}?&pageNum={pageNum}&pageSize={pageSize}";
            
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                response = await SendRequestAsync(request);

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
                response = await SendRequestAsync(request);

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
                response = await SendRequestAsync(request);

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
                response = await SendRequestAsync(request);

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
                response = await SendRequestAsync(request);

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





        public async Task<Bus> PostAsync(Bus dto)
        {

            HttpResponseMessage response;
            string url = $"/api/buses/";
            var body = JsonConvert.SerializeObject(dto);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await SendRequestAsync(request);

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

        public async Task<Bus> PutAsync(long id, Bus bus)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/{id}";
            var body = JsonConvert.SerializeObject(bus);
            using (var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
                response = await SendRequestAsync(request);

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

        public async Task<Bus> DeleteAsync(long id)
        {
            HttpResponseMessage response;
            string url = $"/api/buses/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            response = await SendRequestAsync(request);

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



    }
}
