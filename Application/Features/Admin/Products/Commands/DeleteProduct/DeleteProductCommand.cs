using MediatR;

namespace Application.Features.Admin.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }
    }
}
