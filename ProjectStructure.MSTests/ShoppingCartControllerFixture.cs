using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DtoModels.ShoppingCart;
using Application.Features.ShoppingCarts.Commands.UpdateShoppingCartCommand;
using Application.Features.ShoppingCarts.Commands.DeleteShoppingCartCommand;
using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;

namespace ProjectStructure.UnitTests
{
    [TestClass]
    public class ShoppingCartControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<ShoppingCartController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ShoppingCartController>>();

        }

        [TestMethod]
        public async Task Should_Confirm_Cart_By_Buyer_ConfirmShoppingCart()
        {
            //ARANGE
            var confirmCart = new ShoppingCartConfirmDto
            {
                CouponCart = "asfasf1"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ConfirmShoppingCartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("OrderCode");


            //ACT
            var controller = new ShoppingCartController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.ConfirmShoppingCart(1, confirmCart);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Confirm_Cart_By_Buyer_ConfirmShoppingCart()
        {
            //ARANGE
            var confirmCart = new ShoppingCartConfirmDto
            {
                CouponCart = "asfasf1"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ConfirmShoppingCartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Some random error!");

            //ACT
            var controller = new ShoppingCartController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.ConfirmShoppingCart(1, confirmCart);
            var badResult = result.Result as BadRequestObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Delete_Cart_By_Buyer_DeleteCartByBuyerId()
        {
            //ARANGE
          
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteShoppingCartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Success!");


            //ACT
            var controller = new ShoppingCartController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.DeleteCartByBuyerId(1);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Delete_Cart_By_Buyer_DeleteCartByBuyerId()
        {
            //ARANGE
          
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteShoppingCartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("error!");


            //ACT
            var controller = new ShoppingCartController(_mockLogger.Object, _mockMediator.Object);
            var result = await controller.DeleteCartByBuyerId(1);
            var badResult = result.Result as BadRequestObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }

    }
}
