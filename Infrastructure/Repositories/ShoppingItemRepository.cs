using Application.Contracts.Persistence;
using Domain.Models.Shopping;

namespace Infrastructure.Repositories
{
    public class ShoppingItemRepository : IShoppingItemRepository
    {
        private readonly ApplicationContext _context;

        public ShoppingItemRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddAsync(ShoppingCartItem shoppingItem) => await _context.ShoppingItems.AddAsync(shoppingItem);
        public void Delete(ShoppingCartItem shoppingItem) => _context.ShoppingItems.Remove(shoppingItem);
    }
}
