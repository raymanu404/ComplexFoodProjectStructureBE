using System.Reflection;
using Application.Contracts.Persistence.Admin;
using Application.Features.Admin.Products.Queries.GetAllProducts;
using Infrastructure;
using Infrastructure.Repositories.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IBuyerRepository = Application.Contracts.Persistence.Admin.IBuyerRepository;
using IProductRepository = Application.Contracts.Persistence.Admin.IProductRepository;

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
                .AddMediatR(typeof(Program))
                .AddScoped<IBuyerRepository, BuyerRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IUnitOfWorkAdmin, UnitOfWorkAdmin>();


        return localServices;
    }
}