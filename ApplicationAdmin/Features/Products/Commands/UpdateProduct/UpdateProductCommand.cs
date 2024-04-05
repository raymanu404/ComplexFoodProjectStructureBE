using ApplicationAdmin.DtoModels.Product;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
