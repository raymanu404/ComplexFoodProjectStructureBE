using MediatR;

namespace Application.Features.ShoppingCarts.Commands.DeleteShoppingCartCommand
{
    public class DeleteShoppingCartCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
    }
}
