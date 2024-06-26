﻿using ApplicationAdmin.DtoModels.Order;
using ApplicationAdmin.DtoModels.OrderItem;
using ApplicationAdmin.DtoModels.Product;
using ApplicationAdmin.Features.Products.Queries.GetMostOrderedProducts;
using AutoMapper;
using Domain.Models.Ordering;
using Domain.Models.Shopping;

namespace ApplicationAdmin.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Product, ProductDto>()
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))
            .ForMember(x => x.MerchantPrice, dest => dest.MapFrom(x => x.MerchantPrice.Value))
            .ReverseMap();

        CreateMap<Product, Response.ProductDto>()
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))
            .ForMember(x => x.MerchantPrice, dest => dest.MapFrom(x => x.MerchantPrice.Value))
            .ReverseMap();

        CreateMap<ProductUpdateDto, ProductDto>()
            .ReverseMap();

        CreateMap<Product, ProductCreateDto>()
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))
            .ForMember(x => x.MerchantPrice, dest => dest.MapFrom(x => x.MerchantPrice.Value))
            .ReverseMap();

        CreateMap<Order, OrderDto>()
            .ForMember(x => x.Code, dest => dest.MapFrom(x => x.Code.Value))
            .ForMember(x => x.Discount, dest => dest.MapFrom(x => x.Discount.Value))
            .ForMember(x => x.TotalPrice, dest => dest.MapFrom(x => x.TotalPrice.Value))
            .ForMember(x => x.BuyerFullName, dest => dest.MapFrom(x =>$"{x.Buyer.FirstName.Value} {x.Buyer.LastName.Value}"))
            .ReverseMap();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(x => x.Cantity, dest => dest.MapFrom(x => x.Cantity.Value))
            .ForMember(x => x.Price, dest => dest.MapFrom(x => x.Price.Value))
            .ReverseMap();
    }
}