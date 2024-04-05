using Application.DtoModels.Order;
using MediatR;

namespace Application.Features.Customer.Orders.Commands.UpdateOrderCommand
{
    public class UpdateStatusOrderCommand : IRequest<int>
    {
        public int BuyerId { get; set; }
        public OrderUpdateStatusDto updateStatus { get; set; }
    }
}
