using MediatR;
using Application.DtoModels.Order;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int UserId { get; set; }
    public OrderDto Order { get; set; }
}