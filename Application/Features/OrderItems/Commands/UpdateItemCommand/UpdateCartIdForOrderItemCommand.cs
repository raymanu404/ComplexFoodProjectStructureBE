using MediatR;
using Application.DtoModels.OrderItem;

namespace Application.Features.OrderItems.Commands.UpdateItemCommand
{
    public class UpdateCartIdForOrderItemCommand : IRequest
    {
        public int ItemId { get; set; }
        public int CartId { get; set; }
    }
}
