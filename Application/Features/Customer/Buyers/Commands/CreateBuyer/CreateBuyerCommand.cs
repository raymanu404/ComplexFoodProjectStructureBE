using MediatR;
using Application.DtoModels.Buyer;
using Domain.Models.Roles;

namespace Application.Features.Customer.Buyers.Commands.CreateBuyer
{

    public class CreateBuyerCommand : IRequest<string>
    {
        public BuyerRegisterDto Buyer { get; set; }
    }
}