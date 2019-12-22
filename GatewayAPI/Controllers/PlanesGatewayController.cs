using System;
using System.Collections.Generic;
using System.Linq;
using GatewayAPI.PlanesClient;
using PlaneAPI.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using GatewayAPI.FavoritesClient;
using FavoritesAPI.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesGatewayController : ControllerBase
    {
        private readonly IPlanesHttpClient planesHttpClient;
        private readonly IFavoritesHttpClient favoritesHttpClient;

        public PlanesGatewayController(IPlanesHttpClient planesHttpClient, IFavoritesHttpClient favoritesHttpClient)
        {
            this.planesHttpClient = planesHttpClient;
            this.favoritesHttpClient = favoritesHttpClient;
        }
        // GET: api/PlanesGateway
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result;
            try
            {
                result = Ok(await planesHttpClient.GetAsync());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get all method activated");

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get all method exception {ex.Message}");
                return BadRequest();
            }

            return result;
        }

        // GET: api/PlanesGateway/5
        [HttpGet("{id}", Name = "GetPlanesById")]
        public async Task<IActionResult> GetId(int id)
        {

            IActionResult result;

            try
            {
                result = Ok(await planesHttpClient.GetIdAsync(id));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get planesById method activated");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get planesById method exception {ex.Message}");
                return BadRequest();
            }

            return result;

        }

        //PlanesGateway/companies/Anyname
        [HttpGet("companies/{company}", Name = "GetPlanesByCompanyGateway")]
        public async Task<IActionResult> GetPlanesByCompany(string company)
        {
            IActionResult result;

            try
            {
                var PlanesByCompany = await planesHttpClient.GetAllPlanesByCompany(company);
                result = Ok(PlanesByCompany);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get GetPlanesByCompanyGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get GetPlanesByCompanyGateway method exception {ex.Message}");
            }
            return result;

        }

        //PlanesGateway/priceRange?minPrice = 100&maxPrice = 200
        [HttpGet("priceRange", Name = "GetAllPlanesByPriceGateway")]
        public async Task<IActionResult> GetAllPlanesByPrice(long? minPrice = null, long? maxPrice = null)
        {
            IActionResult result;

            try
            {
                var PlanesByPrice = await planesHttpClient.GetAllPlanesByPrice(minPrice, maxPrice);
                result = Ok(PlanesByPrice);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get GetAllPlanesByPriceGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get GetAllPlanesByPriceGateway method exception {ex.Message}");
            }
            return result;

        }
        //?/????
        [HttpGet("routes", Name = "GetPlanesByRouteGateway")]
        public async Task<IActionResult> GetPlanesByRoute(string inCity, string outCity,int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var PlanesByRoute = await planesHttpClient.GetAllPlanesByRoute(inCity, outCity,pageNum,pageSize);
                result = Ok(PlanesByRoute);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get GetPlanesByRouteGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get GetPlanesByRouteGateway method exception {ex.Message}");
            }
            return result;

        }

        [HttpGet("fastestRoute", Name = "GetFastestPlanesGateway")]
        public async Task<IActionResult> GetFastestPlanes(string inCity, string outCity,int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var PlanesByTime = await planesHttpClient.GetFastestPlanes(inCity, outCity, pageNum,pageSize);
                result = Ok(PlanesByTime);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get GetFastestPlanesGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get GetFastestPlanesGateway method exception {ex.Message}");
            }
            return result;

        }

        [HttpGet("cheapestRoute", Name = "GetCheapestPlanesGateway")]
        public async Task<IActionResult> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var PlanesByCost = await planesHttpClient.GetCheapestPlanes(inCity, outCity, pageNum,pageSize);
                result = Ok(PlanesByCost);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get GetCheapestPlanesGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get GetCheapestPlanesGateway method exception {ex.Message}");
            }
            return result;



        }



        // POST: api/PlanesGateway
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Plane plane)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity =plane;
                var newEntity = await planesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method activated");
            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method dbexception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method exception {ex.Message}");
            }
            return result;

        }

        // PUT: api/PlanesGateway/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Plane plane)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = plane;
                var newEntity = await planesHttpClient.PutAsync(id, entity);
                result = CreatedAtAction(nameof(Put), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Put method activated");
            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method dbexception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method exception {ex.Message}");
            }
            return result;



        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
               
                var newEntity = await planesHttpClient.DeleteAsync(id);
                result = CreatedAtAction(nameof(Put), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method activated");

                var checkFavorites = await favoritesHttpClient.GetAllAsync();
                var checking = checkFavorites.Where(x => x.PlanesRoute.All(y => y.Id == id)).ToList();

                foreach (Favorites item in checking)
                {
                    var newPlanesRoute = item.PlanesRoute.Where(route => route.Id != id).ToList();
                    item.PlanesRoute = newPlanesRoute;
                    await favoritesHttpClient.PutAsync(item.Id, item);
                }


            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method dbexception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method exception {ex.Message}");
            }
            return result;
        }
    }
}
