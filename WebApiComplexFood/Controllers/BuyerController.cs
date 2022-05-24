#region MY IMPORTS
using Application.DtoModels.Buyer;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Features.Buyers.Commands.CreateBuyer;
using Application.Features.Buyers.Commands.DeleteBuyer;
using Application.Features.Buyers.Commands.UpdateBuyer;
using Application.Features.Buyers.Queries.LoginBuyer;
using Application.Features.Buyers.Queries.GetBuyerById;
using Domain.ValueObjects;
#endregion
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Models.Roles;
using System.Net;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("buyers")]
    public class BuyerController : ControllerBase
    {
       
        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;

        public BuyerController(IMediator mediator, ILogger<BuyerController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //GET buyers/login
        [HttpPost("login")]
        public async Task<ActionResult<BuyerDto>> GetBuyerLogin([FromBody]BuyerLoginDto buyerLogin)
        {
            var queryBuyerLogin = await _mediator.Send(new LoginBuyerQuery { BuyerLogin = buyerLogin });
            if(queryBuyerLogin != null)
            {
                return Ok(queryBuyerLogin);
            }
            else
            {
                return NotFound("Email/Password invalid!");
            }
            
        }

        //GET: buyers/{buyerId}
        [HttpGet("{buyerId}")]
        public async Task<ActionResult<BuyerDto>> GetBuyerById(int buyerId)
        {
            var querySelectBuyerById = new GetBuyerByIdQuery
            {
                BuyerId = buyerId
            };
            var getBuyer =  await _mediator.Send(querySelectBuyerById);
            if(getBuyer.Id != 0)
            {
                return Ok(getBuyer);
            }
            else
            {
                return NotFound();
            }
           
        }

        //GET: buyers
        [HttpGet]
        public async Task<ActionResult<IList<BuyerDto>>> GetAllBuyers()
        {

            var querySelectAllBuyers = new GetBuyersListQuery();
            var buyers = await _mediator.Send(querySelectAllBuyers);

            return Ok(buyers);
        }

        //POST buyers/register/email = email
        [HttpPost("register")]
        public async Task<ActionResult<BuyerRegisterDto>> RegisterBuyer([FromBody] BuyerRegisterDto newBuyer)
        {
            var command = new CreateBuyerCommand()
            {
                Buyer = newBuyer
            };

            var buyer = await _mediator.Send(command);
            if(buyer != null)
            {
                return CreatedAtRoute(new { email = buyer.Email }, buyer);
            }
            else
            {
                return BadRequest("Email invalid!");
            }
            
        }
        
        //DELETE buyers/{id}
        [HttpDelete("{id}")]
        public async Task<NoContentResult> DeleteBuyer(int id)
        {
            var command = new DeleteBuyerByIdCommand()
            {
                BuyerId = id
            };

            await _mediator.Send(command);
            return NoContent();
        }

        //Put buyers/update/{buyerId}
        [HttpPut("update/{buyerId}")]
        public async Task<ActionResult> UpdateBuyer(int buyerId, [FromBody]BuyerUpdateDto updateBuyer)
        {

            var command = new UpdateBuyerCommand
            {
                BuyerId = buyerId,
                Buyer = updateBuyer
            }; 
           
            await _mediator.Send(command);
            return Ok();
        }

        //PATCH buyers/confirm/{buyerId}
        [HttpPatch("confirm/{buyerId}")]
        public async Task<ActionResult<string>> ConfirmBuyer(int buyerId,[FromBody] BuyerConfirmDto confirmBuyer)
        {

            var command = new ConfirmBuyerCommand
            {
                BuyerId = buyerId,
                Buyer = confirmBuyer
            };

            var confirmationMessage = await _mediator.Send(command);
            if(confirmationMessage.Equals("Buyer-ul a fost confirmat cu succes!"))
            {
                return Ok(confirmationMessage);
            }
            else
            {
                return BadRequest(confirmationMessage);
            }
           
        }

        //PATCH /deposit-balance/{buyerId}

        [HttpPatch("deposit-balance/{buyerId}")]
        public async Task<ActionResult<string>> DepositBalanceBuyer(int buyerId, [FromBody]float balance)
        {

            var command = new DepositBalanceBuyerCommand
            {
                BuyerId = buyerId,
                Balance = balance
            };

            var depositMessage = await _mediator.Send(command);
            return Ok(depositMessage);
        }

        //PATCH /update-password/{buyerId}

        [HttpPatch("update-password/{buyerId}")]
        public async Task<ActionResult<string>> UpdatePasswordBuyer(int buyerId, [FromBody]BuyerUpdatePasswordDto updatePassword)
        {

            var command = new UpdatePasswordBuyerCommand
            {
                BuyerId = buyerId,
                BuyerUpdatePassword = updatePassword
            };

            var updatePasswordMessage = await _mediator.Send(command);
            return Ok(updatePasswordMessage);
        }

    }
}