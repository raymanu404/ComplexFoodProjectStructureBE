using MediatR;
using Domain.Models.Roles;
using Application.Contracts.Persistence;
using Application.DtoModels.Buyer;
using Domain.ValueObjects;
using AutoMapper;
using Application.Components;
using Domain.Models.Roles;

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
            var encodedPlainText = EncodePassword.ComputeSha256Hash(request.BuyerLogin.Password);
            var loginBuyer = await _unitOfWork.Buyers.LoginBuyer(new Email(request.BuyerLogin.Email), new Password(encodedPlainText));
            return _mapper.Map<BuyerDto>(loginBuyer);
        }
    }
}
