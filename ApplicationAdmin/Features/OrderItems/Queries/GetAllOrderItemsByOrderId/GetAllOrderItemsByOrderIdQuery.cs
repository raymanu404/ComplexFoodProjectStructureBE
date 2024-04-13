using ApplicationAdmin.DtoModels.OrderItem;
using MediatR;

namespace ApplicationAdmin.Features.OrderItems.Queries.GetAllOrderItemsByOrderId
{
    public class GetAllOrderItemsByOrderIdQuery : IRequest<IList<OrderItemDto>>
    {
        public int OrderId { get; set; }
    }

}
