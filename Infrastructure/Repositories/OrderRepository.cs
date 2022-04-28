using Application.Contracts.Persistence;
using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);

        public void DeleteAsync(Order order) => _context.Orders.Remove(order);

        public async Task<List<Order>> GetAllAsync() => await _context.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) => await _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();

        //public async Task<Order?> GetOrderByBuyerId(int buyerId) => await _context.Orders.Where(x => x.Cart.BuyerId == buyerId).FirstOrDefaultAsync();  

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
