namespace ApplicationAdmin.Contracts.Persistence;
public interface IUnitOfWorkAdmin : IAsyncDisposable
{
    IBuyerRepository Buyers { get; }
    IProductRepository Products { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);

}
