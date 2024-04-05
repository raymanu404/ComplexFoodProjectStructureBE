using MediatR;
using Application.DtoModels.Buyer;
using Domain.Models.Roles;

namespace Application.Features.Customer.Buyers.Queries.LoginBuyer
{
    public class LoginBuyerQuery : IRequest<BuyerDto>
    {
        public BuyerLoginDto BuyerLogin { get; set; }
    }
}
