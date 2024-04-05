using AutoMapper;
using MediatR;
using Application.DtoModels.Buyer;
using Application.Contracts.Persistence.Customer;

namespace Application.Features.Customer.Buyers.Queries.GetBuyersList;

public class GetBuyersListQueryHandler : IRequestHandler<GetBuyersListQuery, List<BuyerDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBuyersListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BuyerDto>> Handle(GetBuyersListQuery request, CancellationToken cancellationToken)
    {

        var buyers = await _unitOfWork.Buyers.GetAllAsync();
        return _mapper.Map<List<BuyerDto>>(buyers);
    }
}