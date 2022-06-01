using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<ShoppingCartItem>> GetAllShoppingItemsByShoppingCartId(int shoppingCartId) => await _context.ShoppingItems.Where(x => x.ShoppingCartId == shoppingCartId).ToListAsync();

        public async Task<ShoppingCartItem?> GetShoppingItemByIds(int shoppingCartId, int productId) 
        {

            var shoppingCartItem = await _context.ShoppingItems.Where(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId).FirstOrDefaultAsync();
            return shoppingCartItem;
        }
    }
}
