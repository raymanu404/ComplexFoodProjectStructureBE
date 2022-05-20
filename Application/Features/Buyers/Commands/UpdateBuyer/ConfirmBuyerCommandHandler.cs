using Application.Contracts.Persistence;
using MediatR;
using Domain.ValueObjects;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class ConfirmBuyerCommandHandler : IRequestHandler<ConfirmBuyerCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmBuyerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(ConfirmBuyerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);

                if (buyer != null)
                {
                    if (buyer.ConfirmationCode.Value.Equals("confirmed"))
                    {
                        return "Contul este deja confirmat!";
                    }

                    if (buyer.ConfirmationCode.Value.Equals(command.Buyer.ConfirmationCode))
                    {
                        buyer.Confirmed = true;
                        buyer.ConfirmationCode = new UniqueCode("confirmed");
                        await _unitOfWork.CommitAsync(cancellationToken);

                    }
                    else
                    {
                        return "Cod invalid!";
                    }

                }
                else
                {
                    return "Buyer-ul nu exista!";
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Nu s-a putut confirma!";
            }

            return "Buyer-ul a fost confirmat cu succes!";
        }
    }
}
