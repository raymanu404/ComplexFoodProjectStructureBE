using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.OrderItems.Commands.UpdateItemCommand
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            //var itemToUpdate = await _unitOfWork.Items.GetItemByIdAsync(request.ItemId);
            //if(itemToUpdate != null)
            //{
            //    itemToUpdate.Amount = request.Amount != null ? request.Amount : itemToUpdate.Amount;
            //    //itemToUpdate.ProductId = request.ProductId != null ? request.ProductId : itemToUpdate.ProductId;

            //    await _unitOfWork.CommitAsync(cancellationToken);
            //}

            return Unit.Value;
        }
    }
}
