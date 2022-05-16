using Application.DtoModels.Admin;
using Application.DtoModels.Product;
using Application.DtoModels.OrderItem;
using Application.DtoModels.Buyer;
using Application.DtoModels.Coupon;
using Application.DtoModels.Order;
using Application.DtoModels.Cart;
using Application.DtoModels.ShoppingCartItemDto;
using AutoMapper;
using Domain.Models.Roles;
using Domain.Models.Shopping;
using Domain.Models.Ordering;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        //DTOs
        CreateMap<Admin, AdminDto>().ReverseMap();
        CreateMap<Buyer, BuyerDto>().ReverseMap();
        CreateMap<Buyer, BuyerDtoLogin>().ReverseMap();
        CreateMap<Coupon, CouponDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
        
    }
}