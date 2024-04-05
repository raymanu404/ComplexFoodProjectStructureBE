using ApplicationAdmin.DtoModels.Product;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public ProductDto Product { get; set; }
    }
}
