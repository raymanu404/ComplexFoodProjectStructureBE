using MediatR;
using Application.DtoModels.Order;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public OrderDto Order { get; set; }
}