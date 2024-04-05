using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Buyer;


namespace Application.Features.Customer.Buyers.Queries.GetBuyerByEmail
{
    public class GetBuyerByEmailQuery : IRequest<string>
    {
        public BuyerForgotPasswordDto EmailBuyer { get; set; }
    }
}
