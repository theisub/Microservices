using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DbContext _db;
        private readonly List<string> usernames = new List<string> {  "BusesID", "PlanesID", "FavoritesID" };
        private readonly List<string> passwords = new List<string> {  "BusesSecret", "PlanesSecret", "FavoritesSecret" };

        private readonly string appId = "PlanesID";
        private readonly string appSecret = "PlanesSecret";

        public AccountsController(IConfiguration configuration, DbContext db)
        {
            _configuration = configuration;
            _db = db;
        }



        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo request)
        {



            var byteArray = Encoding.ASCII.GetBytes($"{appId}:{appSecret}");


            var something = Convert.ToBase64String(byteArray);
            string username = Encoding.ASCII.GetString(byteArray, 0, appId.Length);
            string password = Encoding.ASCII.GetString(byteArray, appId.Length + 1, appSecret.Length);



            bool isItAuth = false;

            var authCheck = _db.AuthTokens.SingleOrDefault(m => m.AuthToken == request.Username);
            if (authCheck != null)
            {
                isItAuth = true;
                _db.Remove(authCheck);
                //_db.SaveChanges();
            }





            if (usernames.Contains(request.Username) && passwords.Contains(request.Password) || (isItAuth) || /*(request.Username == "admin" && request.Password == "admin") ||*/ (request.Username == "user" && request.Password == "user"))
            {
                var userclaim = new[] { new Claim(ClaimTypes.Name, request.Username) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var Expiring = DateTime.Now.AddMinutes(1);

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:5051",
                    audience: "https://localhost:5051",
                    claims: userclaim,
                    expires: Expiring,
                    signingCredentials: creds);

                var _refreshTokenObj = new RefreshToken
                {
                    Username = request.Username,
                    Refreshtoken = Guid.NewGuid().ToString()
                };

                _db.Tokens.Add(_refreshTokenObj);
                _db.SaveChanges();
                TimeSpan expires_inTime = Expiring.Subtract(DateTime.Now);
                int secs = Convert.ToInt32(expires_inTime.TotalSeconds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refToken = _refreshTokenObj.Refreshtoken,
                    expires_in = secs

                });
            }

            return BadRequest("Username or password are incorrect");


        }

        [HttpGet]
        public IActionResult Get()
        {


            string value = "Heh";

            return BadRequest("Username or password are incorrect");


        }


        [HttpPost("auth", Name = "auth")]
        public IActionResult Auth([FromBody] LoginInfo request)
        {


            if (request.Username == "LoginPage" && request.Password == "/")
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }



        }



        [HttpPost("oauth", Name = "oauth")]
        public IActionResult OAuth([FromBody] LoginInfo request)
        {

            var userDB = _db.Users.SingleOrDefault(m => m.Username == request.Username);

            if (userDB != null)
            {
                byte[] data = Encoding.Default.GetBytes(request.Password);
                var result = new SHA256Managed().ComputeHash(data);
                var hashedPass = BitConverter.ToString(result).Replace("-", "").ToLower();
                var newAuthToken = new AuthTokens { AuthToken = Guid.NewGuid().ToString() };
                if (hashedPass == userDB.Password)
                {
                    _db.AuthTokens.Add(newAuthToken);
                    _db.SaveChanges();
                    return Ok(new { authCode = newAuthToken.AuthToken });

                }
                else
                {
                    return Unauthorized(); 
                }


            }
            

            return Unauthorized();


        }




        [HttpPost("{refreshToken}/refresh")]
        public IActionResult RefreshToken([FromRoute]string refreshToken)
        {
            var _refreshToken = _db.Tokens.SingleOrDefault(m => m.Refreshtoken == refreshToken);

            if (_refreshToken == null)
            {
                return NotFound("There are no such refreshTokens ");
            }
            var userclaim = new[] { new Claim(ClaimTypes.Name, _refreshToken.Username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            _refreshToken.Refreshtoken = Guid.NewGuid().ToString();
            _db.Tokens.Update(_refreshToken);
            _db.SaveChanges();

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), refToken = _refreshToken.Refreshtoken });
        }
    }
}
