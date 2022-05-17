using MediatR;
using Application.DtoModels.Product;
using Domain.Models.Shopping;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
