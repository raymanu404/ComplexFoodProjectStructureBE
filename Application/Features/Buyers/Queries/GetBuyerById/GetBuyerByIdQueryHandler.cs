using Application.Contracts.Persistence;
using MediatR;
using Domain.Models.Roles;
using AutoMapper;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Queries.GetBuyerById
{
    public class GetBuyerByIdQueryHandler : IRequestHandler<GetBuyerByIdQuery, BuyerDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBuyerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BuyerDto> Handle(GetBuyerByIdQuery command, CancellationToken cancellationToken)
        {
           var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
           return _mapper.Map<BuyerDto>(buyer);
        }
    }

}
