using Domain.Models.Shopping;

namespace Application.Contracts.Persistence
{
    public interface IShoppingItemRepository
    {
        Task AddAsync(ShoppingCartItem shoppingItem);
        void Delete(ShoppingCartItem shoppingItem);
    }
}
