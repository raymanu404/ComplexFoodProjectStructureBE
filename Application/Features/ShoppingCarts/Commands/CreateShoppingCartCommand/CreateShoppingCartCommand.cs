using Application.DtoModels.Cart;
using MediatR;

namespace Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartDto>
    {
        public int BuyerId { get; set; }
        public ShoppingCartDto Cart { get; set; }

    }
}
