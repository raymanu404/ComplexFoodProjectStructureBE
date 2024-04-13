using ApplicationAdmin.DtoModels.Order;
using ApplicationAdmin.DtoModels.OrderItem;
using ApplicationAdmin.DtoModels.Product;
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
            .ReverseMap();

        CreateMap<ProductUpdateDto, ProductDto>()
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
    }
}