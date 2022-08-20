using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Coupons.Queries.GetCouponsByBuyerId;
using Application.Features.Coupons.Commands.CreateCoupon;
using Application.DtoModels.Coupon;
using System.Collections.Generic;
using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ProjectStructure.UnitTests
{
    [TestClass]
    public class CouponControllerFixture
    {

        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<CouponController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<CouponController>>();


        }

        [TestMethod]
        public async Task Get_Coupons_By_BuyerId_GetCouponsByBuyerIdQuery()
        {
            //ARANGE
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetCouponsByBuyerIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CouponDto>()
                {
                    new CouponDto
                    {
                        BuyerId = 1,
                        Code = "12314a",
                        DateCreated = DateTime.UtcNow,
                        Type = Domain.Models.Enums.TypeCoupons.TenProcent,
                    }
                });


            //ACT
            var controller = new CouponController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllCouponsByBuyerId(1);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_Coupons_By_BuyerId_CreateCouponCommand()
        {
            //ARANGE
            var coupons = new CouponCreateDto
            {
                Type = TypeCoupons.TenProcent
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateCouponCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Successfully");

            //ACT
            var controller = new CouponController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.CreateCoupons(1, coupons);
            var createdResult = result.Result as CreatedAtRouteResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.Created, createdResult.StatusCode);
        }

    }
}
