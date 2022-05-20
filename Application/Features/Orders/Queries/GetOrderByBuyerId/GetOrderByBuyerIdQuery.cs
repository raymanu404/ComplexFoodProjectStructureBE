using MediatR;
using Application.DtoModels.Order;


namespace Application.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByBuyerIdQuery : IRequest<OrderDto>
    {
        public int BuyerId { get; set; }
    }
}
