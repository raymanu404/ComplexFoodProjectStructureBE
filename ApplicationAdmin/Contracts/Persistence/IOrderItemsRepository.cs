using Domain.Models.Ordering;

namespace ApplicationAdmin.Contracts.Persistence
{
    public interface IOrderItemsRepository
    {
        Task<List<OrderItem>> GetAllItemsByOrderId(int orderId);
        Task<List<OrderItem>> GetAllItems();
    }
}