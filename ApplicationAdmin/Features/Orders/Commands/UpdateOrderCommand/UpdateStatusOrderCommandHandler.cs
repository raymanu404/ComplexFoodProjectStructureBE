using ApplicationAdmin.Contracts.Persistence;
using ApplicationAdmin.Profiles;
using Domain.Models.Enums;
using HelperLibrary.Constants;
using HelperLibrary.Methods;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAdmin.Features.Orders.Commands.UpdateOrderCommand
{
    public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand, StatusCodeEnum>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;
        public UpdateStatusOrderCommandHandler(IUnitOfWorkAdmin unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StatusCodeEnum> Handle(UpdateStatusOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getOrderByBuyerIdQuery =  _unitOfWork.Orders.GetOrderByBuyerIdQuery(command.BuyerId);
                var getOrderByBuyerId = await getOrderByBuyerIdQuery.FirstOrDefaultAsync(cancellationToken);

                if (getOrderByBuyerId != null)
                {
                    //TODO: check what status was before and try to update depends on this status Placed -> Progress -> Done
                    var status = command.Status.ConvertToEnum(OrderStatus.InProgress);

                    if (getOrderByBuyerId.Status != status)
                    {
                        getOrderByBuyerId.Status = status;
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                    {
                        return StatusCodeEnum.NoOperation;
                    }
            
                }
                else
                {
                    return StatusCodeEnum.NotFound;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCodeEnum.Error;
            }

            return StatusCodeEnum.Success;
        }
    }
}
