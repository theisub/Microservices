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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesGatewayController : ControllerBase
    {
        private readonly IPlanesHttpClient planesHttpClient;

        public PlanesGatewayController(IPlanesHttpClient planesHttpClient)
        {
            this.planesHttpClient = planesHttpClient;
        }
        // GET: api/PlanesGateway
        [HttpGet]
        public async Task<string> Get()
        {
            string resultik;
            try
            {
                resultik = await planesHttpClient.GetAsync(1111);

            }
            catch
            {
                resultik = "well u fucked up with planes";
            }

            return resultik;
            //return new string[] { "value1 etogateway  ", "value2 etogateway " };
        }

        // GET: api/PlanesGateway/5
        [HttpGet("{id}", Name = "GetPlanes")]
        public string Get(int id)
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Very important info");

            return "value etogogateway planes";
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (DbUpdateException)
            {
                result = Conflict();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            }
            catch (DbUpdateException)
            {
                result = Conflict();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            }
            catch (DbUpdateException)
            {
                result = Conflict();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return result;
        }
    }
}
