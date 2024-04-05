using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Buyer;


namespace Application.Features.Buyers.Queries.GetBuyerByEmail
{
    public class GetBuyerByEmailQuery : IRequest<string>
    {
        public BuyerForgotPasswordDto EmailBuyer { get; set; }
    }
}
