using Application.Contracts.Persistence.Customer;
using MediatR;

namespace Application.Features.Customer.Buyers.Commands.DeleteBuyer
{
    public class DeleteBuyerByIdCommandHandler : IRequestHandler<DeleteBuyerByIdCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBuyerByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBuyerByIdCommand request, CancellationToken cancellationToken)
        {
            var buyerToDelete = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
            if (buyerToDelete != null)
            {
                _unitOfWork.Buyers.Delete(buyerToDelete);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
