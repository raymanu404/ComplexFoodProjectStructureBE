using ApplicationAdmin.DtoModels.OrderItem;
using MediatR;

namespace ApplicationAdmin.Features.OrderItems.Queries.GetAllItems
{
    public class GetAllItemsQuery : IRequest<List<OrderItemDto>>
    {
    }
}
