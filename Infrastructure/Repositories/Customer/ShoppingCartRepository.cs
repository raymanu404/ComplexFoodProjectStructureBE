using Application.Contracts.Persistence;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Customer
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationContext _context;
        public ShoppingCartRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ShoppingCart cart) => await _context.ShoppingCarts.AddAsync(cart);

        public void Delete(ShoppingCart cart) => _context.ShoppingCarts.Remove(cart);

        public async Task<ShoppingCart?> GetCartByBuyerIdAsync(int buyerId) => await _context.ShoppingCarts.Where(x => x.BuyerId == buyerId).FirstOrDefaultAsync();

        public async Task<ShoppingCart?> GetCartByIdAsync(int id) => await _context.ShoppingCarts.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
