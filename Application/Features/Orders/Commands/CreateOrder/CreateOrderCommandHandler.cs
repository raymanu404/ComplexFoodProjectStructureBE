using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}