using ApplicationAdmin.DtoModels.Product;
using MediatR;

namespace ApplicationAdmin.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
    }
}
