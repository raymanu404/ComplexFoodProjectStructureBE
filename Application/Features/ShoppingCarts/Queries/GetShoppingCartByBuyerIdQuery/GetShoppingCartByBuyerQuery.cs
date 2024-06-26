﻿using Application.DtoModels.ShoppingCart;
using MediatR;

namespace Application.Features.ShoppingCarts.Queries.GetShoppingCartByBuyerIdQuery
{
    public class GetShoppingCartByBuyerQuery : IRequest<ShoppingCartDto>
    {
        public int BuyerId { get; set; }
    }
}
