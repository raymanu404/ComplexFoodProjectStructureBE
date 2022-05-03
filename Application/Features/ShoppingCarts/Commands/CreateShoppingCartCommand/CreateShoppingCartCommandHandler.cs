using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using Domain.Models.Shopping;
using Domain.ValueObjects;
using Application.DtoModels.Buyer;
using Application.DtoModels.Cart;

namespace Application.Features.ShoppingCarts.Commands.CreateShoppingCartCommand
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCart>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShoppingCart> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
        {

            var cart = _mapper.Map<ShoppingCart>(command.Cart);
            await _unitOfWork.ShoppingCarts.AddAsync(cart);
            await _unitOfWork.CommitAsync(cancellationToken);
            return cart;
        }
   
    }
}
