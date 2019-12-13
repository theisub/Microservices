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

        public PlanesController(IPlaneActions planeActions)
        {
            this.planeActions = planeActions;
        }
        // GET: api/Planes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var planes = await planeActions.GetAllPlanesAsync();

            if (planes == null)
            {
                return NotFound($"Problemes with getting planes");
            }
            var result = planes;

            return Ok(result);
        }

        // GET: api/planes/5
        [HttpGet("{id}", Name = "GetPlane")]
        public async Task<IActionResult> Get(int id)
        {

            var plane = await planeActions.GetPlaneAsync(id);
            if (plane == null)
            {

                return NotFound($"Problemes finding plane with id:{id}");
            }
            return Ok(plane);
               
        }



        [HttpGet("{test}/{id}", Name = "GetAllPlanes")]
        public IEnumerable<string> GetAll(string test, int id)
        {
            return new string[] { "value1 of plane api", "value2 of plane api" };
        }

        [HttpGet("companies/{company}", Name = "GetPlanesByCompany")]
        public async Task<IActionResult> GetPlanesByCompany(string company,int pageNum = 1, int pageSize = 10)
        {
            var planes = await planeActions.GetAllPlanesByCompany(company,pageNum,pageSize);

            if (planes == null)
            {
                return NotFound($"Problemes with {company}");
            }
            var result =planes;

            return Ok(result);
        }

        [HttpGet("routes", Name = "GetPlanesByRoute")]
        public async Task<IActionResult> GetPlanesByRoute(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {
            var planes = await planeActions.GetAllPlanesByRoute(inCity, outCity,pageNum,pageSize);

            if (planes == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = planes;

            return Ok(result);
        }

        //api/planes/priceRange?minPrice=100&maxPrice=200&pageSize=10&pageNum=1
        [HttpGet("priceRange", Name = "GetAllPlanesByPrice")]
        public async Task<IActionResult> GetAllPlanesByPrice(long? minPrice, long? maxPrice, int pageNum = 1, int pageSize = 10)
        {


            var planes = await planeActions.GetAllPlanesByPrice(minPrice, maxPrice,pageNum,pageSize);

            if (planes == null)
            {
                return NotFound($"Problemes with {minPrice} and {maxPrice}");
            }
            var result = planes;

            return Ok(result);
        }
        //api/planes/cheapestRoute?inCity=Moscow&outCity=Paris&pageSize=10&pageNum=1
        [HttpGet("cheapestRoute", Name = "GetCheapestPlanes")]

        public async Task<IActionResult> GetCheapestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {

            var planes = await planeActions.GetCheapestPlanes(inCity, outCity,pageNum,pageSize);

            if (planes == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = planes;

            return Ok(result);
        }

        //api/planes/fastestRoute?inCity=Moscow&outCity=Paris&size=10
        [HttpGet("fastestRoute", Name = "GetFastestPlanes")]

        public async Task<IActionResult> GetFastestPlanes(string inCity, string outCity, int pageNum = 1, int pageSize = 10)
        {

            var buses = await planeActions.GetFastestPlanes(inCity, outCity,pageNum,pageSize);

            if (buses == null)
            {
                return NotFound($"Problemes with {inCity} and {outCity}");
            }
            var result = buses;

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
                var entity = plane;
                var newEntity = await planeActions.AddPlaneAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
            }
            catch (DbUpdateException)
            {
                result = Conflict();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, "Error posting Plane!");
            }
            return result;

        }

        // PUT: api/planes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Plane plane)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findPlane = await planeActions.GetPlaneAsync(id);
            if (findPlane== null)
            {
                return NotFound($"Problemes finding {id}") ;
            }



            try
            {
                var entity = plane;
                entity.Id = id;
                await planeActions.UpdatePlaneAsync(entity);
                result = Ok(plane);
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

            var deletedPlane = await planeActions.DeletePlaneAsync(id);
            if (deletedPlane == null)
            {
                return NotFound($"Problemes deleting {id}");
            }

            return Ok(deletedPlane);
        }
    }
}
