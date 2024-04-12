using ApplicationAdmin.DtoModels.Product;
using ApplicationAdmin.Profiles;
using HelperLibrary.Constants;
using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<StatusCodeEnum>
    {
        public int ProductId { get; set; }
        public ProductUpdateDto Product { get; set; }
    }
}
