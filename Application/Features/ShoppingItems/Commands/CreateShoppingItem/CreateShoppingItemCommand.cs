using MediatR;

namespace Application.Features.ShoppingItems.Commands
{
    public class CreateShoppingItemCommand : IRequest<int>
    {
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Cantity { get; set; }
    }
}
