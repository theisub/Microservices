using System;
using Xunit;
using FavoritesAPI.Services;
using FavoritesAPI.Model;
using FavoritesAPI.Controllers;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace WebApiTest
{
    public class FavoritesTests
    {
        [Fact]
        public async Task GetAllFavoritesSuccess()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            List<Favorites> favoritesListTest = new List<Favorites> { new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" },
                                                                      new Favorites { InCountry = "Germany", OutCountry = "Russia", InCity = "Berlin", OutCity = "Omsk" } 
                                                                    };
            var controller = new FavoritesController(mockRepository.Object);

            controller.ModelState.AddModelError("key", "error message");

            mockRepository.
                Setup(_ => _.Get())
                .ReturnsAsync(favoritesListTest)
                .Verifiable();

            IActionResult actionResult = await controller.Get();

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task GetAllFavoritesNotFound()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            List<Favorites> favoritesListTest = new List<Favorites> { new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" },
                                                                      new Favorites { InCountry = "Germany", OutCountry = "Russia", InCity = "Berlin", OutCity = "Omsk" }
                                                                    };
            var controller = new FavoritesController(mockRepository.Object);

            controller.ModelState.AddModelError("key", "error message");

            mockRepository.
                Setup(_ => _.Get())
                .ReturnsAsync(() => null)
                .Verifiable();

            IActionResult actionResult = await controller.Get();

            var createdResult = actionResult as NotFoundObjectResult;


            Assert.Equal(404, createdResult.StatusCode);


        }

        [Fact]
        public async Task GetOneFavoritesNotFound()
        {


            var mockRepository = new Mock<IFavoritesActions>();

            var controller = new FavoritesController(mockRepository.Object);


            mockRepository.
                Setup(_ => _.Get("1"))
                .ReturnsAsync(() => null)
                .Verifiable();

            IActionResult actionResult = await controller.Get("1");

            var createdResult = actionResult as NotFoundResult;


            Assert.Equal(404, createdResult.StatusCode);


        }
        [Fact]
        public async Task GetOneFavoritesSuccess()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);


            mockRepository.
                Setup(_ => _.Get("1"))
                .ReturnsAsync(FavoritesTest)
                .Verifiable();

            IActionResult actionResult = await controller.Get("1");

            var createdResult = actionResult as OkObjectResult;


            Assert.Equal(200, createdResult.StatusCode);


        }

        [Fact]
        public async Task PostFavoritesSuccess()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            var controller = new FavoritesController(mockRepository.Object);

            IActionResult actionResult = await controller.Create(new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" });
            var createdResult = actionResult as CreatedAtActionResult;

            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("Create", createdResult.ActionName);

        }

        [Fact]
        public async Task PostFavoritesDbException()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.Create(FavoritesTest))
                .ThrowsAsync(new DbUpdateException()).Verifiable();

            IActionResult actionResult = await controller.Create(FavoritesTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ConflictResult;

            Assert.Equal(409, createdResult.StatusCode);

        }

        [Fact]
        public async Task PostFavoritesInternalError()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.Create(FavoritesTest))
                .ThrowsAsync(new Exception()).Verifiable();

            IActionResult actionResult = await controller.Create(FavoritesTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            Assert.Equal(500, createdResult.StatusCode);


        }

        [Fact]
        public async Task PostFavoritesWrongModelError()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.Create(FavoritesTest))
                .ThrowsAsync(new Exception()).Verifiable();
            controller.ModelState.AddModelError("key", "error message");

            IActionResult actionResult = await controller.Create(FavoritesTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.BadRequestObjectResult;

            Assert.Equal(400, createdResult.StatusCode);


        }

       

        [Fact]
        public async Task PutException()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.Get("1"))
                .ReturnsAsync(() => null).Verifiable();

        

            // Act
            IActionResult actionResult = await controller.Update("1", FavoritesTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);


        }
        [Fact]
        public async Task PutSuccess()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            mockRepository.
                Setup(_ => _.Get("1"))
                .ReturnsAsync(FavoritesTest).Verifiable();



            // Act
            IActionResult actionResult = await controller.Update("1", FavoritesTest);
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NoContentResult;

            // Assert
            Assert.Equal(204, createdResult.StatusCode);


        }


        [Fact]
        public async Task DeleteWrongModel()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);
            controller.ModelState.AddModelError("key", "error message");

            // Act
            IActionResult actionResult = await controller.Delete("1");
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);


        }

        [Fact]
        public async Task DeleteSuccess()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" };
            var controller = new FavoritesController(mockRepository.Object);

            mockRepository.
                Setup(_ => _.Get("1"))
                .Returns(Task.FromResult(FavoritesTest));

            // Act
            IActionResult actionResult = await controller.Delete("1");
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NoContentResult;

            // Assert
            Assert.Equal(204, createdResult.StatusCode);


        }

        [Fact]
        public async Task DeleteNotFound()
        {


            var mockRepository = new Mock<IFavoritesActions>();
            Favorites FavoritesTest = new Favorites { InCountry = "Russia", OutCountry = "France", InCity = "Moscow", OutCity = "Paris" }; 
            var controller = new FavoritesController(mockRepository.Object);

            mockRepository.
                Setup(_ => _.Get("1"))
                .ReturnsAsync(() => null);

            // Act
            IActionResult actionResult = await controller.Delete("1");
            var createdResult = actionResult as Microsoft.AspNetCore.Mvc.NotFoundResult;

            // Assert
            Assert.Equal(404, createdResult.StatusCode);


        }
    }
}
