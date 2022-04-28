using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.OrderItems.Commands.UpdateItemCommand
{
    public class UpdateCartIdForOrderItemCommandHandler : IRequestHandler<UpdateCartIdForOrderItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCartIdForOrderItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateCartIdForOrderItemCommand command, CancellationToken cancellationToken)
        {
            //var item = await _unitOfWork.Items.GetItemByIdAsync(command.ItemId);
            //if(item != null)
            //{
            //    var cart = await _unitOfWork.Carts.GetByIdAsync(command.CartId);
            //    if(cart != null)
            //    {
            //        //item.CartId = command.CartId;
            //        await _unitOfWork.CommitAsync(cancellationToken);
            //    }
                
            //}
            return Unit.Value;
        }
    }
}
