using ApplicationAdmin.DtoModels.Product;
using AutoMapper;
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

    }
}