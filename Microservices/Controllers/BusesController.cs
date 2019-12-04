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
        private readonly IMapper mapper; 
        
        public BusesController(IMapper mapper, IBusActions busActions)
        {
            this.busActions = busActions;
            this.mapper = mapper;
        }
        // GET: api/Buses
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1 of bus api", "value2 of bus api" };
        }

        // GET: api/Buses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {

            return "value" + id.ToString() ;
        }



        [HttpGet("{test}/{id}", Name = "GetAll")]
        public async Task<IActionResult> GetAll(string test, int id)
        {
            var buses = await busActions.GetAllBusesAsync();
            
            if (buses == null)
            {
                return NotFound($"Problemes with {id}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }

        [HttpGet("companies/{company}", Name = "GetBusesByCompany")]
        public async Task<IActionResult> GetBusesByCompany(string company)
        {
            var buses = await busActions.GetAllBusesByCompany(company);

            if (buses == null)
            {
                return NotFound($"Problemes with {company}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }

        [HttpGet("routes/{inCity}&{outCity}", Name = "GetBusesByRoute")]
        public async Task<IActionResult> GetBusesByRoute(string inCity, string outCity)
        {
            var buses = await busActions.GetAllBusesByRoute(inCity,outCity);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }

        //api/buses/priceRange?minPrice=100&maxPrice=200
        [HttpGet("priceRange", Name = "GetBusesByPrice")]
        public async Task<IActionResult> GetAllBusesByPrice(long minPrice, long? maxPrice)
        {


            var buses = await busActions.GetAllBusesByPrice(minPrice, maxPrice);

            if (buses == null)
            {
                return NotFound($"Problemes with {minPrice} and {maxPrice}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }
        //api/buses/cheapestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("cheapestRoute", Name = "GetCheapestBuses")]
        
        public async Task<IActionResult> GetCheapestBuses(string inCity, string outCity,int size=10)
        {

            var buses = await busActions.GetCheapestBuses(inCity, outCity,size);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }

        //api/buses/fastestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("fastestRoute", Name = "GetFastestBuses")]

        public async Task<IActionResult> GetFastestBuses(string inCity, string outCity, int size = 10)
        {

            var buses = await busActions.GetFastestBuses(inCity, outCity, size);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Bus>>(buses);

            return Ok(result);
        }

        // POST: api/Buses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = mapper.Map<Bus>(dto);
                var newEntity = await busActions.AddBusAsync(entity);
                result = CreatedAtAction(nameof(Post), mapper.Map<BusDto>(newEntity));
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
