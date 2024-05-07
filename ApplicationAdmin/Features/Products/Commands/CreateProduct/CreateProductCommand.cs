using ApplicationAdmin.DtoModels.Product;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public ProductCreateDto Product { get; set; }
    }
}
