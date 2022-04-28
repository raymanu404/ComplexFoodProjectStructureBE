using MediatR;
using Application.Contracts.Persistence;
using Domain.Models.Ordering;

namespace Application.Features.OrderItems.Commands.DeleteItemCommand
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            //var item = await _unitOfWork.Items.GetItemByIdAsync(request.ItemId);
            //if(item != null)
            //{
            //    _unitOfWork.Items.DeleteItem(item);
            //    await _unitOfWork.CommitAsync(cancellationToken);
            //}

            return Unit.Value;
        }
    }
}
