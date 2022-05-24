using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ShoppingItems.Commands;
using Domain.Models.Shopping;
using Application.DtoModels.ShoppingCartItemDto;
using Application.DtoModels.Product;
using Application.Features.ShoppingItems.Queries.GetAllProductsByCartId;

namespace WebApiComplexFood.Controllers
{

    [ApiController]
    [Route("shoppingItems")]
    public class ShoppingItemController : Controller
    {
        private readonly ILogger<ShoppingItemController> _logger;
        private readonly IMediator _mediator;

        public ShoppingItemController(IMediator mediator, ILogger<ShoppingItemController> logger)
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
            if(shoppingItem == -2)
            {
                return BadRequest("Insufficient funds!");
            }  
            if(shoppingItem == -1)
            {
                return NotFound("Buyer or Product not found!");
            }
            if (shoppingItem == -4)
            {
                return Ok("Item was deleted successesfully!");
            }

            if (shoppingItem > 0)
            {
                return CreatedAtRoute(new { id = shoppingItem }, shoppingItem);
            }

            return BadRequest("Upps, something went wrong...");


        }

        //GET: shoppingItems/{shoppingCartId}
        [HttpGet("{shoppingCartId}")]
        public async Task<ActionResult<List<ProductFromCartDto>>> GetAllProductsByCartId(int shoppingCartId)
        {
            var query = new GetAllProductsByCartIdQuery
            {
                ShoppingCartId = shoppingCartId
            };
            var products = await _mediator.Send(query);
            if(products.Count > 0)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
            
        }

    }
}
