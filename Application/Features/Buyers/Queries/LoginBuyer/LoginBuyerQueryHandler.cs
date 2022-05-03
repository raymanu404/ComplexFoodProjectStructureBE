using MediatR;
using Domain.Models.Roles;
using Application.Contracts.Persistence;
using Application.DtoModels.Buyer;
using Domain.ValueObjects;
using AutoMapper;

namespace Application.Features.Buyers.Queries.LoginBuyer
{
    public class LoginBuyerQueryHandler : IRequestHandler<LoginBuyerQuery, BuyerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoginBuyerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BuyerDto> Handle(LoginBuyerQuery request, CancellationToken cancellationToken)
        {
            var loginBuyer = await _unitOfWork.Buyers.LoginBuyer(new Email(request.BuyerLogin.Email), new Password(request.BuyerLogin.Password));

            return _mapper.Map<BuyerDto>(loginBuyer);
        }
    }
}
