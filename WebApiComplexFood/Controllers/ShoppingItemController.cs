using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ShoppingItems.Commands;
using Domain.Models.Shopping;
using Application.DtoModels.ShoppingCartItemDto;

namespace WebApiComplexFood.Controllers
{

    [ApiController]
    [Route("shoppingItems")]
    public class ShoppingItemController : Controller
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;

        public ShoppingItemController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }
        //POST : /create_shoppingItem/{buyerId}
        [HttpPost("{buyerId}")]
        public async Task<ActionResult<ShoppingCartItem>> Create_ShoppingItem_CreateShoppingItemCommand(int buyerId,[FromBody]ShoppingCartItemDto request )
        {
            var command = new CreateShoppingItemCommand
            {
                BuyerId = buyerId,
                ProductId = request.ProductId,
                Amount = request.Amount,

            };

            var shoppingItem = await _mediator.Send(command);
            if(shoppingItem == -2)
            {
                return BadRequest("Insufficient funds!");
            }

            if(shoppingItem != -1 || shoppingItem != 0 && shoppingItem != -2)
            {
                return CreatedAtRoute(new { id = shoppingItem }, shoppingItem);
            }
            else
            {
                return NotFound("Buyer or Product not found!");
            }
           
        }


    }
}
