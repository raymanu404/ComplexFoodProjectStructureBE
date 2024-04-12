using ApplicationAdmin.Contracts.Persistence;
using Domain.Models.Ordering;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Admin
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void DeleteOrder(Order order) => _context.Orders.Remove(order);


        public  IQueryable<Order> GetOrderByIdQuery(int id) =>  _context.Orders.Where(x => x.Id == id);

        public IQueryable<Order> GetOrderByBuyerIdQuery(int buyerId) =>  _context.Orders.Where(x => x.BuyerId == buyerId);

        public IQueryable<Order> GetQueryable()
        {
            IQueryable<Order> ordersQuery = _context.Orders;
            return ordersQuery;
        }

    }
}
