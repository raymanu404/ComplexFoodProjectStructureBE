using Application.Contracts.Persistence.Customer;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Buyers.Commands.UpdateBuyer;

public class UpdateBuyerCommandHandler : IRequestHandler<UpdateBuyerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBuyerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateBuyerCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);

        if (buyer != null)
        {

            if (command.Buyer.FirstName == null || command.Buyer.FirstName.Equals("string") || command.Buyer.FirstName.Trim().Equals(""))
            {
                command.Buyer.FirstName = buyer.FirstName.Value;
            }

            if (command.Buyer.LastName == null || command.Buyer.LastName.Equals("string") || command.Buyer.LastName.Trim().Equals(""))
            {
                command.Buyer.LastName = buyer.LastName.Value;
            }

            if (command.Buyer.PhoneNumber == null || command.Buyer.PhoneNumber.Equals("string") || command.Buyer.PhoneNumber.Trim().Equals(""))
            {
                command.Buyer.PhoneNumber = buyer.PhoneNumber.Value;
            }

            buyer.FirstName = new Name(command.Buyer.FirstName);
            buyer.LastName = new Name(command.Buyer.LastName);
            buyer.PhoneNumber = new PhoneNumber(command.Buyer.PhoneNumber);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        else
        {
            return -1;
        }

        return 1;
    }
}