namespace Application.Contracts.Persistence.Customer;

public interface IUnitOfWork : IAsyncDisposable
{
    IBuyerRepository Buyers { get; }
    ICouponRepository Coupons { get; }
    IProductRepository Products { get; }
    IOrderItemsRepository OrderItems { get; }
    IShoppingCartRepository ShoppingCarts { get; }
    IShoppingItemRepository ShoppingItems { get; }
    IOrderRepository Orders { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}