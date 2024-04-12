using ApplicationAdmin.DtoModels.Order;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrderByBuyerId
{
    public class GetOrderByBuyerIdQuery : IRequest<OrderDto>
    {
        public int BuyerId { get; set; }
    }
}
