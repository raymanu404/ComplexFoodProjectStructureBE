using Domain.Models.Shopping;

namespace Application.Contracts.Persistence.Customer
{
    public interface IShoppingItemRepository
    {
        Task AddAsync(ShoppingCartItem shoppingItem);
        void Delete(ShoppingCartItem shoppingItem);
        Task<ShoppingCartItem?> GetShoppingItemByIds(int shoppingCartId, int productId);
        Task<List<ShoppingCartItem>> GetAllShoppingItemsByShoppingCartId(int shoppingCartId);

    }
}
