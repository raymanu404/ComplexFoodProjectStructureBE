using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Products.Queries.GetAllProducts;

using Application.DtoModels.Product;
using System.Collections.Generic;
using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace ProjectStructure.UnitTests
{
    [TestClass]
    public class ProductControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<ProductController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ProductController>>();

        }

        [TestMethod]
        public async Task Should_Get_All_Products_GetAllProducts()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = 1
                    }
                });


            //ACT
            var controller = new ProductController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.GetAllProducts();
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Get_All_Orders_GetAllOrders()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProductDto>()
                );


            //ACT
            var controller = new ProductController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.GetAllProducts();
            var notFoundResult = result.Result as NotFoundResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

    }
}
