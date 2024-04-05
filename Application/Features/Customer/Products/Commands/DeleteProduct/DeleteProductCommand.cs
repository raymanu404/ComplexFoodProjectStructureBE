using MediatR;
namespace Application.Features.Customer.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }
    }
}
