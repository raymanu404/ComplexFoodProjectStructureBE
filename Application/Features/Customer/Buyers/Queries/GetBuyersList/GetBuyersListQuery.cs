using MediatR;
using Application.DtoModels.Buyer;

namespace Application.Features.Customer.Buyers.Queries.GetBuyersList;

public class GetBuyersListQuery : IRequest<List<BuyerDto>>
{
}