using MediatR;
using Application.DtoModels.OrderItem;

namespace Application.Features.OrderItems.Queries.GetAllItems
{
    public class GetAllItemsQuery : IRequest<List<OrderItemDto>>
    {
    }
}
