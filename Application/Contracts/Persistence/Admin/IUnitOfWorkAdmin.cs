namespace Application.Contracts.Persistence.Admin;
public interface IUnitOfWorkAdmin : IAsyncDisposable
{
    IBuyerRepository Buyers { get; }
    IProductRepository Products { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);

}
