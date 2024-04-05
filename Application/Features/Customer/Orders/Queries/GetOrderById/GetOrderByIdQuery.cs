using MediatR;
using Application.DtoModels.Order;


namespace Application.Features.Customer.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }
}
