using MediatR;
using Domain.Models.Roles;
using Application.Contracts.Persistence;

namespace Application.Features.Buyers.Queries.LoginBuyer
{
    public class LoginBuyerQueryHandler : IRequestHandler<LoginBuyerQuery, Buyer>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginBuyerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Buyer> Handle(LoginBuyerQuery request, CancellationToken cancellationToken)
        {
            var loginBuyer = await _unitOfWork.Buyers.LoginBuyer(request.BuyerLogin.Email, request.BuyerLogin.Password);
            return loginBuyer;
        }
    }
}
