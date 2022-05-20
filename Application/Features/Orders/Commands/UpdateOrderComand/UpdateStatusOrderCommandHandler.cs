using MediatR;
using Application.Contracts.Persistence;

namespace Application.Features.Orders.Commands.UpdateOrderComand
{
    public class UpdateStatusOrderCommandHandler :IRequestHandler<UpdateStatusOrderComand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusOrderCommandHandler(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork; 
        }

        public async Task<int> Handle(UpdateStatusOrderComand command, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderByBuyerId = await _unitOfWork.Orders.GetOrderByBuyerId(command.BuyerId);
                if (getOrderByBuyerId != null)
                {
                    getOrderByBuyerId.Status = command.updateStatus.Status;
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            
            return 1;
        }
    }
}
