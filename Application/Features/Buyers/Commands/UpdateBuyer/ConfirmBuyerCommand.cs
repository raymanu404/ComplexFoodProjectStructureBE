using MediatR;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class ConfirmBuyerCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public bool Confirmed { get; set; }
    }
}
