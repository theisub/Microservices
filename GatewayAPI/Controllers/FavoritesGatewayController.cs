﻿using Microsoft.AspNetCore.Mvc;
using GatewayAPI.FavoritesClient;
using System;
using FavoritesAPI.Model;
using GatewayAPI.BusesClient;
using GatewayAPI.PlanesClient;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using BusAPI.Model;
using PlaneAPI.Model;

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesGatewayController : ControllerBase
    {
        private readonly IFavoritesHttpClient favoritesHttpClient;
        private readonly IBusesHttpClient busesHttpClient;
        private readonly IPlanesHttpClient planesHttpClient;
        private readonly IMapper mapper;

        public FavoritesGatewayController(IFavoritesHttpClient favoritesHttpClient, IBusesHttpClient busesHttpClient, IPlanesHttpClient planesHttpClient, IMapper mapper) 
        {
            this.favoritesHttpClient = favoritesHttpClient;
            this.busesHttpClient = busesHttpClient;
            this.planesHttpClient = planesHttpClient;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result;


            
            result = Ok(await favoritesHttpClient.GetAllAsync());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Get all method activated");


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
            Console.WriteLine($"Get method by Id activated with id {id}");
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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method is not with valid Model state");
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = favorite;
                var newEntity = await favoritesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method  successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {

                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method internal exception");
            }
            return result;

        }

        [HttpPost("AddFavorite", Name = "GetAndPostFavoriteGatewayAll")]
        public async Task<IActionResult> PostFavorite(string inCity = null, string outCity = null, string inCountry = null, string outCountry = null)
        {
            //https://localhost:44375/api/favoritesgateway/addfavorite?incity=Moscow&outcity=Paris

            IActionResult result;


            bool isPlaneActive = false;
            bool isBusActive = false;

            List<Plane> planesData = new List<Plane>();
            List<Bus> busesData = new List<Bus>();
            try
            {
                 planesData = await planesHttpClient.GetCheapestPlanes(inCity, outCity);
                 isPlaneActive = true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "GetCheapestPlanes failed to get")
                    planesData = new List<Plane>();
            }

            try
            {
                busesData = await busesHttpClient.GetCheapestBuses(inCity, outCity);
                isBusActive = true;
            }
            catch (Exception ex)
            {
               if (ex.Message == "GetCheapestBuses failed to get")
                    busesData = new List<Bus>();
            }

            if (isPlaneActive && isBusActive)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Both GET methods failed");
                return result;
            }


            Favorites favorites = new Favorites();

            //Favorites naFavorite = new Favorites { InCity = inCity, OutCity = outCity, InCountry = busesData[0].InCountry, OutCountry = busesData[0].OutCountry, BusesRoute = busik};
            IEnumerable<Route> busesRoute = mapper.Map<IEnumerable<Route>>(busesData);
            IEnumerable<Route> planesRoute = mapper.Map<IEnumerable<Route>>(planesData);
            Favorites newFavorite = new Favorites
            {
                InCity = inCity,
                OutCity = outCity,
                InCountry = inCountry,
                OutCountry = outCountry,
                BusesRoute = busesRoute,
                PlanesRoute = planesRoute
            };



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LoggerInfo:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Very important info");

            try
            {
                var entity = newFavorite;
                var newEntity = await favoritesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of GetAndPostFavoriteGatewayAll: successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of GetAndPostFavoriteGatewayAll: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of GetAndPostFavoriteGatewayAll: internal exception");
            }
            return result;
        }


        [HttpPost("AddBusAndFavorite", Name = "PostBusesFavoriteGatewayAll")]
        public async Task<IActionResult> PostBusesFavorite([FromBody] Bus bus)
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
                bus = newEntity;
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Buses of PostBusesFavoriteGatewayAll: successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Buses of PostBusesFavoriteGatewayAll: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Buses of PostBusesFavoriteGatewayAll: internal exception");
            }


            Favorites favorites = new Favorites();
            List<Route> emptyPlane = new List<Route>();
            List<Bus> buses = new List<Bus> { bus };
            IEnumerable<Route> busesRoute = mapper.Map<IEnumerable<Route>>(buses);
            Favorites newFavorite = new Favorites
            {
                InCity = bus.InCity,
                OutCity = bus.OutCity,
                InCountry = bus.InCountry,
                OutCountry = bus.OutCountry,
                BusesRoute = busesRoute,
                PlanesRoute = emptyPlane
            };



            try
            {
                var entity = newFavorite;
                var newEntity = await favoritesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of PostBusesFavoriteGatewayAll: successfully created entity{entity}");

            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of PostBusesFavoriteGatewayAll: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method  of PostBusesFavoriteGatewayAll: internal exception");
            }
            return result;
        }


        [HttpPost("AddPlaneAndFavorite", Name = "PostPlanesFavoriteGatewayAll")]
        public async Task<IActionResult> PostPlanesFavorite([FromBody] Plane plane)
        {
            //https://localhost:44375/api/favoritesgateway/addfavorite?incity=Moscow&outcity=Berlin
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var entity = plane;
                var newEntity = await planesHttpClient.PostAsync(entity);
                plane = newEntity;
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Planes of PostPlanesFavoriteGatewayAll: successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Planes of PostPlanesFavoriteGatewayAll: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method for Planes of PostPlanesFavoriteGatewayAll: internal exception");
            }


            Favorites favorites = new Favorites();
            List<Route> emptyBus= new List<Route>();

            List<Plane> planes = new List<Plane> { plane };
            IEnumerable<Route> planesRoute = mapper.Map<IEnumerable<Route>>(planes);
            Favorites newFavorite = new Favorites
            {
                InCity = plane.InCity,
                OutCity = plane.OutCity,
                InCountry = plane.InCountry,
                OutCountry = plane.OutCountry,
                PlanesRoute = planesRoute, 
                BusesRoute = emptyBus
            };


            try
            {
                var entity = newFavorite;
                var newEntity = await favoritesHttpClient.PostAsync(entity);
                result = CreatedAtAction(nameof(Post), newEntity);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of PostPlanesFavoriteGatewayAll: successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of PostPlanesFavoriteGatewayAll: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Post method of PostPlanesFavoriteGatewayAll: internal exception");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method successfully created entity{entity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Put method: internal exception");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Delete method successfully deleted entity {newEntity}");
            }
            catch (DbUpdateException)
            {
                result = Conflict();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Delete method: DbUpdateException");
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LoggerInfo:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Delete method: internal exception");
            }
            return result;
        }
    }

}
