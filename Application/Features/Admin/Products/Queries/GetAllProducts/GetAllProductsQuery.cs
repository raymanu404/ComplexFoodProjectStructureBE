using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Admin.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
