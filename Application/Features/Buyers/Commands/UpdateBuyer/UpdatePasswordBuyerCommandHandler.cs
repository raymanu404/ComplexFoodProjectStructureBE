using MediatR;
using Application.DtoModels.Buyer;
using Application.Contracts.Persistence;
using Application.Components;
using Domain.ValueObjects;


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

                var validatePassword = new Password(request.BuyerUpdatePassword.OldPassword);
                if (validatePassword.Value.Equals(""))
                {
                    return "Old Password invalid!";
                }
                var validatePassword1 = new Password(request.BuyerUpdatePassword.NewPassword);
                if (validatePassword1.Value.Equals(""))
                {
                    return "New Password invalid!";
                }

                var encodeOldPassoword = EncodePassword.ComputeSha256Hash(request.BuyerUpdatePassword.OldPassword);
                if (buyer.Password.Value.Equals(encodeOldPassoword))
                {
                    var encodeNewPassoword = EncodePassword.ComputeSha256Hash(request.BuyerUpdatePassword.NewPassword);
                    buyer.Password = new Password(encodeNewPassoword);

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
