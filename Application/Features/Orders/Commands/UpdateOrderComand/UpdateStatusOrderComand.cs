using MediatR;
using Application.DtoModels.Order;

namespace Application.Features.Orders.Commands.UpdateOrderComand
{
    public class UpdateStatusOrderComand : IRequest<int>
    {
        public  int BuyerId { get; set; }
        public OrderUpdateStatusDto updateStatus { get; set; }
    }
}
