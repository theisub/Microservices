using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlaneAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlaneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly IPlaneActions planeActions;
        private readonly IMapper mapper;

        public PlanesController(IMapper mapper, IPlaneActions planeActions)
        {
            this.planeActions = planeActions;
            this.mapper = mapper;
        }
        // GET: api/Planes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1 of plane api", "value2 of plane api" };
        }

        // GET: api/Buses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {

            return "value" + id.ToString();
        }



        [HttpGet("{test}/{id}", Name = "GetAll")]
        public async Task<IActionResult> GetAll(string test, int id)
        {
            var planes = await planeActions.GetAllPlanesAsync();

            if (planes == null)
            {
                return NotFound($"Problemes with {id}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(planes);

            return Ok(result);
        }

        [HttpGet("companies/{company}", Name = "GetPlanesByCompany")]
        public async Task<IActionResult> GetPlanesByCompany(string company)
        {
            var planes = await planeActions.GetAllPlanesByCompany(company);

            if (planes == null)
            {
                return NotFound($"Problemes with {company}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(planes);

            return Ok(result);
        }

        [HttpGet("routes/{inCity}&{outCity}", Name = "GetPlanesByRoute")]
        public async Task<IActionResult> GetPlanesByRoute(string inCity, string outCity)
        {
            var planes = await planeActions.GetAllPlanesByRoute(inCity, outCity);

            if (planes == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(planes);

            return Ok(result);
        }

        //api/planes/priceRange?minPrice=100&maxPrice=200
        [HttpGet("priceRange", Name = "GetAllPlanesByPrice")]
        public async Task<IActionResult> GetAllPlanesByPrice(long minPrice, long? maxPrice)
        {


            var planes = await planeActions.GetAllPlanesByPrice(minPrice, maxPrice);

            if (planes == null)
            {
                return NotFound($"Problemes with {minPrice} and {maxPrice}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(planes);

            return Ok(result);
        }
        //api/planes/cheapestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("cheapestRoute", Name = "GetCheapestPlanes")]

        public async Task<IActionResult> GetCheapestPlanes(string inCity, string outCity, int size = 10)
        {

            var planes = await planeActions.GetCheapestPlanes(inCity, outCity, size);

            if (planes == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(planes);

            return Ok(result);
        }

        //api/planes/fastestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("fastestRoute", Name = "GetFastestPlanes")]

        public async Task<IActionResult> GetFastestPlanes(string inCity, string outCity, int size = 10)
        {

            var buses = await planeActions.GetFastestPlanes(inCity, outCity, size);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = mapper.Map<IEnumerable<Plane>>(buses);

            return Ok(result);
        }

        // POST: api/planes
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
                var newEntity = await planeActions.AddPlaneAsync(entity);
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
