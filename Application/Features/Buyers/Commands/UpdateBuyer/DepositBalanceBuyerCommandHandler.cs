using MediatR;
using Domain.ValueObjects;
using Application.Contracts.Persistence;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class DepositBalanceBuyerCommandHandler : IRequestHandler<DepositBalanceBuyerCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepositBalanceBuyerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(DepositBalanceBuyerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var buyer = await _unitOfWork.Buyers.GetByIdAsync(request.BuyerId);

                if (buyer != null)
                {
                    if (buyer.Confirmed)
                    {
                        buyer.Balance = new Balance(buyer.Balance.Value + request.DepositBalanceDto.Balance);
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                    {
                        return "Contul dumneavoastra nu este confirmat pentru a putea face depuneri!";
                    }

                }
                else
                {
                    return $"Buyer-ul nu a fost gasit!";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Tranzactia de depunere a fost esuata...";
            }

            return $"Suma {request.DepositBalanceDto.Balance} RON a fost depusa cu succes!";
        }
    }
}
