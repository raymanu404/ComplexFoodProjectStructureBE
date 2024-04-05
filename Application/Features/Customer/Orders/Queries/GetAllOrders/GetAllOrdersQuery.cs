using MediatR;
using Application.DtoModels.Order;

namespace Application.Features.Customer.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
    }
}
