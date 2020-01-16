using System;
using System.Collections.Generic;
using FavoritesAPI.Model;
using FavoritesAPI.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FavoritesAPI.Controllers
{
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {

        private readonly IFavoritesActions favoritesActions;

        public FavoritesController (IFavoritesActions favoritesActions)
        {
            this.favoritesActions = favoritesActions;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var favorites = await favoritesActions.Get();
            if (favorites == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Get method failed");
                return NotFound($"Problemes with getting buses");
            }
            var result = favorites;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Get method activated");
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetFavorite")]
        public async Task<IActionResult> Get(string id)
        {
            var favorite = await favoritesActions.Get(id);

            if (favorite == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("GetByID method failed");
                return NotFound();
            }
            var result = favorite;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("GetByID method activated");
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Favorites favorite)
        {
            // Auth
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            IActionResult result;
            
            try
            {

                var entity = favorite;
                var newEntity = await favoritesActions.Create(favorite);
                result = CreatedAtAction(nameof(Create), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method activated");
            }
            catch(DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method failed: dbException");


            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Post method failed: Exception");
            }

            return result;

        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]Favorites favoriteIn)
        {

            // Auth
            var favorite = await favoritesActions.Get(id);


            if (favorite == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Put method failed");
                return NotFound();
            }
            favoriteIn.Id = id;
            favoritesActions.Update(id, favoriteIn);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Put method activated");
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Auth
            var favorite = await favoritesActions.Get(id);

            if (favorite == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Delete method failed");
                return NotFound();
            }
            
            favoritesActions.Remove(id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Delete method activated");

            return NoContent();
        }
    }
}
