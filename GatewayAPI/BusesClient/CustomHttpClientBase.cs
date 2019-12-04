using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayAPI.BusesClient
{
    public class CustomHttpClientBase
    {
        protected readonly HttpClient client;

        public CustomHttpClientBase(HttpClient client)
        {
            this.client = client;
        }

        protected async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            try
            {
                return await client.SendAsync(request);
            }
            catch (Exception)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.GatewayTimeout
                };
            }
        }

    }
}
