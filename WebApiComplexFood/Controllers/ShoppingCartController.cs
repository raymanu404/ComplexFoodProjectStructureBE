﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DtoModels.ShoppingCart;

using Application.Features.ShoppingCarts.Commands.DeleteShoppingCartCommand;
using Application.Features.ShoppingCarts.Commands.UpdateShoppingCartCommand;


namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("carts")]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IMediator _mediator;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //PATCH : /carts/confirm/{buyerId}
        [HttpPatch("confirm/{buyerId}")]
        public async Task<ActionResult<ShoppingCartConfirmDto>> ConfirmShoppingCart(int buyerId,[FromBody] ShoppingCartConfirmDto confirmCart)
        {
            var command = new ConfirmShoppingCartCommand
            {
                BuyerId = buyerId,
                CouponCode = confirmCart.CouponCode
            };
            var response = await _mediator.Send(command);
            if (response.StartsWith("OrderCode"))
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        //DELETE : carts/delete/{buyerId}
        [HttpDelete("delete/{buyerId}")]
        public async Task<ActionResult<string>> DeleteCartByBuyerId(int buyerId)
        {
            var command = new DeleteShoppingCartCommand
            {
                BuyerId = buyerId
            };
            var returnMessage = await _mediator.Send(command);
            if (returnMessage.StartsWith("Success!"))
            {
                return Ok(returnMessage);
            }
            else
            {
                return BadRequest(returnMessage);
            }
            
        }

    }
}
