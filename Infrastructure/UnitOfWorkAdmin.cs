

using ApplicationAdmin.Contracts.Persistence;

namespace Infrastructure;

public class UnitOfWorkAdmin : IUnitOfWorkAdmin
{
    private readonly ApplicationContext _context;

    public UnitOfWorkAdmin(
        ApplicationContext context,
        IBuyerRepository buyers,
        IProductRepository products,
        IOrderRepository orders,
        IOrderItemsRepository orderItems
        )
    {
        _context = context;
        Buyers = buyers;
        Products = products;
        Orders = orders;
        OrderItems = orderItems;

    }

    public IBuyerRepository Buyers { get; }
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemsRepository OrderItems { get; }

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}