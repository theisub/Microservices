using System;
using System.Collections.Generic;
using FavoritesAPI.Model;
using FavoritesAPI.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FavoritesAPI.Controllers
{
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {

        private readonly FavoritesActions favoritesActions;

        public FavoritesController (FavoritesActions favoritesActions)
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
                return NotFound($"Problemes with getting buses");
            }
            var result = favorites;

            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetFavorite")]
        public async Task<IActionResult> Get(string id)
        {
            var favorite = await favoritesActions.Get(id);

            if (favorite == null)
            {
                return NotFound();
            }
            var result = favorite;

            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Favorites favorite)
        {
            await favoritesActions.Create(favorite);
            IActionResult result; 
            result = CreatedAtRoute("GetFavorite", new { id = favorite.Id.ToString() }, favorite);
            return Ok(result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]Favorites favoriteIn)
        {
            var favorite = favoritesActions.Get(id);


            if (favorite == null)
            {
                return NotFound();
            }
            favoriteIn.Id = id;
            favoritesActions.Update(id, favoriteIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var favorite = favoritesActions.Get(id);

            if (favorite == null)
            {
                return NotFound();
            }
            
            favoritesActions.Remove(id);

            return NoContent();
        }
    }
}
