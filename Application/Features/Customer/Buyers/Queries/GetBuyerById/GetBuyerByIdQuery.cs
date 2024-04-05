using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Customer.Buyers.Queries.GetBuyerById
{
    public class GetBuyerByIdQuery : IRequest<BuyerDto>
    {
        public int BuyerId { get; set; }
    }
}
