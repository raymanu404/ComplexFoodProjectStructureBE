using MediatR;
using Domain.ValueObjects;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class DepositBalanceBuyerCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public Balance Balance { get; set; }
    }
}
