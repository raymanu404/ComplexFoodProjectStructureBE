using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public ProductDto Product { get; set; }
    }
}
