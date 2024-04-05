using MediatR;
using Application.DtoModels.Order;


namespace Application.Features.Customer.Orders.Queries.GetOrdersByBuyer
{
    public class GetOrdersByBuyerIdQuery : IRequest<List<OrderDto>>
    {
        public int BuyerId { get; set; }
    }
}
