using Domain.Models.Ordering;

namespace ApplicationAdmin.Contracts.Persistence
{
    public interface IOrderRepository
    {
        void DeleteOrder(Order order);
        IQueryable<Order> GetOrderByIdQuery(int id);
        IQueryable<Order> GetOrderByBuyerIdQuery(int buyerId);
        IQueryable<Order> GetQueryable();
    }
}
