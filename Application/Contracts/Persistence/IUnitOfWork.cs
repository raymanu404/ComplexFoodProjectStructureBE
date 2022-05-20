namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IAsyncDisposable
{
    IAdminRepository Admins { get; }
    IBuyerRepository Buyers { get; }
    ICouponRepository Coupons { get; }
    IProductRepository Products { get; }
    IOrderItemsRepository OrderItems { get; }
    IShoppingCartRepository ShoppingCarts { get; }
    IShoppingItemRepository ShoppingItems { get; }
    IOrderRepository Orders { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}