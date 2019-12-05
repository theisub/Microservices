using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
namespace GatewayAPI.PlanesClient
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
