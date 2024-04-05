using MediatR;
using Application.DtoModels.OrderItem;

namespace Application.Features.OrderItems.Queries.GetALLItemsByOrderId
{
    public class GetAllItemsByOrderIdQuery : IRequest<List<OrderItemDto>>
    {
        public int OrderId { get; set; }
    }

}
