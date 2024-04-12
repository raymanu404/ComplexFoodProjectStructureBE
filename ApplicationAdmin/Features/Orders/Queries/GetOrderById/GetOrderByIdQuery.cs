using ApplicationAdmin.DtoModels.Order;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }
}
