using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DtoModels.Product;
using Application.DtoModels.ShoppingCartItem;
using Application.Features.ShoppingItems.Commands.CreateShoppingItem;
using Application.Features.ShoppingItems.Queries.GetAllProductsByBuyerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiComplexFood.Controllers;

namespace ProjectStructure.MSTests
{
    [TestClass]
    public class ShoppingItemControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<ShoppingItemController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ShoppingItemController>>();

        }

        [TestMethod]
        public async Task Should_Create_ShoppingItem_CreateShoppingItemCommand()
        {
            //ARANGE
            var newItem = new ShoppingCartItemDto
            {
                Cantity = 2,
                ProductId = 1,
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateShoppingItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);


            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.Create_ShoppingItem_CreateShoppingItemCommand(1, newItem);
            var okResult = result.Result as CreatedAtRouteResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.Created, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Create_ShoppingItem_CreateShoppingItemCommand()
        {
            //ARANGE
            var newItem = new ShoppingCartItemDto
            {
                Cantity = 2,
                ProductId = 1,
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateShoppingItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(-2);


            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.Create_ShoppingItem_CreateShoppingItemCommand(1, newItem);
            var badResult = result.Result as BadRequestObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Delete_ShoppingItem_DeleteShoppingItemCommand()
        {
            //ARANGE
            var newItem = new ShoppingCartItemDto
            {
                Cantity = 2,
                ProductId = 1,
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateShoppingItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(-4);


            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.Create_ShoppingItem_CreateShoppingItemCommand(1, newItem);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_FOUND_ShoppingItem_ShoppingItemCommand()
        {
            //ARANGE
            var newItem = new ShoppingCartItemDto
            {
                Cantity = 2,
                ProductId = 1,
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateShoppingItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(-1);

            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.Create_ShoppingItem_CreateShoppingItemCommand(1, newItem);
            var notFoundResult = result.Result as NotFoundObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Get_ALL_Items_From_Cart_By_Buyer_GetAllShoppingItemsByBuyerId()
        {
            //ARANGE
            var products = new List<ProductFromCartDto>()
            {
               new ProductFromCartDto
               {
                   Id = 1,
               }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllProductsByBuyerIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);


            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.GetAllProductsByBuyerId(1);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        
        [TestMethod]
        public async Task Should_NOT_Get_ALL_Items_From_Cart_By_Buyer_GetAllShoppingItemsByBuyerId()
        {
            //ARANGE
            var products = new List<ProductFromCartDto>();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllProductsByBuyerIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);


            //ACT
            var controller = new ShoppingItemController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.GetAllProductsByBuyerId(1);
            var notFoundResult = result.Result as NotFoundObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

    }
}
