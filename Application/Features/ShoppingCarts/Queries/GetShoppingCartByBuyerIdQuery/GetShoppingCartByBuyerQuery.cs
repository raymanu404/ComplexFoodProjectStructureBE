using MediatR;
using Application.DtoModels.Cart;

namespace Application.Features.ShoppingCarts.Queries.GetShoppingCartByBuyerIdQuery
{
    public class GetShoppingCartByBuyerQuery : IRequest<ShoppingCartDto>
    {
        public int BuyerId { get;set; }
    }
}
