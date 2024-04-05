using MediatR;
using Application.DtoModels.Order;

namespace Application.Features.Customer.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommand : IRequest<int>
{
    public OrderDto Order { get; set; }
}