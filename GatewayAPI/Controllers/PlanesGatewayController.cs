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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesGatewayController : ControllerBase
    {
        private readonly IPlanesHttpClient planesHttpClient;
        private readonly IMapper mapper;

        public PlanesGatewayController(IPlanesHttpClient planesHttpClient, IMapper mapper)
        {
            this.planesHttpClient = planesHttpClient;
            this.mapper = mapper;
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

        [HttpGet("routes/{inCity}&{outCity}", Name = "GetPlanesByRouteGateway")]
        public async Task<IActionResult> GetPlanesByRoute(string inCity, string outCity)
        {
            IActionResult result;

            try
            {
                var PlanesByRoute = await planesHttpClient.GetAllPlanesByRoute(inCity, outCity);
                result = Ok(PlanesByRoute);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        [HttpGet("fastestRoute", Name = "GetFastestPlanesGateway")]
        public async Task<IActionResult> GetFastestPlanes(string inCity, string outCity, int size = 10)
        {
            IActionResult result;

            try
            {
                var PlanesByTime = await planesHttpClient.GetFastestPlanes(inCity, outCity, size);
                result = Ok(PlanesByTime);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        [HttpGet("cheapestRoute", Name = "GetCheapestPlanesGateway")]
        public async Task<IActionResult> GetCheapestPlanes(string inCity, string outCity, int size = 10)
        {
            IActionResult result;

            try
            {
                var PlanesByCost = await planesHttpClient.GetCheapestPlanes(inCity, outCity, size);
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
                var entity = mapper.Map<Plane>(plane);
                var newEntity = await planesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), mapper.Map<Plane>(newEntity));
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
