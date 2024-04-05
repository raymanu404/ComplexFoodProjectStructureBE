using MediatR;

namespace Application.Features.Customer.ShoppingCarts.Commands.DeleteShoppingCartCommand
{
    public class DeleteShoppingCartCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
    }
}
