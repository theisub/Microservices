using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GatewayAPI.BusesClient;
using System.Net.Http;
using BusAPI.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GatewayAPI.FavoritesClient;
using FavoritesAPI.Model;

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesGatewayController : ControllerBase
    {
        private readonly IBusesHttpClient busesHttpClient;
        private readonly IFavoritesHttpClient favoritesHttpClient;



        public BusesGatewayController(IBusesHttpClient busesHttpClient, IFavoritesHttpClient favoritesHttpClient)
        {
            this.favoritesHttpClient = favoritesHttpClient;
            this.busesHttpClient = busesHttpClient;

        }
        // GET: api/BusesGateway
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result;

            
            try
            {
                result = Ok(await busesHttpClient.GetAsync());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get method activated");
            }
            catch(Exception ex)
            {
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Get method Exception {ex.Message} ");
                return BadRequest();
            }

            return result;
            //return new string[] { "value1 etogateway  ", "value2 etogateway " };
        }

        // GET: api/BusesGateway/5
        [HttpGet("{id}", Name = "GetBuses")]
        public string Get(int id)
        {
            return "value etogogateway";
        }

        //busesGateway/companies/Anyname
        [HttpGet("companies/{company}", Name = "GetBusesByCompanyGateway")]
        public async Task<IActionResult> GetBusesByCompany(string company, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByCompany = await busesHttpClient.GetAllBusesByCompany(company,pageNum,pageSize);
                result = Ok(busesByCompany);

                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetBusesByCompanyGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"GetBusesByCompanyGateway method exception {ex.Message}");
            }
            return result;

        }

        //busesGateway/priceRange?minPrice=100&maxPrice=200
        [HttpGet("priceRange", Name = "GetAllBusesByPriceGateway")]
        public async Task<IActionResult> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByPrice = await busesHttpClient.GetAllBusesByPrice(minPrice,maxPrice, pageNum, pageSize);
                result = Ok(busesByPrice);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetAllBusesByPriceGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"GetAllBusesByPriceGateway method exception {ex.Message}");
            }
            return result;

        }

        [HttpGet("routes", Name = "GetBusesByRouteGateway")]
        public async Task<IActionResult> GetBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByRoute = await busesHttpClient.GetAllBusesByRoute(inCity, outCity, pageNum, pageSize);
                result = Ok(busesByRoute);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetBusesByRouteGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"GetBusesByRouteGateway method exception {ex.Message}");
            }
            return result;

        }

        [HttpGet("fastestRoute", Name = "GetFastestBusesGateway")]
        public async Task<IActionResult> GetFastestBuses(string inCity, string outCity,int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByTime = await busesHttpClient.GetFastestBuses(inCity, outCity, pageNum, pageSize);
                result = Ok(busesByTime);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetFastestBusesGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"GetFastestBusesGateway method exception {ex.Message}");
            }
            return result;

        }

        [HttpGet("cheapestRoute", Name = "GetCheapestBusesGateway")]
        public async Task<IActionResult> GetCheapestBuses (string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByCost = await busesHttpClient.GetCheapestBuses(inCity, outCity, pageNum, pageSize);
                result = Ok(busesByCost);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetCheapestBusesGateway method activated");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"GetCheapestBusesGateway method exception {ex.Message}");
            }
            return result;



        }



        // POST: api/BusesGateway
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = bus;
                var newEntity = await busesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method activated");
            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method exception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method exception {ex.Message}");
            }
            return result;

        }

        // PUT: api/BusesGateway/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bus plane)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = plane;
                var newEntity = await busesHttpClient.PutAsync(id, entity);
                result = CreatedAtAction(nameof(Put), newEntity);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Put method activated");
            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method exception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

                var newEntity = await busesHttpClient.DeleteAsync(id);
                result = AcceptedAtAction(nameof(Delete), newEntity);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Delete method activated");

                var checkFavorites = await favoritesHttpClient.GetAllAsync();
                var checking = checkFavorites.Where(x => x.BusesRoute.All(y => y.Id == id)).ToList();

                foreach (Favorites item in checking)
                {
                    var newBusesRoute = item.BusesRoute.Where(route => route.Id != id).ToList();
                    item.BusesRoute = newBusesRoute;
                    await favoritesHttpClient.PutAsync(item.Id, item);
                }


            }
            catch (DbUpdateException dbEx)
            {
                result = Conflict();
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Delete method exception {dbEx.Message}");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Delete method exception {ex.Message}");
            }
            return result;
        }
    }
}
