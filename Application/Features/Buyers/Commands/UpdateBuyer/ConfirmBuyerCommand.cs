using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Commands.UpdateBuyer
{
    public class ConfirmBuyerCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public BuyerDtoConfirm Buyer { get; set; }
    }
}
