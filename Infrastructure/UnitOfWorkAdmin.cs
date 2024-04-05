using Application.Contracts.Persistence.Admin;
using IBuyerRepository = Application.Contracts.Persistence.Admin.IBuyerRepository;
using IProductRepository = Application.Contracts.Persistence.Admin.IProductRepository;

namespace Infrastructure;

public class UnitOfWorkAdmin : IUnitOfWorkAdmin
{
    private readonly ApplicationContext _context;

    public UnitOfWorkAdmin(
        ApplicationContext context,
        IBuyerRepository buyers,
        IProductRepository products
        )
    {
        _context = context;
        Buyers = buyers;
        Products = products;

    }

    public IBuyerRepository Buyers { get; }
    public IProductRepository Products { get; }

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}