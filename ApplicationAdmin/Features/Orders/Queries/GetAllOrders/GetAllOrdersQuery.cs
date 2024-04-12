using ApplicationAdmin.Contracts.Abstractions;
using ApplicationAdmin.DtoModels.Order;
using HelperLibrary.Classes;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<ResponseData<OrderDto>>
    {
        public SearchParams SearchParams { get; set; }
    }
}
