using Application.Contracts.Persistence;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(
            ApplicationContext context,
            IBuyerRepository buyers,
            ICouponRepository coupons,
            IProductRepository products,
            IAdminRepository admins,
            IShoppingCartRepository carts,
            IShoppingItemRepository shoppingItems,
            IOrderRepository orders,
            IOrderItemsRepository items)
    {
        _context = context;
        Buyers = buyers;
        Coupons = coupons;
        Products = products;
        Admins = admins;
        OrderItems = items;
        ShoppingCarts = carts;
        Orders = orders;
        ShoppingItems = shoppingItems;
    }

    public IBuyerRepository Buyers { get; }
    public ICouponRepository Coupons { get; }
    public IProductRepository Products { get; }

    public IAdminRepository Admins { get; }

    public IOrderItemsRepository OrderItems { get; }

    public IShoppingCartRepository ShoppingCarts { get; }
    public IShoppingItemRepository ShoppingItems { get; }

    public IOrderRepository Orders { get; }

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}