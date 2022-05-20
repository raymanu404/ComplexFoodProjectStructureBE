using MediatR;
using Application.DtoModels.Order;


namespace Application.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }
}
