using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DtoModels.OrderItem;
using System.Collections.Generic;
using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Customer.OrderItems.Queries.GetAllItems;
using Application.Features.Customer.OrderItems.Queries.GetALLItemsByOrderId;


namespace ProjectStructure.UnitTests
{
    [TestClass]
    public class OrderItemControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<OrderItemController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<OrderItemController>>();

        }

        [TestMethod]
        public async Task Should_Get_All_Items_GetAllOrderItems()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllItemsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        OrderItemId = 1,
                    }
                });


            //ACT
            var controller = new OrderItemController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrderItems();
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Get_All_Orders_GetAllOrders()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllItemsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderItemDto>()
                );


            //ACT
            var controller = new OrderItemController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrderItems();
            var notFoundResult = result.Result as NotFoundResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Get_All_Items_By_Buyer_ID_GetAllItemsByBuyerId()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllItemsByOrderIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderItemDto>()
                {
                    new OrderItemDto
                    {
                        OrderItemId = 1,
                    }
                });

            //ACT
            var controller = new OrderItemController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrderItemsByOrderId(1);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Get_All_Items_By_Buyer_ID_GetAllItemsByBuyerId()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllItemsByOrderIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderItemDto>()
                );

            //ACT
            var controller = new OrderItemController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrderItemsByOrderId(1);
            var notFoundResult = result.Result as NotFoundResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }
    }
}
