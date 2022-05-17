using Application.Contracts.Persistence;
using MediatR;
using Domain.Models.Roles;
using AutoMapper;
using Application.DtoModels.Buyer;
using Application.Models;
using Microsoft.Extensions.Options;

namespace Application.Features.Buyers.Queries.GetBuyerById
{
    public class GetBuyerByIdQueryHandler : IRequestHandler<GetBuyerByIdQuery, BuyerDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;

        public GetBuyerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSettings = emailSettings.Value;
        }
        public async Task<BuyerDto> Handle(GetBuyerByIdQuery command, CancellationToken cancellationToken)
        {
           var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
           if(buyer == null)
           {
                return new BuyerDto() { };
           }

           return _mapper.Map<BuyerDto>(buyer);
        }
    }

}
