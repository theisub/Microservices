using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GatewayAPI.AuthorizationClient
{
    public class AuthHttpClient : IAuthHttpClient
    {
        protected readonly HttpClient client;
        private string accessToken = null;
        private bool tokenValid = false;
        private string refreshToken = null;

        public AuthHttpClient(HttpClient client) : base()
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

        public async Task<TokenInfo> Auth(AppInfo appInfo)
        {
            HttpResponseMessage response;
            TokenInfo tokenInfo = null;
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
            }
            return tokenInfo;



        }

        public async Task<bool> IsTokenValid(string accessToken)
        {
            HttpResponseMessage response;

            string urlAuth = $"https://localhost:5051/api/test";

            tokenValid = false;
            this.client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", accessToken);
            using (var request = new HttpRequestMessage(HttpMethod.Get, urlAuth))
            {
                response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    this.tokenValid = true;
            }
            return this.tokenValid;
        }

        public async Task<TokenInfo> RefreshToken(string refreshToken)
        {
            HttpResponseMessage response;
            TokenInfo tokenInfo = null;
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
                    this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
                }


            }
            return tokenInfo;


        }
    }
}
