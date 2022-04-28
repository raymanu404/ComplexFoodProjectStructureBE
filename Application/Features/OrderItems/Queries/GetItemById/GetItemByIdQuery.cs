using MediatR;
using Application.DtoModels.OrderItem;

namespace Application.Features.OrderItems.Queries.GetItemById
{
    public class GetItemByIdQuery : IRequest<OrderItemDto>
    {
        public int ItemId { get; set; }
    }
}
