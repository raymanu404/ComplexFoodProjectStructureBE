using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public ProductDto Product { get; set; }
    }
}
