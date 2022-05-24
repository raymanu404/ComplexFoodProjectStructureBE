//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using Moq;
//using MediatR;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.DtoModels.Buyer;
//using Application.Features.Buyers.Queries.GetBuyersList;
//using Application.Features.Buyers.Commands.CreateBuyer;
//using Application.Features.Buyers.Commands.DeleteBuyer;
//using Application.Features.Buyers.Commands.UpdateBuyer;
//using Domain.ValueObjects;
//using WebApiComplexFood.Controllers;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Mvc;
//using Domain.Models.Roles;

//namespace ProjectStructure.UnitTests
//{
//    [TestClass]
//    public class BuyerControllerFixture
//    {
//        private static TestContext _testContext;
//        private static Mock<IMediator> _mockMediator;
//        private static Mock<ILogger<BuyerController>> _mockLogger;

//        [ClassInitialize]
//        public static void ClassInitialize(TestContext testContext)
//        {
//            _testContext = testContext;
//            _mockMediator = new Mock<IMediator>();
//            _mockLogger = new Mock<ILogger<BuyerController>>();
//        }


//        [TestMethod]
//        public async Task Get_All_Buyers_GetBuyersListQuery()
//        {
//            //ARANGE
//            _mockMediator
//                .Setup(m => m.Send(It.IsAny<GetBuyersListQuery>(), It.IsAny<CancellationToken>()))
//                .Verifiable();

//            //ACT
//            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
//            await controller.GetAllBuyers();

//            //ASSERT
//            _mockMediator.Verify(x => x.Send(It.IsAny<GetBuyersListQuery>(), It.IsAny<CancellationToken>()), Times.Once());

//        }

//        [TestMethod]
//        public async Task Delete_Buyer_By_Id_DeleteBuyerByIdCommandCalled()
//        {
//            _mockMediator
//                .Setup(m => m.Send(It.IsAny<DeleteBuyerByIdCommand>(), It.IsAny<CancellationToken>()))
//                .Verifiable();

//            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);

//            await controller.DeleteBuyer(2);

//            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteBuyerByIdCommand>(), It.IsAny<CancellationToken>()), Times.Once());
//        }

//        [TestMethod]
//        public async Task Create_Buyer_CreateBuyerCommand()
//        {

//            //ARRANGE
//            var newBuyer = new BuyerDto
//            {
//                Email = "aurel@email.com",
//                FirstName = "Marius",
//                LastName = "Aurel",
//                Password = "123ALAA2",
//                Gender = "M",
//                PhoneNumber = "081231242"

//            };

//            //_mockMediator
//            //    .Setup(m => m.Send(It.IsAny<CreateBuyerCommand>(), It.IsAny<CancellationToken>()))
//            //    .ReturnsAsync(
//            //        new Buyer
//            //        {

//            //        }
//            //    );

//            //ACT
//            //var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
//            //var result = await controller.CreateBuyer(new CreateBuyerCommand() { Buyer = newBuyer });

//            //var createdResult = result.Result as CreatedAtRouteResult;


//            ////ASSERT
//            //Assert.AreEqual((int)HttpStatusCode.Created, createdResult.StatusCode);
//        }

//        [TestMethod]
//        public async Task Update_Buyer_UpdateBuyerCommand()
//        {
//            var newBuyer = new BuyerDto
//            {
//                Email = new Email("aurel1@email.com"),
//                FirstName = new Name("Marius"),

//            };

//            _mockMediator
//                .Setup(m => m.Send(It.IsAny<UpdateBuyerCommand>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(
//                    newBuyer
//                );


//            var controller = new BuyerController(_mockMediator.Object);

//            var result = await controller.UpdateBuyer(9, newBuyer);

//            var createdResult = result.Result as OkObjectResult;

//            Assert.AreEqual((int)HttpStatusCode.OK, createdResult.StatusCode);
//        }

//        [TestMethod]
//        public async Task Confirm_Buyer_ConfirmBuyerCommand()
//        {
//            //ARRANGE
//            _mockMediator
//                .Setup(m => m.Send(It.IsAny<ConfirmBuyerCommand>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(
//                    "Buyer-ul a fost confirmat cu succes!"
//                );

//            //ACT

//            var controller = new BuyerController(_mockMediator.Object);
//            var result = await controller.ConfirmBuyer(1);
//            var okResult = result.Result as OkObjectResult;

//            //ASSERT
//            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
//        }

//        [TestMethod]
//        public async Task Deposit_Balance_DepositBalanceBuyerCommand()
//        {
//            //ARRANGE
//            _mockMediator
//                .Setup(m => m.Send(It.IsAny<DepositBalanceBuyerCommand>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync("");

//            //ACT
//            var controller = new BuyerController(_mockMediator.Object);
//            var result = await controller.DepositBalanceBuyer(9, new Balance(400));
//            var okResult = result.Result as OkObjectResult;


//            //ASSERT
//            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
//        }

//    }
//}
