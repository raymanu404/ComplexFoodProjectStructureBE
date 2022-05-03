using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Queries.LoginBuyer
{
    public class LoginBuyerQuery: IRequest<BuyerDto>
    {
        public BuyerDtoLogin BuyerLogin { get; set; }       
    }
}
