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

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesGatewayController : ControllerBase
    {
        private readonly IBusesHttpClient busesHttpClient;
        private readonly IMapper mapper;

        public BusesGatewayController(IBusesHttpClient busesHttpClient, IMapper mapper)
        {
            this.busesHttpClient = busesHttpClient;
            this.mapper = mapper;
        }
        // GET: api/BusesGateway
        [HttpGet]
        public async Task<string> Get()
        {
            string resultik;
            try
            {
                resultik = await busesHttpClient.GetAsync(1111);
                
            }
            catch
            {
              resultik="well u fucked up";
            }

            return resultik;
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
        public async Task<IActionResult> GetBusesByCompany(string company)
        {
            IActionResult result;

            try
            {
                var busesByCompany = await busesHttpClient.GetAllBusesByCompany(company);
                result = Ok(busesByCompany);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        //busesGateway/priceRange?minPrice = 100&maxPrice = 200
        [HttpGet("priceRange", Name = "GetAllBusesByPriceGateway")]
        public async Task<IActionResult> GetAllBusesByPrice(long? minPrice = null, long? maxPrice = null)
        {
            IActionResult result;

            try
            {
                var busesByPrice = await busesHttpClient.GetAllBusesByPrice(minPrice,maxPrice);
                result = Ok(busesByPrice);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        [HttpGet("routes/{inCity}&{outCity}", Name = "GetBusesByRouteGateway")]
        public async Task<IActionResult> GetBusesByRoute(string inCity, string outCity)
        {
            IActionResult result;

            try
            {
                var busesByRoute = await busesHttpClient.GetAllBusesByRoute(inCity, outCity);
                result = Ok(busesByRoute);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        [HttpGet("fastestRoute", Name = "GetFastestBusesGateway")]
        public async Task<IActionResult> GetFastestBuses(string inCity, string outCity, int size = 10)
        {
            IActionResult result;

            try
            {
                var busesByTime = await busesHttpClient.GetFastestBuses(inCity, outCity,size);
                result = Ok(busesByTime);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
            return result;

        }

        [HttpGet("cheapestRoute", Name = "GetCheapestBusesGateway")]
        public async Task<IActionResult> GetCheapestBuses (string inCity, string outCity, int size = 10)
        {
            IActionResult result;

            try
            {
                var busesByCost = await busesHttpClient.GetCheapestBuses(inCity, outCity, size);
                result = Ok(busesByCost);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
                var entity = mapper.Map<Bus>(bus);
                var newEntity = await busesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), mapper.Map<Bus>(newEntity));
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

        // PUT: api/BusesGateway/5
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
