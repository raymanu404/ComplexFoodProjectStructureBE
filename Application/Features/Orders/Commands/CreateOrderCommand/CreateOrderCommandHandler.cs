using MediatR;
using AutoMapper;
using Domain.Models.Ordering;
using Application.Contracts.Persistence;

namespace Application.Features.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        int orderId = 0;

        try
        {
            //facem order-ul pentru acel buyer. dupa aceea se sterge si cartul, e totul verificat de dinainte      
            var newOrder = _mapper.Map<Order>(command.Order);
            await _unitOfWork.Orders.AddAsync(newOrder);

            await _unitOfWork.CommitAsync(cancellationToken);
            var order = await _unitOfWork.Orders.GetOrderByBuyerId(newOrder.BuyerId);
            if (order != null)
            {
                orderId = order.Id;
            }
            else
            {
                return -1;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            orderId = -1;
        }

        return orderId;
    }
}