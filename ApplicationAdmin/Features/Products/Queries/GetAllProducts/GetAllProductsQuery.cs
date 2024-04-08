
using ApplicationAdmin.Contracts.Abstractions;
using ApplicationAdmin.DtoModels.Product;
using HelperLibrary.Classes;
using MediatR;

namespace ApplicationAdmin.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ResponseData<ProductDto>>
    {
        public SearchParams SearchParams { get; set; }

    }

}
