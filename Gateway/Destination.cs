using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public class Destination
    {
        public string Path { get; set; }
        public bool RequiresAuthentification { get; set; }
        static HttpClient client = new HttpClient();

        public Destination (string uri, bool requiresAuthentification)
        {
            Path = uri;
            RequiresAuthentification = requiresAuthentification;
        }

        public Destination(string uri) : this(uri,false)
        {

        }

        private Destination()
        {

            Path = "/";
            RequiresAuthentification = false;
        }
        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;

            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request)))
            {
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                var response = await client.SendAsync(newRequest);
                return  response;
                
            }
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            /*
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();

            string[] endpointSplit = requestPath.Substring(1).Split('/');
            string endpoint = endpointSplit[0];

            queryString = requestPath.Substring(requestPath.IndexOf('/')+endpoint.Length+1);

            if (endpointSplit.Length > 1)
                endpoint = endpointSplit[0];
            //if (queryString == "" && endpointSplit.Length > 1)
            //    queryString = "/" +endpointSplit[endpointSplit.Length-1];

            //return "https://localhost:44365/api/trains/3";
        
            return Path + endpoint + queryString;
            */
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();

            string endpoint = "";
            string[] endpointSplit = requestPath.Substring(1).Split('/');

            if (endpointSplit.Length > 1)
                endpoint = endpointSplit[1];


            return Path + endpoint + queryString;
        }

    }
}
