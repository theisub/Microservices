using System;
using Xunit;
using BusAPI.Model;
using BusAPI.Controllers;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesAsync())
                .ReturnsAsync(BusesListTaskReturn())
                .Verifiable();

            var response = await controller.GetAll() as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(response.StatusCode, 200);



        }

        //GetBusesByCompany
        [Fact]
        public async Task Test2()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesByCompany("TourCompany",1,10))
                .ReturnsAsync(BusesSearch())
                .Verifiable();

            var response = await controller.GetBusesByCompany("TourCompany", 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200,response.StatusCode);

            var commandMock2 = new Mock<IBusActions>();

            commandMock2.
               Setup(_ => _.GetAllBusesByCompany("TourCompany112", 1, 10))
               .ReturnsAsync(()=>null)
               .Verifiable();
            var response2 = await controller.GetBusesByCompany("TourCompany112", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404,response2.StatusCode);

        }

        [Fact]
        public async Task Test3()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesByRoute("Omsk", "Berlin",1, 10))
                .ReturnsAsync(BusesByRoute())
                .Verifiable();

            var response = await controller.GetBusesByRoute("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200, response.StatusCode);

            var commandMock2 = new Mock<IBusActions>();

            commandMock2.
               Setup(_ => _.GetAllBusesByRoute("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response2 = await controller.GetBusesByRoute("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response2.StatusCode);

        }


        private List<Bus> BusesListTaskReturn()
        {
            List<Bus> bus = new List<Bus>()
            {
                new Bus() { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false },
                new Bus() { BusCompany = "TourCompany2", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Bordeaux", Price = 66, TravelTime = 94, Transit = false },
                new Bus() { BusCompany = "TourCompany3", InCountry = "Russia", OutCountry = "Germany", InCity = "Omsk", OutCity = "Berlin", Price = 148, TravelTime = 210, Transit = false }
            };

            return bus;

        }

        private IEnumerable<Bus> BusesSearch()
        {
            IEnumerable<Bus> bus = new List<Bus>()
            {
                new Bus() { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false }
              
            };

            return bus;

        }

        private IEnumerable<Bus> BusesByRoute()
        {
            IEnumerable<Bus> bus = new List<Bus>()
            {
                new Bus() { BusCompany = "TourCompany3", InCountry = "Russia", OutCountry = "Germany", InCity = "Omsk", OutCity = "Berlin", Price = 148, TravelTime = 210, Transit = false }

            };

            return bus;

        }


    }
}
