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
                var getOrderByBuyerIdQuery =  _unitOfWork.Orders.GetOrderByIdQuery(command.OrderId);
                var getOrderByBuyerId = await getOrderByBuyerIdQuery.FirstOrDefaultAsync(cancellationToken);

                if (getOrderByBuyerId != null)
                {
                    var status = command.Status.ConvertToEnum(backup:getOrderByBuyerId.Status);
                    var checkStatus = command.Status.IsNotSmallerThan((int)getOrderByBuyerId.Status);

                    if (checkStatus)
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
