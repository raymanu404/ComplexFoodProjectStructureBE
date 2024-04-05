using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Customer.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
