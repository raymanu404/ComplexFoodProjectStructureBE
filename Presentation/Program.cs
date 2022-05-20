#region IMPORT REQUESTS
using Application.Features.Buyers.Commands.CreateBuyer;
using Application.Features.Buyers.Commands.UpdateBuyer;
using Application.Features.Buyers.Commands.DeleteBuyer;
using Application.Features.Buyers.Queries.GetBuyersList;
using Application.Features.Buyers.Queries.GetBuyerById;
using Application.Features.Coupons.Commands.CreateCoupon;
using Application.Features.Coupons.Queries.GetCouponsByBuyerId;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.OrderItems.Commands.DeleteItemCommand;
using Application.Features.OrderItems.Queries.GetItemById;
using Application.Features.Admins.Commands;
using Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand;
#endregion
#region DtoModels
using Application.DtoModels.Product;
using Application.DtoModels.Admin;
using Application.DtoModels.OrderItem;
using Application.DtoModels.Buyer;
using Application.DtoModels.Cart;
using Application.DtoModels.Coupon;
using Application.DtoModels.Order;
#endregion
using Application.Contracts.Persistence;
using Application.Profiles;
using Domain.ValueObjects;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models.Enums;

namespace Presentation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var diContainer = new ServiceCollection()
            .AddMediatR(typeof(CreateBuyerCommand)) 
            .AddScoped<IBuyerRepository, BuyerRepository>()
            .AddScoped<ICouponRepository, CouponRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IAdminRepository, AdminRepository>()
            .AddScoped<IOrderItemsRepository, OrderItemRepository>()
            .AddScoped<IShoppingCartRepository, ShoppingCartRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddAutoMapper(typeof(MappingProfile))
            .AddDbContext<ApplicationContext>(options => options.UseSqlServer("Server=DESKTOP-T4A7BVN;Database=ComplexFoodDatabase;Trusted_Connection=True;"))
            .BuildServiceProvider();

        var mediator = diContainer.GetRequiredService<IMediator>();


        //BUYER
        //var buyer = new BuyerDto
        //{

        //    Email = new Email("abc.abc4@email.com"),
        //    FirstName = new Name("Alexandru"),
        //    LastName = new Name("Avramescu"),
        //    Password = new Password("123Acasd1"),
        //    Gender = new Gender("M"),
        //    PhoneNumber = new PhoneNumber("+4072693923"),
        //};

        //var buyerId = await mediator.Send(new CreateBuyerCommand
        //{
        //    Buyer = buyer,
        //});

        //var buyers = await mediator.Send(new GetBuyersListQuery());

        //foreach(var buyer in buyers)
        //{
        //    Console.WriteLine(buyer.Email);
        //}

        //await mediator.Send(new UpdateBuyerCommand
        //{
        //    BuyerId = buyerId,
        //    Buyer  = new BuyerDtoUpdate
        //    { 
        //        Email = new Email("emilie.stefanescu@email.com"),
        //        FirstName=new Name("Emilia"),
        //        LastName=new Name("Stefanescu")
        //    }
        //});

        //var confirmedBuyer = await mediator.Send(new ConfirmBuyerCommand
        //{
        //    BuyerId = 2,
        //    Confirmed = true,
        //});

        //Console.WriteLine(confirmedBuyer);

        //var getBuyer1 = await mediator.Send(new GetBuyerByIdQuery
        //{
        //    BuyerId = 1
        //});

        //var deleteBuyer = await mediator.Send(new DeleteBuyerByIdCommand
        //{
        //    BuyerId = 1
        //});


        //var addBalanceBuyer1 = await mediator.Send(new DepositBalanceBuyerCommand
        //{
        //    BuyerId = 2,
        //    Balance = new Balance(300),
        //});

        //Console.WriteLine(addBalanceBuyer1);


        //COUPON    
        var newCoupon = new CouponDto
        {
          
            DateCreated = DateTime.Now,
            Type = TypeCoupons.ThirtyProcent,
            
        };

        var newCoupon2 = new CouponDto
        {

            DateCreated = DateTime.Now,
            Type = TypeCoupons.TwentyProcent,

        };

        //var coupon = await mediator.Send(new CreateCouponCommand
        //{
        //    BuyerId = 2,
        //    Coupon = newCoupon2,
        //    Amount = new Amount(5)
        //});

        //Console.WriteLine(coupon);

        //var getCouponsByBuyerId = await mediator.Send(new GetCouponsByBuyerIdQuery
        //{
        //    BuyerId = 4
        //});

        //foreach (var item in getCouponsByBuyerId)
        //{
        //    Console.WriteLine(item.Code);
        //}
        //Console.WriteLine(getCouponsByBuyerId.ElementAt(0).Buyer.LastName);

        //var useCoupon = await mediator.Send(new DeleteCouponCommand
        //{
        //    BuyerId = getCouponsByBuyerId.ElementAt(0).BuyerId,
        //    Code = getCouponsByBuyerId.ElementAt(0).Code,
        //});

        //Console.WriteLine(useCoupon);


        //PRODUCTS

        //var newProduct = new ProductDto
        //{
        //    Category = Categories.Meat,
        //    Title = "Snitel Paradisul Piratilor",
        //    Description = "Snitel Paradisul Piratilor 300 de grame",
        //    Price = new Price(3),
        //    DateCreated = DateTime.Now,
        //    DateUpdated = DateTime.Now,
        //    IsInStock = true,
        //};

        //var productId = await mediator.Send(new CreateProductCommand
        //{
        //    Product = newProduct
        //});

        //newProduct.Title = "Coca Cola la 2L";
        //await mediator.Send(new UpdateProductCommand
        //{
        //    ProductId = 6,
        //    Product = newProduct
        //});

        //await mediator.Send(new DeleteProductCommand
        //{
        //    ProductId = 5
        //});

        //var products = await mediator.Send(new GetAllProductsQuery());
        //foreach(var product in products)
        //{
        //    Console.WriteLine(product.Description);
        //}

        //var getproduct = await mediator.Send(new GetProductByIdQuery { ProductId = 3 });
        //Console.WriteLine(getproduct.Description);


        //ITEMS 
        //relatie one to one cu Products 

        //var newItem = new OrderItemDto
        //{
        //    Amount = new Amount(7),
        //    ProductId = 2,

        //};

        //var itemID = await mediator.Send(new CreateItemCommand
        //{
        //    OrderItem = newItem,
        //    BuyerId = 3,

        //});

        //Console.WriteLine($"Item ID : {itemID}");

        //await mediator.Send(new DeleteItemCommand { 
        //    ItemId = 9 
        //});

        //await mediator.Send(new UpdateCartIdForOrderItemCommand
        //{
        //    ItemId = 1,
        //    CartId = 1
        //});

        //await mediator.Send(new UpdateItemCommand
        //{
        //    ItemId = 1,
        //    Amount = new Amount(12),
        //    ProductId = 9
        //});


        //CART

        //var newCart = new CartDto
        //{
        //    Discount = new Discount(10),
        //};

        //var cart = await mediator.Send(new CreateCartCommand
        //{
        //    BuyerId = 1,
        //    Cart = newCart
        //});

        //Console.WriteLine(cart);

        //ORDERS



        //ADMINS

        //var newAdmin = new AdminDto
        //{
        //    Email = new Email("admin1@email.com"),
        //    FirstName = new Name("Admin"),
        //    LastName = new Name("Admin"),
        //    Password = new Password("parolamea123W")
        //};

        //await mediator.Send(new CreateAdminCommand { 
        //    Admin = newAdmin
        //});


    }

}