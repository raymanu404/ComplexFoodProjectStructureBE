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
        CreateMap<Admin, AdminDto>()
            .ForMember(x => x.FirstName, dest => dest.MapFrom(x => x.FirstName.Value))
            .ForMember(x => x.LastName, dest => dest.MapFrom(x => x.LastName.Value))
            .ForMember(x => x.Email, dest => dest.MapFrom(x => x.Email.Value))
            .ForMember(x => x.Password, dest => dest.MapFrom(x => x.Password.Value))
            .ReverseMap();

        CreateMap<Buyer, BuyerDto>()
            .ForMember(x => x.Balance, dest => dest.MapFrom(x => x.Balance.Value))
            .ForMember(x => x.Email, dest => dest.MapFrom(x => x.Email.Value))
            .ForMember(x => x.PhoneNumber, dest => dest.MapFrom(x => x.PhoneNumber.Value))
            .ForMember(x => x.FirstName, dest => dest.MapFrom(x => x.FirstName.Value))
            .ForMember(x => x.LastName, dest => dest.MapFrom(x => x.LastName.Value))
            .ForMember(x => x.Password, dest => dest.MapFrom(x => x.Password.Value))
            .ForMember(x => x.Gender, dest => dest.MapFrom(x => x.Gender.Value))
            .ForMember(x => x.ConfirmationCode, dest => dest.MapFrom(x => x.ConfirmationCode.Value))
            .ReverseMap(); 

        CreateMap<Buyer, BuyerRegisterDto>()          
            .ForMember(x => x.Email, dest => dest.MapFrom(x => x.Email.Value))
            .ForMember(x => x.PhoneNumber, dest => dest.MapFrom(x => x.PhoneNumber.Value))
            .ForMember(x => x.FirstName, dest => dest.MapFrom(x => x.FirstName.Value))
            .ForMember(x => x.LastName, dest => dest.MapFrom(x => x.LastName.Value))
            .ForMember(x => x.Password, dest => dest.MapFrom(x => x.Password.Value))
            .ForMember(x => x.Gender, dest => dest.MapFrom(x => x.Gender.Value))
            .ReverseMap();

        CreateMap<Buyer, BuyerLoginDto>().ReverseMap();

        CreateMap<Coupon, CouponDto>()
            .ForMember(x => x.Code, dest => dest.MapFrom(x => x.Code.Value))
            .ReverseMap();

        CreateMap<Product, ProductDto>()
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))
            .ReverseMap();

        CreateMap<Order, OrderDto>()
           .ForMember(x => x.Code, dest => dest.MapFrom(x => x.Code.Value))
           .ForMember(x => x.Discount, dest => dest.MapFrom(x => x.Discount.Value))
           .ForMember(x => x.TotalPrice, dest => dest.MapFrom(x => x.TotalPrice.Value))
           .ReverseMap();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(x => x.Cantity, dest => dest.MapFrom(x => x.Cantity.Value))
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))             
            .ReverseMap();

        CreateMap<ShoppingCart, ShoppingCartDto>()
            .ForMember(x => x.Code, dest => dest.MapFrom(x => x.Code.Value))
            .ForMember(x => x.Discount, dest => dest.MapFrom(x => x.Discount.Value))
            .ForMember(x => x.TotalPrice, dest => dest.MapFrom(x => x.TotalPrice.Value))
            .ReverseMap();       

        CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
            .ForMember(x => x.Cantity, dest => dest.MapFrom(x => x.Cantity.Value))
            .ReverseMap();
        
    }
}