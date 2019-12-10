using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BusAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {

        private readonly IBusActions busActions;
        
        public BusesController(IBusActions busActions)
        {
            this.busActions = busActions;
        }
        // GET: api/Buses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var buses = await busActions.GetAllBusesAsync();

            if (buses == null)
            {
                return NotFound($"Problemes with getting buses");
            }
            var result = buses;

            return Ok(result);
        }

        // GET: api/Buses/5
        [HttpGet("{id}", Name = "GetBus")]
        public async Task<IActionResult> Get(int id)
        {

            var bus = await busActions.GetBusAsync(id);
            if (bus == null)
            {

                return NotFound($"Problemes finding plane with id:{id}");
            }
            return Ok(bus);
        }



        [HttpGet("{test}/{id}", Name = "GetAll")]
        public IEnumerable<string> GetAll(string test, int id)
        {
            return new string[] { "value1 of plane api", "value2 of plane api" };
        }

        [HttpGet("companies/{company}", Name = "GetBusesByCompany")]
        public async Task<IActionResult> GetBusesByCompany(string company, int pageNum = 1, int pageSize = 10)
        {
            var buses = await busActions.GetAllBusesByCompany(company,pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {company}");
            }
            var result = buses;

            return Ok(result);
        }

        [HttpGet("routes", Name = "GetBusesByRoute")]
        public async Task<IActionResult> GetBusesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            var buses = await busActions.GetAllBusesByRoute(inCity,outCity,pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = buses;

            return Ok(result);
        }

        //api/buses/priceRange?minPrice=100&maxPrice=200
        [HttpGet("priceRange", Name = "GetBusesByPrice")]
        public async Task<IActionResult> GetAllBusesByPrice(long minPrice, long? maxPrice, int pageNum = 1, int pageSize = 10)
        {


            var buses = await busActions.GetAllBusesByPrice(minPrice, maxPrice,pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {minPrice} and {maxPrice}");
            }
            var result = buses;

            return Ok(result);
        }
        //api/buses/cheapestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("cheapestRoute", Name = "GetCheapestBuses")]
        
        public async Task<IActionResult> GetCheapestBuses(string inCity, string outCity,int pageNum = 1, int pageSize = 10)
        {

            var buses = await busActions.GetCheapestBuses(inCity, outCity,pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = buses;

            return Ok(result);
        }

        //api/buses/fastestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("fastestRoute", Name = "GetFastestBuses")]

        public async Task<IActionResult> GetFastestBuses(string inCity, string outCity,int pageNum = 1, int pageSize = 10)
        {

            var buses = await busActions.GetFastestBuses(inCity, outCity, pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result =buses;

            return Ok(result);
        }

        // POST: api/Buses
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
                var entity =bus;
                var newEntity = await busActions.AddBusAsync(entity);
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

        // PUT: api/Buses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bus bus)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findBus = await busActions.GetBusAsync(id);
            if (findBus == null)
            {
                return NotFound($"Problemes finding {id}");
            }


            try
            {
                var entity = bus;
                entity.Id = id;
                await busActions.UpdateBusAsync(entity);
                result = Ok(bus);
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

            var deletedBus= await busActions.DeleteBusAsync(id);
            if (deletedBus == null)
            {
                return NotFound($"Problemes deleting {id}");
            }

            return Ok(deletedBus);
        }
    }
}
