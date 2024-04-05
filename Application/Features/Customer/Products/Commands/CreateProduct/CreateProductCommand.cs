using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Customer.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public ProductDto Product { get; set; }
    }
}
