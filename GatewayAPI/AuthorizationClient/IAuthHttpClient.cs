using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayAPI.Controllers;
using static GatewayAPI.AuthorizationClient.AuthHttpClient;

namespace GatewayAPI.AuthorizationClient
{
    public interface IAuthHttpClient
    {
        Task<TokenInfo> Auth(AppInfo appInfo);
        Task<bool> IsTokenValid(string accessToken);
        Task<TokenInfo> RefreshToken(string refreshToken);
    }
}
