using ApplicationAdmin.Contracts.Abstractions;
using ApplicationAdmin.DtoModels.Order;
using HelperLibrary.Classes;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersByBuyer
{
    public class GetOrdersByBuyerIdQuery : IRequest<ResponseData<OrderDto>>
    {
        public int BuyerId { get; set; }
        public SearchParams SearchParams { get; set; }
    }
}
