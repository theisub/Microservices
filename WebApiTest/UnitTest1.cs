using System;
using Xunit;
using BusAPI.Model;
using BusAPI.Controllers;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;
using Microsoft.EntityFrameworkCore;

namespace WebApiTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetAllBusesSuccess()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesAsync())
                .ReturnsAsync(BusesListTaskReturn())
                .Verifiable();

            IActionResult actionResult = await controller.GetAll();

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(createdResult.StatusCode, 200);

        }

        [Fact]
        public async Task GetAllBusesNotFound()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesAsync())
                .ReturnsAsync(()=> null)
                .Verifiable();

            IActionResult actionResult = await controller.GetAll();

            var createdResult = actionResult as NotFoundObjectResult;


            Assert.Equal(createdResult.StatusCode, 404);



        }


        //GetBusesByCompany
        [Fact]
        public async Task GetBusesByCompanySuccess()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesByCompany("TourCompany", 1, 10))
                .ReturnsAsync(BusesSearch())
                .Verifiable();

            IActionResult actionResult = await controller.GetBusesByCompany("TourCompany", 1, 10);

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(200, createdResult.StatusCode);
        }

        [Fact]
        public async Task GetBusesByCompanyNotFound()
        {
            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);

            commandMock.
               Setup(_ => _.GetAllBusesByCompany("TourCompany112", 1, 10))
               .ReturnsAsync(()=>null)
               .Verifiable();

            IActionResult actionResult = await controller.GetBusesByCompany("TourCompany112", 1, 10);
            var createdResult = actionResult as NotFoundObjectResult;

            Assert.Equal(404,createdResult.StatusCode);

        }

        [Fact]
        public async Task GetAllBusesSuccessTest()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesByRoute("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(BusesByRoute())
                .Verifiable();

            var response = await controller.GetBusesByRoute("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200, response.StatusCode);
        }
        [Fact]
        public async Task GetAllBusesTest()
        {

            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
               Setup(_ => _.GetAllBusesByRoute("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetBusesByRoute("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]


        public async Task GetAllBusesByPriceSuccessTest()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllBusesByPrice(100, 200, 1, 10))
                .ReturnsAsync(BusesListTaskReturn())
                .Verifiable();

            var response = await controller.GetAllBusesByPrice(100, 200, 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAllBusesByPriceNotFoundTest()
        {
           var commandMock = new Mock<IBusActions>();
           var controller = new BusesController(commandMock.Object);
            commandMock.
              Setup(_ => _.GetAllBusesByPrice(100, 200, 1, 10))
              .ReturnsAsync(() => null)
              .Verifiable();

           var response = await controller.GetAllBusesByPrice(100, 200, 1, 10) as NotFoundObjectResult;

           Assert.Equal(404, response.StatusCode);

       }

        [Fact]
        public async Task GetCheapestBusesSuccessTest()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetCheapestBuses("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(BusesByRoute())
                .Verifiable();

            var response = await controller.GetCheapestBuses("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200, response.StatusCode);
        }
        [Fact]
        public async Task GetCheapestBusesNotFoundTest()
        {
            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
               Setup(_ => _.GetCheapestBuses("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetCheapestBuses("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public async Task GetFastestBusesSuccessTest()
        {


            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetFastestBuses("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(BusesByRoute())
                .Verifiable();

            var response = await controller.GetFastestBuses("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Bus>;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetFastestBusesNotFoundTest()
        {

            var commandMock = new Mock<IBusActions>();
            var controller = new BusesController(commandMock.Object);

            commandMock.
               Setup(_ => _.GetFastestBuses("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetFastestBuses("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public async Task PostBusesTestSuccess()
        {


            var mockRepository = new Mock<IBusActions>();
            var controller = new BusesController(mockRepository.Object);

            // Act
            IActionResult actionResult = await controller.Post(new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false});
            var createdResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("Post", createdResult.ActionName);

        }

        [Fact]
        public async Task PostBusesDbException()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.AddBusAsync(busTest))
                .ThrowsAsync(new DbUpdateException("error", new Exception())).Verifiable();
            // Act
            IActionResult actionResult = await controller.Post(busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ConflictResult;

            // Assert
            Assert.Equal(409, createdResult.StatusCode);

        }

        [Fact]
        public async Task PostInternalError()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.AddBusAsync(busTest))
                .ThrowsAsync(new Exception("error", new Exception())).Verifiable();
            // Act
            IActionResult actionResult = await controller.Post(busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            // Assert
            Assert.Equal(500, createdResult.StatusCode);
            Assert.Equal("Error posting bus!", createdResult.Value);


        }


        [Fact]
        public async Task PutNotFound()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetBusAsync(1))
                .ReturnsAsync(() => null).Verifiable();
            // Act
            IActionResult actionResult = await controller.Put(1,busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundObjectResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);
            Assert.Equal("Problemes finding 1", createdResult.Value);


        }

        [Fact]
        public async Task PutSuccess()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetBusAsync(1))
                .ReturnsAsync(busTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdateBusAsync(busTest))
                .Returns(Task.FromResult(busTest));
            
            // Act
            IActionResult actionResult = await controller.Put(1, busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // Assert
            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutWrongModel()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            controller.ModelState.AddModelError("key", "error message") ;

            // Act
            IActionResult actionResult = await controller.Put(1, busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;

            // Assert
            Assert.Equal(400, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutDbException()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetBusAsync(1))
                .ReturnsAsync(busTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdateBusAsync(busTest))
                .ThrowsAsync(new DbUpdateException("error", new Exception())).Verifiable();

            // Act
            IActionResult actionResult = await controller.Put(1, busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ConflictResult;

            // Assert
            Assert.Equal(409, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutException()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetBusAsync(1))
                .ReturnsAsync(busTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdateBusAsync(busTest))
                .ThrowsAsync(new Exception("error")).Verifiable();

            // Act
            IActionResult actionResult = await controller.Put(1, busTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            // Assert
            Assert.Equal(500, createdResult.StatusCode);


        }


        [Fact]
        public async Task DeleteWrongModel()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            IActionResult actionResult = await controller.Delete(1);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;

            // Assert
            Assert.Equal(400, createdResult.StatusCode);


        }

        [Fact]
        public async Task DeleteSuccess()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);
            
            mockRepository.
                Setup(_ => _.DeleteBusAsync(1))
                .Returns(Task.FromResult(busTest));

            // Act
            IActionResult actionResult = await controller.Delete(1);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // Assert
            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task DeleteNotFound()
        {


            var mockRepository = new Mock<IBusActions>();
            Bus busTest = new Bus { BusCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new BusesController(mockRepository.Object);

            mockRepository.
                Setup(_ => _.DeleteBusAsync(1))
                .ReturnsAsync(() => null);

            // Act
            IActionResult actionResult = await controller.Delete(1);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundObjectResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);


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
