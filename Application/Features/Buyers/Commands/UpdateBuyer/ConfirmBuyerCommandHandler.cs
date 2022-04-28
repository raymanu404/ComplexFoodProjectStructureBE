using Application.Contracts.Persistence;
using MediatR;


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
                    buyer.Confirmed = command.Confirmed;
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Nu s-a putut confirma!";
            }

            return "Buyer-ul a fost confirmat cu succes!";
        }
    }
}
