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


        public BusesGatewayController(IBusesHttpClient busesHttpClient)
        {
            this.busesHttpClient = busesHttpClient;

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
        public async Task<IActionResult> GetBusesByCompany(string company, int pageNum = 1, int pageSize = 10)
        {
            IActionResult result;

            try
            {
                var busesByCompany = await busesHttpClient.GetAllBusesByCompany(company,pageNum,pageSize);
                result = Ok(busesByCompany);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
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
                var entity = bus;
                var newEntity = await busesHttpClient.PostAsync(entity);
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

                var newEntity = await busesHttpClient.DeleteAsync(id);
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
