using MediatR;
using Application.DtoModels.Buyer;
using Domain.Models.Roles;

namespace Application.Features.Buyers.Commands.CreateBuyer
{

    public class CreateBuyerCommand : IRequest<BuyerDto>
    {
        public BuyerDto Buyer { get; set; }
    }
}