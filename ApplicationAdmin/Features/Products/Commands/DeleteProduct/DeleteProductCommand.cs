using MediatR;

namespace ApplicationAdmin.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
    }
}
