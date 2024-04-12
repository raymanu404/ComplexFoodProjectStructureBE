using System.Reflection;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.Profiles;
using Infrastructure;
using Infrastructure.Repositories.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GetAllProductsQuery = ApplicationAdmin.Features.Products.Queries.GetAllProducts.GetAllProductsQuery;
using GetAllOrdersQuery = ApplicationAdmin.Features.Orders.Queries.GetAllOrders.GetAllOrdersQuery;

namespace WebApiComplexFoodAdmin.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration Configuration)
    {
        var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

        var localServices =
            services
                .AddDbContext<ApplicationContext>(options => options.UseSqlServer(defaultConnectionString, opt =>
                    opt.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null))
                )
                .AddAutoMapper(typeof(MappingProfile))
               
                .AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly);
                    config.RegisterServicesFromAssembly(typeof(GetAllOrdersQuery).Assembly);
                })
                .AddScoped<IBuyerRepository, BuyerRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IUnitOfWorkAdmin, UnitOfWorkAdmin>();


        return localServices;
    }
}