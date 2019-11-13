using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway
{
    public class Router
    {
        public List<Route> Routes { get; set; }
        public Destination AuthentificationService { get; set; }

        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonWorker.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonWorker.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthentificationService = JsonWorker.Deserialize<Destination>(Convert.ToString(router.authentificationService));
        }
        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            
            
            
            
            string basePath = '/' + path.Split('/')[1];
            Console.WriteLine(path);

            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return ConstructErrorMessage("The path could not be found.");
            }

            if (destination.RequiresAuthentification)
            {
                string token = request.Headers["token"];
                request.Query.Append(new KeyValuePair<string, StringValues>("token", new StringValues(token)));
                HttpResponseMessage authResponse = await AuthentificationService.SendRequest(request);
                if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");
            }

            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }
    }
}
