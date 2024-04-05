using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Shopping;
using Application.DtoModels.Product;
using Application.DtoModels.ShoppingCartItem;

using Application.Features.ShoppingItems.Commands.CreateShoppingItem;
using Application.Features.ShoppingItems.Queries.GetAllProductsByBuyerId;


namespace WebApiComplexFood.Controllers
{

    [ApiController]
    [Route("shoppingItems")]
    public class ShoppingItemController : Controller
    {
        private readonly ILogger<ShoppingItemController> _logger;
        private readonly IMediator _mediator;

        public ShoppingItemController(ILogger<ShoppingItemController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //POST : create/{buyerId}
        [HttpPost("create/{buyerId}")]
        public async Task<ActionResult<ShoppingCartItem>> Create_ShoppingItem_CreateShoppingItemCommand(int buyerId,[FromBody]ShoppingCartItemDto request )
        {
            var command = new CreateShoppingItemCommand
            {
                BuyerId = buyerId,
                ProductId = request.ProductId,
                Cantity = request.Cantity,

            };

            var shoppingItem = await _mediator.Send(command);

            if (shoppingItem == -1)
            {
                return NotFound("Buyer or Product not found!");
            }
            else
            if (shoppingItem == -4)
            {
                return Ok("Item was deleted successesfully!");
            }
            else

            if (shoppingItem > 0)
            {
                return CreatedAtRoute(new { id = shoppingItem }, shoppingItem);
            }
            else 
                return BadRequest("Upps, Something wrong has happened!");    

        }

        ////GET: shoppingItems/{shoppingCartId}
        //[HttpGet("{shoppingCartId}")]
        //public async Task<ActionResult<List<ProductFromCartDto>>> GetAllProductsByCartId(int shoppingCartId)
        //{
        //    var query = new GetAllProductsByCartIdQuery
        //    {
        //        ShoppingCartId = shoppingCartId
        //    };
        //    var products = await _mediator.Send(query);
        //    if(products.Count > 0)
        //    {
        //        return Ok(products);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
            
        //}

        //GET: shoppingItems/get_items/{buyerId}
        [HttpGet("get_items/{buyerId}")]
        public async Task<ActionResult<List<ProductFromCartDto>>> GetAllProductsByBuyerId(int buyerId)
        {
            var query = new GetAllProductsByBuyerIdQuery
            {
                BuyerId = buyerId
            };
            var products = await _mediator.Send(query);
            if(products.Count > 0)
            {
                return Ok(products);
            }
            else
            {
                return NotFound("Your cart is empty!");
            }
            
        }

    }
}
