using System.Reflection;
using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.Features.Products.Queries.GetAllProducts;
using ApplicationAdmin.Profiles;
using Infrastructure;
using Infrastructure.Repositories.Admin;
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
                .AddAutoMapper(typeof(MappingProfile))
                .AddMediatR(typeof(GetAllProductsQuery))
                .AddScoped<IBuyerRepository, BuyerRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IUnitOfWorkAdmin, UnitOfWorkAdmin>();


        return localServices;
    }
}