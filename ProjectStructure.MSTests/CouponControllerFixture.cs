using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Coupons.Queries.GetCouponsByBuyerId;
using Application.Features.Coupons.Commands.CreateCoupon;
using Application.DtoModels.Coupon;

using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;

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
                .Verifiable();


            //ACT
            var controller = new CouponController(_mockMediator.Object, _mockLogger.Object);
            await controller.GetAllCouponsByBuyerId(1);

            //ASSERT
            _mockMediator.Verify(x => x.Send(It.IsAny<GetCouponsByBuyerIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Create_Coupons_By_BuyerId_CreateCouponCommand()
        {
            //ARANGE
            var coupons = new CouponCreateDto
            {
                Type = Domain.Models.Enums.TypeCoupons.ThirtyProcent
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateCouponCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();


            //ACT
            var controller = new CouponController(_mockMediator.Object, _mockLogger.Object);
            await controller.CreateCoupons(1, coupons);

            //ASSERT
            _mockMediator.Verify(x => x.Send(It.IsAny<CreateCouponCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
