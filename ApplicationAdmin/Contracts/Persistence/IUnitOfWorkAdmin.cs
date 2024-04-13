namespace ApplicationAdmin.Contracts.Persistence;
public interface IUnitOfWorkAdmin : IAsyncDisposable
{
    IBuyerRepository Buyers { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IOrderItemsRepository OrderItems { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);

}
