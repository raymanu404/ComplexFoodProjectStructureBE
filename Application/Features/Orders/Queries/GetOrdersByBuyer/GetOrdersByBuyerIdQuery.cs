using MediatR;
using Application.DtoModels.Order;


namespace Application.Features.Orders.Queries.GetOrdersByBuyer
{
    public class GetOrdersByBuyerIdQuery : IRequest<List<OrderDto>>
    {
        public int BuyerId { get; set; }
    }
}
