using MediatR;
using Application.DtoModels.Buyer;
using Application.Contracts.Persistence;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class UpdatePasswordBuyerCommandHandler : IRequestHandler<UpdatePasswordBuyerCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePasswordBuyerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(UpdatePasswordBuyerCommand request, CancellationToken cancellationToken)
        {
            string returnMessage = "";
            var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);
            if(buyer != null)
            {
                if (buyer.Password.Value.Equals(request.BuyerUpdatePassword.OldPassword))
                {
                    buyer.Password = new Domain.ValueObjects.Password(request.BuyerUpdatePassword.NewPassword);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    returnMessage = "Password was updated successfully!";
                }
                else
                {
                    returnMessage = "Password incorect!";
                }

            }
            else
            {
                returnMessage = "Buyer doesn't exists!";
            }

            return returnMessage;
        }
    }
}
