using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.DtoModels.Buyer;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Features.Buyers.Commands.CreateBuyer;
using Application.Features.Buyers.Commands.DeleteBuyer;
using Application.Features.Buyers.Commands.UpdateBuyer;
using Application.Features.Buyers.Queries.GetBuyerByEmail;
using Domain.ValueObjects;
using WebApiComplexFood.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Roles;
using Application.Features.Buyers.Queries.LoginBuyer;

namespace ProjectStructure.UnitTests
{
    [TestClass]
    public class BuyerControllerFixture
    {
        private static TestContext _testContext;
        private static Mock<IMediator> _mockMediator;
        private static Mock<ILogger<BuyerController>> _mockLogger;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<BuyerController>>();
        }

        [TestMethod]
        public async Task Should_Login_Buyer_LoginBuyerQuery()
        {
            //ARRANGE
            var buyer = new BuyerDto
            {
                Id = 1,
                Email = "email1@test.com",
                Password = "12345678a"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<LoginBuyerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(buyer);

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.LoginBuyer(new BuyerLoginDto { Email = buyer.Email, Password = buyer.Password});
            var okResult = result.Result as OkObjectResult;
            

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            
        }


        [TestMethod]
        public async Task Delete_Buyer_By_Id_DeleteBuyerByIdCommandCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteBuyerByIdCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);

            await controller.DeleteBuyer(2);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteBuyerByIdCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Register_Buyer_CreateBuyerCommandReturnCreatedStatus()
        {

            //ARRANGE
            var newBuyer = new BuyerRegisterDto
            {
                Email = "aurel@email.com",
                FirstName = "Marius",
                LastName = "Aurel",
                Password = "123ALAA2",
                Gender = "M",
                PhoneNumber = "0724274272"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    newBuyer.Email
                );
                           
            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.RegisterBuyer(newBuyer);

            var createdResult = result.Result as CreatedAtRouteResult;

            //ASSERT
            Assert.AreEqual(newBuyer.Email, createdResult.Value);
        }

        [TestMethod]
        public async Task Should_Update_Buyer_UpdateBuyerCommand()
        {
            var updateBuyer = new BuyerUpdateDto
            {
                LastName = "Albert",
                FirstName = "Marius",
                PhoneNumber = "07921412414"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    1
                );


            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.UpdateBuyer(1, updateBuyer);
            var okResult = result.Result as OkObjectResult;
          
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_Confirm_Buyer_ConfirmBuyerCommand()
        {
            //ARRANGE
            var confirmBuyer = new BuyerConfirmDto
            {
               ConfirmationCode = "af2fas"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ConfirmBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    "Buyer-ul a fost confirmat cu succes!"
                );

            //ACT

            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.ConfirmBuyer(1, confirmBuyer);
            var okResult = result.Result as OkObjectResult;
            
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }  

        [TestMethod]
        public async Task Should_NOT_Confirm_Buyer_ConfirmBuyerCommand()
        {
            //ARRANGE
            var confirmBuyer = new BuyerConfirmDto
            {
               ConfirmationCode = "af"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ConfirmBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    "Error"
                );

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.ConfirmBuyer(1, confirmBuyer);
            var badResult = result.Result as BadRequestObjectResult;
            
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_NOT_Deposit_Balance_DepositBalanceBuyerCommand()
        {
            //ARRANGE
            var depositBalance = new BuyerDepositBalanceDto
            {
               Balance = 100

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DepositBalanceBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Error");

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.DepositBalanceBuyer(1, depositBalance);
            var badResult = result.Result as BadRequestObjectResult;
                     
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        } 

        [TestMethod]
        public async Task Should_Update_Buyer_Password_UpdatePasswordBuyerCommand()
        {
            //ARRANGE
            var updatePassword = new BuyerUpdatePasswordDto
            {
               NewPassword = "ala",
               OldPassword = "bala"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdatePasswordBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("successfully!");

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.UpdatePasswordBuyer(1, updatePassword);
            var okResult = result.Result as OkObjectResult;
                     
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        } 

        [TestMethod]
        public async Task Should_NOT_Update_Buyer_Password_UpdatePasswordBuyerCommand()
        {
            //ARRANGE
            var updatePassword = new BuyerUpdatePasswordDto
            {
               NewPassword = "ala",
               OldPassword = "bala"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdatePasswordBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Random error");

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.UpdatePasswordBuyer(1, updatePassword);
            var badResult = result.Result as BadRequestObjectResult;
                     
            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }


        [TestMethod]
        public async Task Should_Get_Buyer_By_Email_GetBuyerByEmailQuery()
        {
            //ARRANGE
            var getBuyer = new BuyerForgotPasswordDto
            {
               Email = "random@email.com"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBuyerByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("A fost trimis mailul!");

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.ForgotPasswordBuyer(getBuyer);
            var okResult = result.Result as OkObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        [TestMethod]
        public async Task Should_NOT_Change_Password_ChangePasswordBuyer()
        {
            //ARRANGE
            var changePassword= new BuyerChangePasswordDto
            {
               Password = "alta parola123"

            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ChangePasswordBuyerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Password invalid!");

            //ACT
            var controller = new BuyerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.ChangePasswordBuyer(1, changePassword);
            var badResult = result.Result as BadRequestObjectResult;

            //ASSERT
            Assert.AreEqual((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }

    }
}
