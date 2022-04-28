#region MY IMPORTS
using Application.DtoModels.Buyer;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Features.Buyers.Commands.CreateBuyer;
using Application.Features.Buyers.Commands.DeleteBuyer;
using Application.Features.Buyers.Commands.UpdateBuyer;
using Application.Features.Buyers.Queries.LoginBuyer;
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

        public BuyerController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }

        //GET Buyer/buyer_login
        [HttpPost("login")]
        public async Task<ActionResult<BuyerDto>> GetBuyerLogin([FromBody]BuyerDtoLogin buyerLogin)
        {
            var queryBuyerLogin = await _mediator.Send(new LoginBuyerQuery { BuyerLogin = buyerLogin });
            if(queryBuyerLogin != null)
            {
                return Ok(queryBuyerLogin);
            }
            else
            {
                return NotFound("Buyer doesn't exist!");
            }
            
        }

        //GET: Buyers
        [HttpGet]
        public async Task<ActionResult<IList<BuyerDto>>> GetAllBuyers()
        {

            var querySelectAllBuyers = new GetBuyersListQuery();
            var buyers = await _mediator.Send(querySelectAllBuyers);

            return Ok(buyers);
        }

        //POST BuyerController/id = id
        [HttpPost]
        public async Task<ActionResult<BuyerDto>> CreateBuyer([FromBody] CreateBuyerCommand command)
        {
            var buyer = await _mediator.Send(command);
            return CreatedAtRoute(new { id = buyer.Id }, buyer);
        }
        
        //DELETE /{id}
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

        //PATCH /update-buyer/{buyerId}
        [HttpPatch("update/{buyerId}")]
        public async Task<ActionResult<BuyerDto>> UpdateBuyer(int buyerId, [FromBody]BuyerDto updateBuyer)
        {

            var command = new UpdateBuyerCommand
            {
                BuyerId = buyerId,
                Buyer = updateBuyer
            };
           
            var buyer = await _mediator.Send(command);

            return Ok(buyer);
        }

        //PATCH /confirm-buyer/{buyerId}

        [HttpPatch("confirm/{buyerId}")]
        public async Task<ActionResult<BuyerDto>> ConfirmBuyer(int buyerId)
        {

            var command = new ConfirmBuyerCommand
            {
                BuyerId = buyerId,
                Confirmed = true
            };

            var buyer = await _mediator.Send(command);

            return Ok(buyer);
        }

        //PATCH /deposit-balance/{buyerId}

        [HttpPatch("deposit-balance/{buyerId}")]
        public async Task<ActionResult<BuyerDto>> DepositBalanceBuyer(int buyerId, [FromBody]Balance balance)
        {

            var command = new DepositBalanceBuyerCommand
            {
                BuyerId = buyerId,
                Balance = balance
            };

            var buyer = await _mediator.Send(command);

            return Ok(buyer);
        }

     


    }
}