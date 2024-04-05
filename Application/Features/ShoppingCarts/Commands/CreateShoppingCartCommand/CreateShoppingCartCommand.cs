using Application.DtoModels.ShoppingCart;
using MediatR;
using Domain.Models.Shopping;

namespace Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCart>
    {
        public int BuyerId { get; set; }
        public ShoppingCartDto Cart { get; set; }

    }
}
