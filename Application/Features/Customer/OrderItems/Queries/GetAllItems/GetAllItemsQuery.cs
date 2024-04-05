using MediatR;
using Application.DtoModels.OrderItem;

namespace Application.Features.Customer.OrderItems.Queries.GetAllItems
{
    public class GetAllItemsQuery : IRequest<List<OrderItemDto>>
    {
    }
}
