using Application.Contracts.Persistence;
using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemsRepository
    {
        private readonly ApplicationContext _context;

        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateItemAsync(OrderItem item) => await _context.OrderItems.AddAsync(item);

        public void DeleteItem(OrderItem item) => _context.OrderItems.Remove(item);

        public async Task<List<OrderItem?>> GetAllItemsAsync() => await _context.OrderItems.ToListAsync();

        //public async Task<List<OrderItem?>> GetALLItemsByBuyerId(int buyerId) => await _context.OrderItems.Where(x => x.BuyerId == buyerId).ToListAsync();

        public async Task<OrderItem?> GetItemByIdAsync(int id) => await _context.OrderItems.FindAsync(id);

        public Task UpdateItemAsync(OrderItem item)
        {
            throw new NotImplementedException();
        }
    }
}
