using MediatR;
using Domain.Models.Roles;
using Application.DtoModels.Buyer;


namespace Application.Features.Buyers.Queries.LoginBuyer
{
    public class LoginBuyerQuery: IRequest<Buyer>
    {
        public BuyerDtoLogin BuyerLogin { get; set; }       
    }
}
