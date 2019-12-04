using Microsoft.AspNetCore.Http;
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
    }
}
