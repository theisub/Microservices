using Microsoft.AspNetCore.Mvc;
using GatewayAPI.FavoritesClient;
using System;
using FavoritesAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesGatewayController : ControllerBase
    {
        private readonly IFavoritesHttpClient favoritesHttpClient;

        public FavoritesGatewayController(IFavoritesHttpClient favoritesHttpClient) 
        {
            this.favoritesHttpClient = favoritesHttpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result;

            result = Ok(await favoritesHttpClient.GetAllAsync());

           

            return result;
            //return new string[] { "value1 etogateway  ", "value2 etogateway " };
        }

        // GET: api/favoritesgateway/5
        [HttpGet("{id}", Name = "GetFavoriteGateway")]
        public async Task<IActionResult> Get(int id)
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Very important info");
            IActionResult result;

            result = Ok(await favoritesHttpClient.GetAsync(id));

            return result;
        }

        // POST: api/favoritesgateway
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Favorites favorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = favorite;
                var newEntity = await favoritesHttpClient.PostAsync(entity);
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

        // PUT: api/favoritesgateway/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Favorites ravorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = ravorite;
                var newEntity = await favoritesHttpClient.PutAsync(id, entity);
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

        // DELETE: api/favoritesgateway/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {

                var newEntity = await favoritesHttpClient.DeleteAsync(id);
                result = CreatedAtAction(nameof(Delete), newEntity);
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
