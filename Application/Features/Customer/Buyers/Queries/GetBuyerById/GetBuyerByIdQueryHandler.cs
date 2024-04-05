using MediatR;
using AutoMapper;
using Application.DtoModels.Buyer;
using Application.Models;
using Microsoft.Extensions.Options;
using Application.Contracts.Persistence.Customer;

namespace Application.Features.Customer.Buyers.Queries.GetBuyerById
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
            if (buyer == null)
            {
                return new BuyerDto() { };
            }

            return _mapper.Map<BuyerDto>(buyer);
        }
    }

}
