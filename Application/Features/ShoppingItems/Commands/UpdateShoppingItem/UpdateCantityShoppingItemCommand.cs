using MediatR;

namespace Application.Features.ShoppingItems.Commands
{
    public class UpdateCantityShoppingItemCommand : IRequest<string>
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Cantity { get; set; }
        public int BuyerId { get; set; }
    }
}
