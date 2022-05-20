using MediatR;

namespace Application.Features.ShoppingCarts.Commands
{
    public class DeleteShoppingCartCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
    }
}
