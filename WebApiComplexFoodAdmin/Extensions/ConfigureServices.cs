using Application.Contracts.Persistence;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Features.Products.Queries.GetAllProducts;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                    .AddScoped<IBuyerRepository, BuyerRepository>()
                    //.AddScoped<ICouponRepository, CouponRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
            //.AddScoped<IOrderRepository, OrderRepository>()
            //.AddScoped<IOrderItemsRepository, OrderItemRepository>()
            //.AddScoped<IUnitOfWork, UnitOfWork>()
            //adding mappers
            .AddMediatR(typeof(GetAllProductsQuery))
            ;


        return localServices;
    }
}