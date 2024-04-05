using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DtoModels.Order;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrdersByBuyer;
using Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiComplexFood.Controllers;

namespace ProjectStructure.MSTests
{
    [TestClass]
    public class OrderControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<OrderController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<OrderController>>();

        }

        [TestMethod]
        public async Task Should_Get_All_Orders_GetAllOrders()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderDto>()
                {
                    new OrderDto
                    {
                        BuyerId = 1,
                        Code = "fffaaa2",
                        DatePlaced = DateTime.UtcNow,
                        Discount= 10,
                        Id =1,
                        Status = OrderStatus.Placed,
                        TotalPrice = 32,
                    }
                });


            //ACT
            var controller = new OrderController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrders();
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        } 

        [TestMethod]
        public async Task Should_NOT_Get_All_Orders_GetAllOrders()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderDto>()
                );


            //ACT
            var controller = new OrderController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrders();
            var notFoundResult = result.Result as NotFoundResult;
            
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Get_All_Orders_By_Buyer_ID_GetAllOrdersByBuyerId()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetOrdersByBuyerIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderDto>()
                {
                    new OrderDto
                    {
                        BuyerId = 1,
                        Code = "fffaaa2",
                        DatePlaced = DateTime.UtcNow,
                        Discount= 10,
                        Id =1,
                        Status = OrderStatus.Placed,
                        TotalPrice = 32,
                    }
                });

            //ACT
            var controller = new OrderController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrdersByBuyerId(1);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        } 

        [TestMethod]
        public async Task Should_NOT_Get_All_Orders_By_Buyer_ID_GetAllOrdersByBuyerId()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetOrdersByBuyerIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderDto>()
                );

            //ACT
            var controller = new OrderController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllOrdersByBuyerId(1);
            var notFoundResult = result.Result as NotFoundResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        }
    }
}
