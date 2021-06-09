using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cherepko.Controllers;
using CherepkoLib.Entities;
using Xunit;

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

        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();
            controller.rods = TestData.GetRodsList();

            // Act
            var result = controller.Index(pageNo: page, group: null) as ViewResult;
            var model = result?.Model as List<Rod>;
            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].RodId);
        }

        [Fact]
        public void ControllerSelectsGroup()
        {

            // arrange
            var controller = new ProductController();
            var data = TestData.GetRodsList();
            controller.rods = data;
            var comparer = Comparer<Rod>
            .GetComparer((d1, d2) => d1.RodId.Equals(d2.RodId));
            // act
            var result = controller.Index(3) as ViewResult;
            var model = result.Model as List<Rod>;
            // assert
            Assert.Equal(2, model.Count);
            Assert.Equal(data[3], model[0], comparer);
        }

    }
}
