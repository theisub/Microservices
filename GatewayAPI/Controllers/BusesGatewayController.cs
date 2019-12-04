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
            return "value etogateway blet";
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
