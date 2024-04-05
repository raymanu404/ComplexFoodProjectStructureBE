using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
