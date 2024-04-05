using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class DepositBalanceBuyerCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public BuyerDepositBalanceDto DepositBalanceDto { get; set; }
    }
}
