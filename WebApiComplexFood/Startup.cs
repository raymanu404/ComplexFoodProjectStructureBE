#region MY IMPORTS
using Infrastructure;
using Application.Profiles;
#endregion

using MediatR;

using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using Application.Contracts.FileUtils;
using Infrastructure.FileUtils;
using Application.Models;
using Stripe;
using WebApiComplexFood.Extensions;
using Infrastructure.Repositories.Customer;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Contracts.Persistence;

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
            

            services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));
            services.Configure<StripeSettings>(Configuration.GetSection(nameof(StripeSettings)));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(defaultConnectionString, opt =>
                opt.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null))
            );
          
            // ----- repositories
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();
            services.AddSingleton<IFileReader, FileReader>();

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
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUiExtension();
                app.UseRedirectSwaggerPath();
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
            }).ApplyMigrations();
           
        }
    }
}


