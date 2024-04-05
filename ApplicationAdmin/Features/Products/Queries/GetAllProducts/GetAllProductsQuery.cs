using ApplicationAdmin.DtoModels.Product;
using MediatR;

namespace ApplicationAdmin.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
