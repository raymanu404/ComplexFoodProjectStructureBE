using MediatR;
using Application.DtoModels.Buyer;
using Domain.Models.Roles;

namespace Application.Features.Buyers.Queries.GetBuyersList;

public class GetBuyersListQuery : IRequest<List<Buyer>>
{
}