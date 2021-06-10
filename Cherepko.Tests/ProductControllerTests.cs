using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cherepko.Controllers;
using CherepkoLib.Entities;
using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using CherepkoLib.Data;
using Microsoft.EntityFrameworkCore;

namespace Cherepko.Tests
{
    public class ProductControllerTests
    {

        //[Theory]
        //[InlineData(1, 3, 1)] 
        //[InlineData(2, 2, 4)] 
        //public void ControllerGetsProperPage(int page, int qty, int id)
        //{
        //    // Arrange
        //    var controller = new ProductController();
        //    controller.rods = new List<Rod>
        //    {
        //        new Rod{ RodId=1},
        //        new Rod{ RodId=2},
        //        new Rod{ RodId=3},
        //        new Rod{ RodId=4},
        //        new Rod{ RodId=5}
        //    };
        //    // Act
        //    var result = controller.Index(page) as ViewResult;
        //    var model = result?.Model as List<Rod>;
        //    // Assert
        //    Assert.NotNull(model);
        //    Assert.Equal(qty, model.Count);
        //    Assert.Equal(id, model[0].RodId);
        //}

        //[Theory]
        //[MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        //public void ControllerGetsProperPage(int page, int qty, int id)
        //{
        //    // Arrange
        //    var controller = new ProductController();
        //    controller.rods = TestData.GetRodsList();

        //    // Act
        //    var result = controller.Index(pageNo: page, group: null) as ViewResult;
        //    var model = result?.Model as List<Rod>;
        //    // Assert
        //    Assert.NotNull(model);
        //    Assert.Equal(qty, model.Count);
        //    Assert.Equal(id, model[0].RodId);
        //}

        //[Fact]
        //public void ControllerSelectsGroup()
        //{

        //    // arrange
        //    var controller = new ProductController();
        //    var data = TestData.GetRodsList();
        //    controller.rods = data;
        //    var comparer = Comparer<Rod>
        //    .GetComparer((d1, d2) => d1.RodId.Equals(d2.RodId));
        //    // act
        //    var result = controller.Index(3) as ViewResult;
        //    var model = result.Model as List<Rod>;
        //    // assert
        //    Assert.Equal(2, model.Count);
        //    Assert.Equal(data[3], model[0], comparer);
        //}

        //[Fact]
        //public void ControllerHttpContext()
        //{
        //    // Контекст контроллера
        //    var controllerContext = new ControllerContext();
        //    // Макет HttpContext
        //    var moqHttpContext = new Mock<HttpContext>();
        //    moqHttpContext.Setup(c => c.Request.Headers)
        //    .Returns(new HeaderDictionary());
        //    controllerContext.HttpContext = moqHttpContext.Object;
        //    var controller = new ProductController()
        //    { ControllerContext = controllerContext };
        //}

        DbContextOptions<ApplicationDbContext> options;
        public ProductControllerTests()
        {
            options =
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;
        }
        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            //заполнить DB данными
            using (var context = new ApplicationDbContext(options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(options))
            {
                // создать объект класса контроллера
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                // Act
                var result = controller.Index(group: null, pageNo: page) as ViewResult;
                var model = result?.Model as List<Rod>;
                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].RodId);
            }
            // удалить базу данных из памяти
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
            }
        }
        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            //заполнить DB данными
            using (var context = new ApplicationDbContext(options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                var comparer = Comparer<Rod>.GetComparer((d1, d2) => d1.RodId.Equals(d2.RodId));
                // act
                var result = controller.Index(2) as ViewResult;
                var model = result.Model as List<Rod>;
                // assert
                Assert.Equal(2, model.Count);
                Assert.Equal(context.Rods.ToArrayAsync().GetAwaiter()
                .GetResult()[2], model[0], comparer);
            }
        }

    }
}
