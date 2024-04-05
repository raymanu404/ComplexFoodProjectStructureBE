using Domain.Models.Ordering;

namespace Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        void DeleteOrder(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetOrderByBuyerId(int buyerId);
        Task<List<Order>> GetAllOrdersByBuyerId(int buyerId);
        Task<List<Order>> GetAllAsync();
    }
}
