using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Admin.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
    }
}
