using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ShoppingItems.Commands;
using Domain.Models.Shopping;

namespace WebApiComplexFood.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShoppingItemController : Controller
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;

        public ShoppingItemController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }
        //POST : /create_shoppingItem
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItem>> Create_ShoppingItem_CreateShoppingItemCommand([FromBody] CreateShoppingItemCommand command )
        {
            var shoppingItem = await _mediator.Send(command);
            if(shoppingItem != -1 || shoppingItem != 0)
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
