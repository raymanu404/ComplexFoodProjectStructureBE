using MediatR;

namespace Application.Features.OrderItems.Commands.DeleteItemCommand
{
    public class DeleteItemCommand : IRequest
    {
        public int ItemId { get; set; }
    }
}
