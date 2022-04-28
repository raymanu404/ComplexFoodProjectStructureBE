using Domain.Models.Shopping;

namespace Application.Contracts.Persistence
{
    public interface IShoppingCartRepository
    {
        Task AddAsync(ShoppingCart cart);
        void Delete(ShoppingCart cart);
        Task<ShoppingCart?> GetCartByBuyerIdAsync(int buyerId);
    }
}
