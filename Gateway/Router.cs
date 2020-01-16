using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

            /*testing start
            var client = new HttpClient();

            var urls = new string[]
            {
                "https://localhost:44365/api/trains/3",
                "https://localhost:44365/api/trains",
                "https://localhost:44331/api/buses"
            };



            var requests = urls.Select
            (
                url =>  client.GetAsync(url)
            );
            await Task.WhenAll(requests);

            string s="";
            var responses = requests.Select
            (
                task => task.Result
            );
            foreach (var r in responses)
            {
                s += await r.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(s);
            }


            *///testing ended

            // сюда нужно проверку на кол-во обращений в апи


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

            //здесь сделать агрегацию всех респонсев в один и только потом отправлять
            return await destination.SendRequest(request);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpClient client,HttpRequest request)
        {
            string requestContent;

            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), "https://localhost:44365/api/trains/3"))
            {
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                var response = await client.SendAsync(newRequest);

                var mess = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Received response {mess} from {request}");
                return response;

            }
           
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
