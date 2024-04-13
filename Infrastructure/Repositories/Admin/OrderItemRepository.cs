using ApplicationAdmin.Contracts.Persistence;
using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Admin
{
    public class OrderItemRepository : IOrderItemsRepository
    {
        private readonly ApplicationContext _context;

        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        //TODO: for now we don't consume this method, if yes, add pagination, or get compute ingredients from this
        public async Task<List<OrderItem>> GetAllItems() => await _context.OrderItems.ToListAsync();

        public async Task<List<OrderItem>> GetAllItemsByOrderId(int orderId) => await _context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
    }
}
