#region MY IMPORTS
using Infrastructure;
using Application.Contracts.Persistence;
using Infrastructure.Repositories;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Profiles;
#endregion

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApiComplexFood
{
    public class Startup
    {
        const string CORS_POLICY = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public  void ConfigureServices(IServiceCollection services)
        {
            string defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(defaultConnectionString));
          
            // ----- repositories
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();

            // ---- unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ---- mapping
            services.AddAutoMapper(typeof(MappingProfile));

            // ---- mediatr
            services.AddMediatR(typeof(GetBuyersListQuery));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectStructure.Api", Version = "v1" });
            });

            services.AddCors(options => options.AddPolicy(CORS_POLICY, builder => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader()));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectStructure.Api v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(CORS_POLICY);
         
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}


