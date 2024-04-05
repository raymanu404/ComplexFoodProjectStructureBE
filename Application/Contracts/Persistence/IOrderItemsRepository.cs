using Domain.Models.Ordering;

namespace Application.Contracts.Persistence
{
    public interface IOrderItemsRepository
    {
        Task AddAsync(OrderItem orderItem);
        Task<List<OrderItem>> GetAllItemsByOrderId(int orderId);
        Task<List<OrderItem>> GetAllItems();
    }
}