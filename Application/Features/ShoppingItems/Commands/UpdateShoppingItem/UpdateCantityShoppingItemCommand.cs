using MediatR;

namespace Application.Features.ShoppingItems.Commands.UpdateShoppingItem
{
    public class UpdateCantityShoppingItemCommand : IRequest<int>
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Cantity { get; set; }
        public int BuyerId { get; set; }
    }
}
