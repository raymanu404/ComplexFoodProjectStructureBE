using Domain.Models.Roles;
using Domain.Models.Ordering;
using Domain.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityConfigurations.Roles;
using Infrastructure.EntityConfigurations.Orders;
using Infrastructure.EntityConfigurations.Products;

namespace Infrastructure;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    public DbSet<Buyer> Buyers => Set<Buyer>(); 
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ShoppingCartItem> ShoppingItems => Set<ShoppingCartItem>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Admin> Admins => Set<Admin>();

    //public DbSet<Announcement> Announcements = Set<Announcement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //daca nu specificam nimic aici, inseamna ca se creeaza ori by convention ori pe tipul de adnotari

        modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CartEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CouponEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdminEntityTypeConfiguration());

        //modelBuilder.ApplyConfiguration(new AnnouncementEntityTypeConfiguration());

        //sau varianta a doua, aplica toate configurariile
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}