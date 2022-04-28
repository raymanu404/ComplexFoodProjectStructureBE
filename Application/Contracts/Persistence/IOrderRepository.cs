using Domain.Models.Ordering;

namespace Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        void DeleteAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        //Task<Order?> GetOrderByBuyerId(int buyerId);
        Task<List<Order>> GetAllAsync();
    }
}
