using System;
using Xunit;
using PlaneAPI.Model;
using PlaneAPI.Controllers;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace WebApiTest
{
    public class PlaneTests
    {
        [Fact]
        public async Task GetAllPlanesSuccess()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllPlanesAsync())
                .ReturnsAsync(PlanesListTaskReturn())
                .Verifiable();

            IActionResult actionResult = await controller.GetAll();

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(200, createdResult.StatusCode);

        }

        [Fact]
        public async Task GetAllPlanesNotFound()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllPlanesAsync())
                .ReturnsAsync(() => null)
                .Verifiable();

            IActionResult actionResult = await controller.GetAll();

            var createdResult = actionResult as NotFoundObjectResult;


            Assert.Equal(404, createdResult.StatusCode);



        }


        //GetPlanesByCompany
        [Fact]
        public async Task GetPlanesByCompanySuccess()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllPlanesByCompany("TourCompany", 1, 10))
                .ReturnsAsync(PlanesSearch())
                .Verifiable();

            IActionResult actionResult = await controller.GetPlanesByCompany("TourCompany", 1, 10);

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(200, createdResult.StatusCode);
        }

        [Fact]
        public async Task GetPlanesByCompanyNotFound()
        {
            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);

            commandMock.
               Setup(_ => _.GetAllPlanesByCompany("TourCompany112", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();

            IActionResult actionResult = await controller.GetPlanesByCompany("TourCompany112", 1, 10);
            var createdResult = actionResult as NotFoundObjectResult;

            Assert.Equal(404, createdResult.StatusCode);

        }

        [Fact]
        public async Task GetAllPlanesSuccessTest()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllPlanesByRoute("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(PlanesByRoute())
                .Verifiable();

            var response = await controller.GetPlanesByRoute("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Plane>;

            Assert.Equal(200, response.StatusCode);
        }
        [Fact]
        public async Task GetAllPlanesTest()
        {

            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
               Setup(_ => _.GetAllPlanesByRoute("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetPlanesByRoute("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]


        public async Task GetAllPlanesByPriceSuccessTest()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetAllPlanesByPrice(100, 200, 1, 10))
                .ReturnsAsync(PlanesListTaskReturn())
                .Verifiable();

            var response = await controller.GetAllPlanesByPrice(100, 200, 1, 10) as OkObjectResult;

            var list = response.Value as List<Plane>;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPlanesByPriceNotFoundTest()
        {
            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
              Setup(_ => _.GetAllPlanesByPrice(100, 200, 1, 10))
              .ReturnsAsync(() => null)
              .Verifiable();

            var response = await controller.GetAllPlanesByPrice(100, 200, 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public async Task GetCheapestPlanesSuccessTest()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetCheapestPlanes("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(PlanesByRoute())
                .Verifiable();

            var response = await controller.GetCheapestPlanes("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Plane>;

            Assert.Equal(200, response.StatusCode);
        }
        [Fact]
        public async Task GetCheapestPlanesNotFoundTest()
        {
            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
               Setup(_ => _.GetCheapestPlanes("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetCheapestPlanes("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public async Task GetFastestPlanesSuccessTest()
        {


            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);
            commandMock.
                Setup(_ => _.GetFastestPlanes("Omsk", "Berlin", 1, 10))
                .ReturnsAsync(PlanesByRoute())
                .Verifiable();

            var response = await controller.GetFastestPlanes("Omsk", "Berlin", 1, 10) as OkObjectResult;

            var list = response.Value as List<Plane>;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetFastestPlanesNotFoundTest()
        {

            var commandMock = new Mock<IPlaneActions>();
            var controller = new PlanesController(commandMock.Object);

            commandMock.
               Setup(_ => _.GetFastestPlanes("Omsk11", "Berlin", 1, 10))
               .ReturnsAsync(() => null)
               .Verifiable();
            var response = await controller.GetFastestPlanes("Omsk11", "Berlin", 1, 10) as NotFoundObjectResult;

            Assert.Equal(404, response.StatusCode);

        }

        [Fact]
        public async Task PostPlanesTestSuccess()
        {


            var mockRepository = new Mock<IPlaneActions>();
            var controller = new PlanesController(mockRepository.Object);

            // Act
            IActionResult actionResult = await controller.Post(new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false });
            var createdResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("Post", createdResult.ActionName);

        }

        [Fact]
        public async Task PostPlanesDbException()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.AddPlaneAsync(PlaneTest))
                .ThrowsAsync(new DbUpdateException("error", new Exception())).Verifiable();
            // Act
            IActionResult actionResult = await controller.Post(PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ConflictResult;

            // Assert
            Assert.Equal(409, createdResult.StatusCode);

        }

        [Fact]
        public async Task PostInternalError()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.AddPlaneAsync(PlaneTest))
                .ThrowsAsync(new Exception("error", new Exception())).Verifiable();
            // Act
            IActionResult actionResult = await controller.Post(PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            // Assert
            Assert.Equal(500, createdResult.StatusCode);
            Assert.Equal("Error posting Plane!", createdResult.Value);


        }


        [Fact]
        public async Task PutNotFound()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetPlaneAsync(1))
                .ReturnsAsync(() => null).Verifiable();
            // Act
            IActionResult actionResult = await controller.Put(1, PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundObjectResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);
            Assert.Equal("Problemes finding 1", createdResult.Value);


        }

        [Fact]
        public async Task PutSuccess()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetPlaneAsync(1))
                .ReturnsAsync(PlaneTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdatePlaneAsync(PlaneTest))
                .Returns(Task.FromResult(PlaneTest));

            // Act
            IActionResult actionResult = await controller.Put(1, PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // Assert
            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutWrongModel()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            IActionResult actionResult = await controller.Put(1, PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;

            // Assert
            Assert.Equal(400, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutDbException()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetPlaneAsync(1))
                .ReturnsAsync(PlaneTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdatePlaneAsync(PlaneTest))
                .ThrowsAsync(new DbUpdateException("error", new Exception())).Verifiable();

            // Act
            IActionResult actionResult = await controller.Put(1, PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ConflictResult;

            // Assert
            Assert.Equal(409, createdResult.StatusCode);


        }

        [Fact]
        public async Task PutException()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.GetPlaneAsync(1))
                .ReturnsAsync(PlaneTest).Verifiable();

            mockRepository.
                Setup(_ => _.UpdatePlaneAsync(PlaneTest))
                .ThrowsAsync(new Exception("error")).Verifiable();

            // Act
            IActionResult actionResult = await controller.Put(1, PlaneTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            // Assert
            Assert.Equal(500, createdResult.StatusCode);


        }


        [Fact]
        public async Task DeleteWrongModel()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);
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


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);

            mockRepository.
                Setup(_ => _.DeletePlaneAsync(1))
                .Returns(Task.FromResult(PlaneTest));

            // Act
            IActionResult actionResult = await controller.Delete(1);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // Assert
            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task DeleteNotFound()
        {


            var mockRepository = new Mock<IPlaneActions>();
            Plane PlaneTest = new Plane { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false };
            var controller = new PlanesController(mockRepository.Object);

            mockRepository.
                Setup(_ => _.DeletePlaneAsync(1))
                .ReturnsAsync(() => null);

            // Act
            IActionResult actionResult = await controller.Delete(1);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundObjectResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);


        }







        private List<Plane> PlanesListTaskReturn()
        {
            List<Plane> Plane = new List<Plane>()
            {
                new Plane() { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false },
                new Plane() { PlaneCompany = "TourCompany2", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Bordeaux", Price = 66, TravelTime = 94, Transit = false },
                new Plane() { PlaneCompany = "TourCompany3", InCountry = "Russia", OutCountry = "Germany", InCity = "Omsk", OutCity = "Berlin", Price = 148, TravelTime = 210, Transit = false }
            };

            return Plane;

        }

        private IEnumerable<Plane> PlanesSearch()
        {
            IEnumerable<Plane> Plane = new List<Plane>()
            {
                new Plane() { PlaneCompany = "TourCompany1", InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris", Price = 115, TravelTime = 121, Transit = false }

            };

            return Plane;

        }

        private IEnumerable<Plane> PlanesByRoute()
        {
            IEnumerable<Plane> Plane = new List<Plane>()
            {
                new Plane() { PlaneCompany = "TourCompany3", InCountry = "Russia", OutCountry = "Germany", InCity = "Omsk", OutCity = "Berlin", Price = 148, TravelTime = 210, Transit = false }

            };

            return Plane;

        }
    }
}
