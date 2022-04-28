using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Queries.GetBuyerById
{
    public class GetBuyerByIdQuery : IRequest<BuyerDto>
    {
        public int BuyerId { get; set; }
    }
}
