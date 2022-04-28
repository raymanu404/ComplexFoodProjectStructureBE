using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Commands.UpdateBuyer;

public class UpdateBuyerCommand : IRequest<BuyerDto>
{
    public int BuyerId { get; set; }
    public BuyerDto Buyer { get; set; }

}